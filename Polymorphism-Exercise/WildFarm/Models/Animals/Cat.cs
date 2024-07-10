using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        private const double increased = 0.30;
        public Cat(string name, double weight,  string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {
        }

        public override void Eaten(string food, int quantity)
        {
            //•	Cats eat vegetables and meat
            if (food != "Vegetable" && food != "Meat")
            {
                throw new ArgumentException($"{GetType().Name} does not eat {food}!");
            }
            Weight += quantity * increased;
            FoodEaten = quantity;
        }

        public override string ProduseSound()
       => "Meow";
    }
}
