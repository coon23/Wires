using System.Windows.Media;
using System.Windows.Shapes;

namespace Wires.Data
{
    internal interface IDrawable
    {
        Shape CrossSectionVisual(double scale, Brush stroke, double strokeThickness, Brush fill);
    }
}
