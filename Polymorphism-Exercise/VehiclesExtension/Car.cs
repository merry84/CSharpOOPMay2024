using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclesExtension
{
    public class Car : Vehicle
    {
        //increased by 0.9 liters for the car
        private const double IncreasedFuel = 0.9;
        public Car(double fuelQuantity, double fuelConsumptionPerKm,double tankCapacity)
            : base(fuelQuantity, fuelConsumptionPerKm + IncreasedFuel,tankCapacity)
        {
        }
    }
}
