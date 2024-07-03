using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclesExtension
{
    public class Truck : Vehicle
    {
        //1.6 liters for the truck
        //Also, the truck has a tiny hole in its tank and when it’s refueled it keeps only 95% of the given fuel
        private const double IncreasedFuel = 1.6;
        private const double RealFuel = 0.95;
        public Truck(double fuelQuantity, double fuelConsumptionPerKm,double tankCapacity)
            : base(fuelQuantity, fuelConsumptionPerKm + IncreasedFuel, tankCapacity)
        {
        }
        public override void Refuel(double fuelAmount)
        {
            if (fuelAmount <= 0) 
                throw new ArgumentException("Fuel must be a positive number");
            if (TankCapacity < fuelAmount + FuelQuantity)
                throw new ArgumentException($"Cannot fit {fuelAmount} fuel in the tank");
            base.Refuel(fuelAmount * RealFuel);
        }
    }
}
