using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        private int money;
        private List<Product> products = new();

        public Person(string name, int money)
        {
            Name = name;
            Money = money;
            products = new List<Product>();
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
        public int Money
        {
            get => money;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
            }
        }
        public IReadOnlyCollection<Product> Products { get => products.AsReadOnly(); }
        public string AddProduct(Product product)
        {
            if (money < product.Price)
            {
                //("{personName} can't afford {productName}").
                return $"{Name} can't afford {product.Name}";
            }
            products.Add(product);
            Money -= product.Price;
            return $"{Name} bought {product.Name}";
        }

    }
}
