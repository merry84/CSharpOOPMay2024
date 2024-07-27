using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private double maxMileage;
        private string licensePlateNumber;
        private int batteryLevel;
        private bool isDamaged;

        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
            this.maxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
            batteryLevel = 100;
            isDamaged = false;
        }

        public string Brand
        {
            get
            => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.BrandNull);
                }
                brand = value;
            }
        }

        public string Model
        {
            get
            => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.ModelNull);
                }
                model = value;
            }
        }

        public double MaxMileage =>maxMileage;

        public string LicensePlateNumber
        {
            get
            => licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.LicenceNumberRequired);
                }
                licensePlateNumber = value;
            }
            
        }

        public int BatteryLevel => batteryLevel;

        public bool IsDamaged => isDamaged;

        public void ChangeStatus()
        {
            if (IsDamaged)
            { isDamaged = false; }
            else
            { isDamaged = true; }
        }

        public void Drive(double mileage)
        {
            /*The Drive() method should reduce the BatteryLevel by a certain percentage. 
             * It should be calculated what part of the MaxMileage will be passed (for example: if the given mileage is 90 kilometers and the Vehicle’s MaxMileage
                is 180 kilometers, then you should reduce BatteryLevel by 50%). 
            Also when driving CargoVan you should reduce additional 5%, because of the load. The percentage should be rounded to the closest integer number*/
            double percentage = Math.Round((mileage / this.maxMileage) * 100);
            this.batteryLevel -= (int)percentage;

            if (this.GetType().Name == "CargoVan")
            {
                this.batteryLevel -= 5;
            }

        }

        public void Recharge()
        {
            batteryLevel = 100;
        }
        public override string ToString()
        {
            /*Override the existing method ToString() and modify it, so the returned string must be in the following format:
                "{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: OK/damaged"*/
            string status;
            if (IsDamaged) 
            {
                status = "damaged";
            }
            else
            {
                status = "OK";
            }
            return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {status}";
        }
    }
}
