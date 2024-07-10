
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Contracts;

namespace WildFarm.Models.Animals
{
    public abstract class Animal : ISound

    {
        private string name;
        private double weight;
        private int foodEaten;

        protected Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
           
        }

        public string Name { get => name; set => name = value; }
        public double Weight { get => weight; set => weight = value; }
        public int FoodEaten { get => foodEaten; set => foodEaten = value; }

        public abstract string ProduseSound();

        public abstract void Eaten(string food, int quantity);
    }
}
