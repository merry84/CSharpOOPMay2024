using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Product
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        /*•	A constructor with the following parameters:
o	Name – string
o	Price – decimal
*/
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

}
