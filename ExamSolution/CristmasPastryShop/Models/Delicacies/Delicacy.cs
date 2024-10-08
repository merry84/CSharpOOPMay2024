﻿using System;
using System.Collections.Generic;
using System.Text;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy :IDelicacy
    {
        private string name;
        private double price;

        protected Delicacy(string name,double price)
        {
            Name = name;
            Price = price;
        }
        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }

        public double Price
        {
            get;
            private set;
        }

        public override string ToString()
        =>$"{Name} - {Price:f2} lv";
    }
}
