using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public class Stolen :Delicacy
    {
        //The Stolen has a constant value for stolenPrice – 3.50
        private const double StolenPrice = 3.50;
        public Stolen(string name)
            : base(name, StolenPrice)
        {
        }
    }
}
