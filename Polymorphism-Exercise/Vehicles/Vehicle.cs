
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Contracts;

namespace Vehicles
{
    public abstract class Vehicle : IDriveble, IRefuelble
    {
        /*Car and truck both have fuel quantity, fuel consumption in liters per km, 
         * and can be driven a given distance and refueled with a given amount of fuel.
         * It's summer, so both vehicles use air conditioners and 
         * their fuel consumption per km is increased by 0.9 liters for the car and with 1.6 liters for the truck.
         * Also, the truck has a tiny hole in its tank and when it’s refueled it keeps only 95% of the given fuel. 
         * The car has no problems and adds all the given fuel to its tank. 
         * If a vehicle cannot travel the given distance, its fuel does not change*/
        private double fuelQuantity;
        private double fuelConsumptionPerKm;

        protected Vehicle(double fuelQuantity, double fuelConsumptionPerKm)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumptionPerKm = fuelConsumptionPerKm;
        }

        public double FuelQuantity
        {
            get => fuelQuantity;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fuel quantity must be positive number.");
                }
                fuelQuantity = value;
            }
        }
        public double FuelConsumptionPerKm
        {
            get => fuelConsumptionPerKm;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fuel consumption must be positive number.");
                }
                fuelConsumptionPerKm = value;
            }
        }

        public virtual void Drive(double distance)
        {
            double allConsumption = distance * FuelConsumptionPerKm;
            if (FuelQuantity< allConsumption)
            {
                //§	"Car/Truck travelled {distance} km"
                //Car/Truck needs refueling"
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }
            FuelQuantity -= allConsumption;
            Console.WriteLine($"{GetType().Name} travelled {distance} km");
        }

        public virtual void Refuel(double fuelAmount)
        {
            if (fuelAmount < 0)
            {
                throw new ArgumentException("Fuel Amount must be positive number");
            }
            FuelQuantity += fuelAmount;
        }
        public override string ToString()
        => $"{this.GetType().Name}: {FuelQuantity:f2}".ToString();

    }
}
