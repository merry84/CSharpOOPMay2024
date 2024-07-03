using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Shape
    {
        //o	height and width 
        private double height;
        private double width;

        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }

        public double Height { get; private set; }

        public double Width { get; private set; }
        public override double CalculateArea()
        {
            return Height * Width;
        }

        public override double CalculatePerimeter()
        {

            return 2 * (Width + Height);
        }
        public override string Draw()
        {
            return base.Draw() + nameof(Rectangle);
        }
    }
}
