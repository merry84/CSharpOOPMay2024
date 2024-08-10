using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar :Car
    {
        //The cubic centimeters for this type of car are 5000. Minimum horsepower is 400 and maximum horsepower is 600.
        // If you receive horsepower which is not in the given range throw ArgumentException with the message "Invalid horse power: {horsepower}.".
        // 
        public MuscleCar(string model, int horsePower)
            : base(model, horsePower, 5000, 400, 600)
        {
        }
    }
}
