using System;
using System.Collections.Generic;
using System.Text;
using OnlineShop.Common.Constants;

namespace OnlineShop.Models.Products
{
    public abstract  class Product : IProduct

    {
        private int id;
        private string manufacturer;
        private string model;
        private decimal price;
        private double overallPerformance;

        protected Product(int id, string manufacturer, string model, decimal price, double overallPerformance)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Price = price;
            OverallPerformance = overallPerformance;
        }
        public int Id
        {
            get=> id;
            private set
            {
                if (value <= 0)
                {
                    string.Format(ExceptionMessages.InvalidProductId);
                }
                id = value;
            }
        }

        public string Manufacturer
        {
            get=> manufacturer;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.InvalidManufacturer);
                }
                manufacturer = value;
            }
        }

        public string Model
        {
            get=>model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.InvalidModel);
                }
                model = value;
            }
        }

        public virtual decimal Price
        {
            get=>price;
            private set
            {
                if (value <= 0)
                {
                    string.Format(ExceptionMessages.InvalidPrice);
                }
                price = value;
            }
        }

        public virtual double OverallPerformance
        {
            get=> overallPerformance;
            private set
            {
                if (value <= 0)
                {
                    string.Format(ExceptionMessages.InvalidOverallPerformance);

                }
                overallPerformance = value;
            }
        }

        public override string ToString()
        =>$"Overall Performance: {OverallPerformance}. Price: {Price} - {GetType().Name}: {Manufacturer} {Model} (Id: {Id})";
        
    }
}
