using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models.Animals
{
    public abstract class Bird : Animal
    {
        public Bird(string name, double weight,  double wingSize) :
            base(name, weight)
        {
            WingSize = wingSize;
        }
        public double WingSize { get; set; }

     
        public override string ToString() => $"{GetType().Name} [{Name}, {WingSize}, {Weight}, {FoodEaten}]";



    }
}
