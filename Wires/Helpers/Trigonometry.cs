using System;
using Wires.Data;

namespace Wires.Helpers
{
    internal static class Trigonometry
    {

        /// <summary>
        /// Cosine theorem - returns cos(aplha).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double CosineTheorem(double a, double b, double c)
        {
            return
                (Math.Pow(c, 2) + Math.Pow(b, 2) - Math.Pow(a, 2)) / (2 * b * c);
        }

        /// <summary>
        /// Returns
        /// </summary>
        /// <param name="offSet"></param>
        /// <param name="side"></param>
        /// <param name="cosine"></param>
        /// <returns></returns>
        public static Tuple<Point2D, Point2D> Apex(Point2D offSet, double side, double cosine)
        {
            return new Tuple<Point2D, Point2D>
                (
                    new Point2D(
                        offSet.X + side * cosine,
                        offSet.Y + side * Math.Sqrt(1 - Math.Pow(cosine, 2))
                        ),

                    new Point2D(
                        offSet.X + side * cosine,
                        offSet.Y - side * Math.Sqrt(1 - Math.Pow(cosine, 2))
                        )
                );              
                          
        }

        
    }
}
