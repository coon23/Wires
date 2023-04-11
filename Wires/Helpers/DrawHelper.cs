using System.Windows.Controls;
using System.Windows.Shapes;

namespace Wires.Helpers
{
    internal static class DrawHelper
    {
        public static Shape GetShapeToDraw(Shape shape, double setTop, double setLeft)
        {
            Canvas.SetTop(shape, setTop);
            Canvas.SetLeft(shape, setLeft);
            return shape;
        } 
                
    }
}
