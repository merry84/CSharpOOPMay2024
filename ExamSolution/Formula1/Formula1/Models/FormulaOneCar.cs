using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1.Core;
using Formula1.Models.Contracts;
using Formula1.Utilities;

namespace Formula1.Models
{
    public abstract class FormulaOneCar : IFormulaOneCar
    {
        private string model;
        private int horsePower;
        private double engineDisplacement;


        protected FormulaOneCar(string model, int horsepower, double engineDisplacement)
        {
            Model = model;
            Horsepower = horsepower;
            EngineDisplacement = engineDisplacement;

        }
        //o	If the model is null, white space, or the length is less than 3 symbols,
        //throw an ArgumentException with the message: "Invalid car model: { model }."
        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    string.Format(ExceptionMessages.InvalidF1CarModel, value);
                }
                model = value;
            }
        }
        //o	If the horsepower is less than 900, or more than 1050, throw an ArgumentException with the message: "Invalid car horsepower: { horsepower }."
        public int Horsepower
        {
            get => horsePower;
            private set
            {
                if (value < 900 || value > 1050)
                {
                    string.Format(ExceptionMessages.InvalidF1HorsePower, value);
                }
                horsePower = value;
            }
        }
        //o	If the engine displacement  is less than 1.6, or more than 2.00,
        //throw an ArgumentException with the message: "Invalid car engine displacement: { engine displacement }."
        public double EngineDisplacement
        {
            get => engineDisplacement;
            private set
            {
                if (value < 1.6 || value > 2.00)
                {
                    string.Format(ExceptionMessages.InvalidF1EngineDisplacement, value);
                }
                engineDisplacement = value;
            }
        }

        public double RaceScoreCalculator(int laps)
            => EngineDisplacement / Horsepower * laps;
    }
}
