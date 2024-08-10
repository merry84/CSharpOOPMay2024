using System;
using System.Collections.Generic;
using System.Text;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Races.Entities
{
    public class Race :IRace
    {
        private string name;
        private int laps;

        private List<IDriver> drivers;

        public Race(string name,int laps)
        {
            Name = name;
            Laps = laps;
            drivers = new List<IDriver>();
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

        public int Laps
        {
            get=>laps;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidNumberOfLaps, 1));
                }
                laps = value;
            }
        }
        public IReadOnlyCollection<IDriver> Drivers =>drivers.AsReadOnly();
        public void AddDriver(IDriver driver)
        {
            /*•	If a Driver is null throw an ArgumentNullException with the message "Driver cannot be null."
               •	If a Driver cannot participate in the Race (the Driver doesn't own a Car) 
            throw an ArgumentException with the message "Driver {driver name} could not participate in race."
               •	If the Driver already exists in the Race throw an ArgumentNullException with the message:
               "Driver {driver name} is already added in {race name} race."
               */
            if (driver == null)
            {
                throw new ArgumentNullException(ExceptionMessages.DriverInvalid);
            }

            if (!driver.CanParticipate)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriverNotParticipate, driver.Name));
            }

            if (drivers.Contains(driver))
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, Name));
            }

            drivers.Add(driver);
        }
    }
}
