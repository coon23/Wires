using System.Windows.Media;
using System.Windows.Shapes;

namespace Wires.Data
{
    internal class Wire : IDrawable
    {
        public Point2D? Center { get; set; }

        public double Radius { get; set; }


        public Wire(Point2D? center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public Shape CrossSectionVisual(double magnify, Brush stroke, double strokeThickness, Brush fill)
        {
            return new Ellipse
            {
                Width = 2 * Radius * magnify,
                Height = 2 * Radius * magnify,
                Stroke = stroke,
                StrokeThickness = strokeThickness,
                Fill = fill
            };            
        }
    }
}
