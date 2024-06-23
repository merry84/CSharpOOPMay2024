using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree
{
    public class Product
    {
        private string name;
        private int price;

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name
        {
            get => name;
            private set
            {//(empty name Exception message: "Name cannot be empty") 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }
        public int Price
        {
            get => price;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                price = value;
            }
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
