using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public class Gingerbread : Delicacy
    {
        //The Gingerbread has a constant value for gignerbreadPrice – 4.00
        private const double GignerbreadPrice = 4.00;
        public Gingerbread(string name) 
            : base(name, GignerbreadPrice)
        {
        }
    }
}
