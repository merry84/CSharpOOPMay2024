using System;
using System.Collections.Generic;
using System.Text;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Utilities.Messages;

namespace PlanetWars.Models.Weapons
{
    public abstract class Weapon :IWeapon
    {
        private double price;
        private int destructionLevel;

        protected Weapon(double price,int destructionLevel)
        {
            Price = price;
            DestructionLevel = destructionLevel;
        }
        public double Price
        {
            get;
            private set;

        }

        public int DestructionLevel
        {
            get=>destructionLevel;
            private set
            {
                if (value < 1)
                {
                    string.Format(ExceptionMessages.TooLowDestructionLevel);
                } 
                if (value > 10)
                {
                    string.Format(ExceptionMessages.TooHighDestructionLevel);
                }
                destructionLevel = value;
            }
        }
    }
}
