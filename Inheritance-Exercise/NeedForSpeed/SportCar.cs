using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class SportCar : Car
    {
        private double DefaultFuelConsumption = 10;
        public override double FuelConsumption => DefaultFuelConsumption;
        public SportCar(int horsePower, double fuel) : base(horsePower, fuel)
        {
        }
    }

}
