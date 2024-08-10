using System;
using System.Collections.Generic;
using System.Text;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        /*•	Model - string
           o	If the model is null, whitespace or less than 4 symbols, throw an ArgumentException with the message "Model {model} cannot be less than 4 symbols."
           o	All models are unique
           •	HorsePower - int
           o	Every type of car has a different range of valid horsepower. If the horsepower is not in the valid range,
        throw an ArgumentException with the message "Invalid horse power: {horsepower}."
           •	CubicCentimeters - double 
           o	Every type of car has different cubic centimeters
           */
        private string model;
        private int horsePower;
        private double cubicCentimeters;
        private int minHorsePower;
        private int maxHorsePower;

        protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            Model = model;
            CubicCentimeters = cubicCentimeters;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
            HorsePower = horsePower;

        }
        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidModel, value, 4));
                }
                model = value;
            }
        }

        public virtual int HorsePower
        {
            get => horsePower;
            private set
            {
                if (value < minHorsePower || value > maxHorsePower)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, value));
                }
                horsePower = value;
            }
        }

        public double CubicCentimeters
        {
            get => cubicCentimeters;
            private set
            {
                cubicCentimeters = value;
            }
        }
        public double CalculateRacePoints(int laps)
        => CubicCentimeters / HorsePower * laps;
    }
}
