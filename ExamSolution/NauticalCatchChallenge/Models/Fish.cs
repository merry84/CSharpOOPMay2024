using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public abstract class Fish : IFish
    {
        private string name;
        private double points;
        private int timeToCatch;

        protected Fish(string name, double points, int timeToCatch)
        {
            Name = name;
            Points = points;
            TimeToCatch = timeToCatch;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.FishNameNull);
                }
                name = value;
            }
        }
        /*o	Represents the points a fish will bring to the diver.
            o	Must be a value between 1 and 10, both inclusive. 
        If not, throw a new ArgumentException with the message:
        "Points must be a numeric value, between 1 and 10."
            o	This number will have at most one decimal place. 
        This means the value can range from a whole number like 1 or 10, 
        to a number with one digit after the decimal point, such as 1.1, 2.5, 3.7, or 9.1.
            */
        public double Points
        {
            get => points;

            private set
            {
                if(value<1 && value > 10)
                {
                    string.Format(ExceptionMessages.PointsNotInRange);
                }
                points = value;
            }
        }
        public int TimeToCatch
        {
            get => timeToCatch;
            private set
            {
                timeToCatch = value;
            }
        }
        public override string ToString()
        {
            return$"{GetType().Name}: {Name} [ Points: {Points}, Time to Catch: {TimeToCatch} seconds ]";
        }

    }
}
