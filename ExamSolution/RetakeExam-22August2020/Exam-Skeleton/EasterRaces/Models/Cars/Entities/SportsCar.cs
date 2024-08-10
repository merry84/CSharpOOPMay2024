using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        //The cubic centimeters for this type of car are 3000. Minimum horsepower is 250 and maximum horsepower is 450.
        public SportsCar(string model, int horsePower)
            : base(model, horsePower, 3000, 250, 450)
        {
        }
    }
}
