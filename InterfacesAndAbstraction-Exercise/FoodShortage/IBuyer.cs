
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShortage
{
    public interface IBuyer :IIdentifiable
    {
        void BuyFood();
        public int Food { get;}
    }
}
