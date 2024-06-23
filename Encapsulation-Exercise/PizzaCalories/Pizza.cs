using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Pizza
    {
        private string name;
        private Dough dough;
        private List<Topping> toppings;

        public Pizza(string name, Dough dough)
        {
            Name = name;
            this.dough = dough;
            toppings = new List<Topping>();
        }

        public string Name//Pizza should not be an empty string.
                          //In addition, it should not be longer than 15 symbols.
                          //If it does not fit, throw an Exception with
                          //the message "Pizza name should be between 1 and 15 symbols.".
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value)
                    || string.IsNullOrWhiteSpace(value)
                    || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                name = value;
            }
        }
        public Dough Dough { get => dough; set => dough = value; }
        public int ToppingCount => toppings.Count();
        public double TotalCalories()
        {
            double totalcalories = dough.GetCalories();
            foreach (var item in toppings)
            {
                totalcalories += item.GetCalories();
            }
            return totalcalories;
        }
        public void AddTopping(Topping topping)
        {
            if (ToppingCount is 10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }
            toppings.Add(topping);
        }
        public override string ToString()
        {
            return $"{name} - {TotalCalories():f2} Calories.";
        }

    }
}
