using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models.Animals
{
    public class Hen : Bird
    {
        private const double increased = 0.35;
        public Hen(string name, double weight,  double wingSize)
            : base(name, weight,  wingSize)
        {
        }

        public override void Eaten(string food, int quantity)
        {
            Weight += quantity * increased;
            FoodEaten = quantity;
        }

        public override string ProduseSound()
        => "Cluck";
    }
}
