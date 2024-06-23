using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBoxData
{
    public class Box
    {
        /*•Length - double, should not be zero or negative number
      •	Width - double, should not be zero or negative number
      •	Height - double, should not be zero or negative number
      If one of the properties is a zero or negative number throw ArgumentException with the message:
       "{propertyName} cannot be zero or negative." Use try-catch block to process the error. 
       All properties are set by the constructor and when set, they cannot be modified.
      */
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public double Length
        {
            get => length;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Length cannot be zero or negative.");
                }
                length = value;
            }
        }
        public double Width
        {
            get => width;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Width cannot be zero or negative.");
                }
                width = value;
            }
        }
        public double Height
        {
            get => height;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Height cannot be zero or negative.");
                }
                height = value;
            }
        }
        public double SurfaceArea()
        {
            //Surface Area = 2lw + 2lh + 2wh
            double surfaceArea = 2 * length * width + 2 * height * length + 2 * height * width;
            return surfaceArea;
        }

        public double LateralSurfaceArea()
        {
            //Lateral Surface Area = 2lh + 2wh
            double lateralArea = 2 * length * height + 2 * width * height;
            return lateralArea;
        }
        public double Volume()
        {
            double volume = length * width * height;
            return volume;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Surface Area - {SurfaceArea():f2}");
            sb.AppendLine($"Lateral Surface Area - {LateralSurfaceArea():f2}");
            sb.AppendLine($"Volume - {Volume():f2}");
            return sb.ToString().TrimEnd();
        }

    }
}
