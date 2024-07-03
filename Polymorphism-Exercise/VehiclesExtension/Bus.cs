using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclesExtension
{
    public class Bus : Vehicle
    {
        //fuel consumption per kilometer is increased by 1.4 liters
        private const double IncreasedFuel = 1.4;
        public Bus(double fuelQuantity, double fuelConsumptionPerKm, double tankCapacity)
            : base(fuelQuantity, fuelConsumptionPerKm, tankCapacity)
        {
        }
        public override void Drive(double distance)
        {
            double consumptionWithPeoples = distance * (FuelConsumptionPerKm + IncreasedFuel);
            if (consumptionWithPeoples > FuelQuantity)
            {
                throw new ArgumentException("Bus needs refueling");
            }
            FuelQuantity -= consumptionWithPeoples;
            Console.WriteLine($"{GetType().Name} travelled {distance} km");
        }
        public void DriveEmptyBus(double distance) 
        {
            double consumptions = distance * FuelConsumptionPerKm ;
            if(consumptions > FuelQuantity)
            {
                throw new ArgumentException("Bus needs refueling");
            }
            FuelQuantity-= consumptions;
            Console.WriteLine($"{GetType().Name} travelled {distance} km");
        }
    }
}
