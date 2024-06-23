﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class Car : Vehicle
    {
        private double DefaultFuelConsumption = 3;
        public override double FuelConsumption => DefaultFuelConsumption;
        public Car(int horsePower, double fuel) : base(horsePower, fuel)
        {
        }
    }

}