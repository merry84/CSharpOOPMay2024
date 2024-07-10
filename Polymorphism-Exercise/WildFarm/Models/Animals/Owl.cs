using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models.Animals
{
    public class Owl : Bird
    {
        //increase with every piece of food it eats
        private const double increased = 0.25;
        public Owl(string name, double weight, double wingSize)
            : base(name, weight,  wingSize)
        {
        }

        public override void Eaten(string food, int quantity)
        {
            //Owls eat only meat
            if (food != "Meat")
            {
                throw new ArgumentException($"{GetType().Name} does not eat {food}!");
            }
            Weight += quantity * increased;
            FoodEaten = quantity;
        }

        public override string ProduseSound()
        => "Hoot Hoot";
    }
}
