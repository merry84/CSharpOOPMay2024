using System;
using System.Collections.Generic;
using System.Text;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private string name;
        private ICar car;
        private bool canPaticipate;
        private int numberOfWins;

        public Driver(string name)
        {
            Name = name;
            CanParticipate = false;
        }

        //o	If the name is null, empty or less than 5 symbols throw an ArgumentException with the message "Name {name} cannot be less than 5 symbols."
        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, 5));
                }
                name = value;
            }
        }

        public ICar Car
        {
            get=>car;
            private set
            {
                //. If the car is null, throw ArgumentNullException with the message "Car cannot be null.".
                if (value == null)
                {
                    throw new ArgumentNullException(string.Format(ExceptionMessages.CarInvalid));
                }
                car = value;
            }
        }

        public int NumberOfWins
        {
            get=>numberOfWins;
            private set
            {
                numberOfWins = value;
            }
        }

        public bool CanParticipate
        {
            get=>canPaticipate;
            private set
            {
                canPaticipate = value;
            }
        }
        public void WinRace()
        {
            NumberOfWins++;
        }

        public void AddCar(ICar car)
        {
           Car = car;
           CanParticipate = true;
        }
    }
}
