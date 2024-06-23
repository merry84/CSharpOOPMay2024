using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Topping
    {
        private const double Meat = 1.20;
        private const double Veggies = 0.80;
        private const double Cheese = 1.10;
        private const double Sauce = 0.90;

        private string type;
        private double grams;

        public Topping(string type, double grams)
        {
            Type = type;
            Grams = grams;
        }

        private string Type
        {
            set
            {
                if (!new List<string> { "meat", "veggies", "cheese", "sauce" }.Contains(value.ToLower()))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                type = value;
            }
        }
        private double Grams
        {
            set//a Тopping is in the range [1..50] grams. 
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{type} weight should be in the range [1..50].");
                }
                grams = value;
            }

        }
        private double CaloriesPerGram
        {
            get
            {
                double calories = 2;
                if (type.ToLower() is "meat") calories *= Meat;
                else if (type.ToLower() is "veggies") calories *= Veggies;
                else if (type.ToLower() is "cheese") calories *= Cheese;
                else if (type.ToLower() is "sauce") calories *= Sauce;

                return calories;

            }
        }
        public double GetCalories()
        {
            return grams * CaloriesPerGram;
        }

    }
}
