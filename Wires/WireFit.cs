using System;
using System.Collections.Generic;
using System.Linq;
using Wires.Data;
using Wires.Helpers;

namespace Wires
{
    class WireFit
    {
        private readonly Point2D ORIGIN;


        public Wire[] Wires { get;}
        public Cable Cable { get; set; }


        public WireFit(Wire[] wires)
        {
            ORIGIN = new Point2D(0, 0);
            Wires = wires;
            Cable = new Cable(ORIGIN, 0, wires);
        }

        /// <summary>
        /// Calculates position of all wires and calculates cable radius and position.
        /// </summary>
        public void Fit()
        {
            if (Wires == null || Wires.Length == 0)
            {
                return;
            }

            if(Wires.Length >= 1) // center of first put to the origin
            {
                Wires[0].Center = ORIGIN;
                Cable = Cable.CreateCable(Wires.Take(1).ToArray());
            }
            if(Wires.Length >= 2) // second wire next to first on x-axis
            {
                Wires[1].Center = new Point2D(Wires[0].Radius + Wires[1].Radius, 0);
                Cable = Cable.CreateCable(Wires.Take(2).ToArray());
            }

            List<Point2D> possibleCentersInOneStep;
            if (Wires.Length > 2)
            {
                for (int i = 2; i < Wires.Length; i++)
                {
                    possibleCentersInOneStep = GetPossibleWireCenters(Wires.Take(i + 1).ToArray()); // calculate new center using already placed centers
                    Cable = FindCable(possibleCentersInOneStep, Wires.Take(i + 1).ToArray());
                    Wires[i].Center = Cable.Wires.Last().Center;
                }
            }
        }

        /// <summary>
        /// Calculates center of next wire.
        /// *Should be optimized...*
        /// </summary>
        /// <param name="placedWires"></param>
        /// <returns></returns>
        private List<Point2D> GetPossibleWireCenters(Wire[] wiresPart)
        {
            Wire toPlace = wiresPart.Last();

            var pairs = Calc.GetAllPairs(wiresPart.Count() - 1); //already placed

            List<Point2D> possibleCenters = new List<Point2D>();

            //find wire center - all options
            foreach(var wirePair in pairs)
            {
                var bothCenters = CalculatedWireCenters(wiresPart[wirePair.Item1], wiresPart[wirePair.Item1], toPlace);
                Point2D point = CheckDistance(wiresPart, bothCenters);
                if(point == null)
                {
                    continue;
                }

                possibleCenters.Add(point);
            }

            return possibleCenters;
        }

        /// <summary>
        /// Calculates both centers of third wire.
        /// </summary>
        /// <param name="wiresPair"></param>
        /// <returns></returns>
        private Tuple<Point2D, Point2D> CalculatedWireCenters(Wire wire1, Wire wire2, Wire newWire)
        {
            double distance = wire1.Radius + newWire.Radius;
            double cos = Trigonometry.CosineTheorem(wire2.Radius + newWire.Radius, distance, wire1.Radius + wire2.Radius);

            return Trigonometry.Apex(wire1.Center, distance, cos);
        }

        /// <summary>
        /// Select appropriate wire center. 
        /// </summary>
        /// <param name="wiresPart"></param>
        /// <param name="centersPair"></param>
        /// <returns></returns>
        private Point2D? CheckDistance(Wire[] wiresPart, Tuple<Point2D, Point2D> centersPair)
        {
            double[] dist1 = new double[wiresPart.Length];
            double[] dist2 = new double[wiresPart.Length];

            for (int i = 0; i < wiresPart.Length - 1; i++) // last is to place
            {
                dist1[i] = Point2D.Distance(wiresPart[i].Center, centersPair.Item1) - wiresPart[i].Radius - wiresPart.Last().Radius;
                dist2[i] = Point2D.Distance(wiresPart[i].Center, centersPair.Item2) - wiresPart[i].Radius - wiresPart.Last().Radius;
            }

            Point2D result = dist1.Any(x => x >= 0) ? centersPair.Item1 :
               (dist2.Any(x => x >= 0) ? centersPair.Item2 : null );

            return result;
        }

        private Cable FindCable(List<Point2D> possibleCentersInOneStep, Wire[] wiresPart)
        {
            Cable[] cables = new Cable[possibleCentersInOneStep.Count];
            
            for (int i = 0; i < possibleCentersInOneStep.Count; i++)
            {
                wiresPart.Last().Center = possibleCentersInOneStep[i];
                cables[i] = Cable.CreateCable(wiresPart);
            }

            return cables.OrderBy(c => c.Radius).First();            
        }

    }
}
