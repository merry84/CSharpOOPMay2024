using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public abstract class Shape
    {
        /*•	Abstract methods:
        o	CalculatePerimeter(): double
        o	CalculateArea(): double
        •	Virtual methods:
        o	Draw(): string
        §	The method should get the name of class type as string, and should return a message in the format: 
        $"Drawing {classType.Name}"
        */
        public abstract double CalculatePerimeter();
        public abstract double CalculateArea();
        public virtual string Draw()
        => $"Drawing ";
    }
}
