using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class Vehicle
    {
        /*•	A constructor that accepts the following parameters: int horsePower, double fuel
        •	DefaultFuelConsumption – double 
        •	FuelConsumption – virtual double
        •	Fuel – double
        •	HorsePower – int
        •	virtual void Drive(double kilometers)
        o	The Drive method should have a functionality to reduce the Fuel based on the traveled kilometers.
        The default fuel consumption for Vehicle is 1.25*/

        private double DefaultFuelConsumption = 1.25;

        public Vehicle(int horsePower, double fuel)
        {
            HorsePower = horsePower;
            Fuel = fuel;

        }
        public virtual double FuelConsumption => DefaultFuelConsumption;
        public double Fuel { get; set; }
        public int HorsePower { get; set; }
        public virtual void Drive(double kilometers)
        {
            Fuel -= kilometers * FuelConsumption;
        }

    }

}
