
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShortage
{
    public class Rebel : IPerson ,IBuyer
    {
        private const int foodIncreases = 5;

        public Rebel(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
        }

        public string Name { get ; set ; }
        public int Age { get; set ; }
        public string Group { get; set; }
        public int Food { get; set; }
        public string Id { get ; set ; }

        public void BuyFood()
        => Food += foodIncreases;
    }
}
