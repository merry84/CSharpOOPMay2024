using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Food : Product
    {
        public Food(string name, decimal price, double grams) : base(name, price)
        {
            Grams = grams;
        }
        public double Grams { get; set; }
        /*•	A constructor with the following parameters: string name, decimal price, double grams
        o	Name – string
        o	Price – decimal
        o	Grams – double
        */
    }

}
