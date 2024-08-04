using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public class MulledWine :Cocktail
    {
        //The MulledWine has constant value for price of Large MulledWine – 13.50
        private const double MulledWinePrice = 13.50;
        public MulledWine(string name, string size)
            : base(name, size, MulledWinePrice)
        {
        }
    }
}
