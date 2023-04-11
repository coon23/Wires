using System.Collections.Generic;
using System.Linq;

namespace Wires.Data
{
    internal class Cable : Wire
    {
        public Wire[] Wires { get; set; }

        public Cable(Point2D center, double radius, Wire[] wires) : base(center, radius)
        {
            Wires = wires;
        }

        public static Cable? CreateCable(Wire[] wires)
        {
            if (wires == null)
            {
                return null;
            }

            //find center of circumscribed circle using 4 point
            double xPlus = wires.Max(w => (w.Center?.X ?? 0) + w.Radius); // Ox + directiom
            double xMinus = wires.Min(w => (w.Center?.X ?? 0) - w.Radius); // Ox - direction
            double yPlus = wires.Max(w => (w.Center?.Y ?? 0) + w.Radius); // Oy + directiom
            double yMinus = wires.Min(w => (w.Center?.Y ?? 0) - w.Radius); // Oy - direction

            Point2D center = new Point2D(
                    (xPlus + xMinus) / 2,
                    (yPlus + yMinus) / 2
                );

            //radius
            List<double> possibleRadii = new List<double>();
            Point2D edge; // point on the wire in the direction of dirNormVector = the farthest point
            Point2D dirNormVector; // normalized vector in direction between centers

            foreach(Wire wire in wires) // for all wires
            {
                dirNormVector = Point2D.NormalizedVector(center, wire.Center);
                edge = new Point2D(dirNormVector.X * wire.Radius, dirNormVector.Y * wire.Radius);
                possibleRadii.Add(
                    Point2D.Distance(center, edge)
                    );
            }

            double radius = possibleRadii.Max(); // maximum

            return new Cable(center, radius, wires);
        }

        

    }
}
