using System;

namespace Wires.Data
{
    /// <summary>
    /// Class represents a point in 2D plane.
    /// </summary>
    class Point2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns distance between two points.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double Distance(Point2D p1, Point2D p2)
        {
            return Math.Sqrt(Math.Pow(p1.X + p2.X, 2) + Math.Pow(p1.Y + p2.Y, 2));
        }

        /// <summary>
        /// Returns normalized (length = 1) vector given by two points.
        /// Direction: p2 ---> p1.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point2D NormalizedVector(Point2D p1, Point2D p2)
        {
            double distance = Distance(p1, p2);
            return new Point2D
                (
                    (p1.X - p2.X) / distance,
                    (p1.Y - p2.Y) / distance
                );
        }

    }
}
