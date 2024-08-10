using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasterRaces.Core.Contracts;

using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;
namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        //150/150
        private CarRepository cars;
        private DriverRepository drivers;
        private RaceRepository races;

        public ChampionshipController()
        {
            cars = new CarRepository();
            drivers = new DriverRepository();
            races = new RaceRepository();
        }
        public string CreateDriver(string driverName)
        {
            /*Creates a Driver with the given name and adds it to the appropriate repository.
               The method should return the following message:
               "Driver {name} is created."
               If a driver with the given name already exists in the driver repository, 
            throw an ArgumentException with the message "Driver {name} is already created."
               */
            if (drivers.GetAll().Any(x => x.Name == driverName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }
            IDriver driver = new Driver(driverName);
            //if (driver != null)
            //{
            //    string.Format(ExceptionMessages.DriversExists,driverName);
            //}
            drivers.Add(driver);
            return string.Format(OutputMessages.DriverCreated, driverName);

        }

        public string CreateCar(string type, string model, int horsePower)
        {
            /*Create a Car with the provided model and horsepower and add it to the repository. There are two types of Car: "Muscle" and "Sports".
               If the Car already exists in the appropriate repository throw an ArgumentException with the following message:
               "Car {model} is already created."
               If the Car is successfully created, the method should return the following message:
               "{"MuscleCar"/ "SportsCar"} {model} is created."
               */
            if (cars.GetAll().Any(x => x.Model == model))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
            }
            ICar car = null;

            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }

            else if (type == "Sports")
            {
                car = new SportsCar(model, horsePower);
            }
            cars.Add(car);
            return string.Format(OutputMessages.CarCreated, car.GetType().Name,model);
        }

        public string CreateRace(string name, int laps)
        {
            /*Creates a Race with the given name and laps and adds it to the RaceRepository.
               If the Race with the given name already exists, throw an InvalidOperationException with the message:
               •	"Race {name} is already create."
               If everything is successful you should return the following message:
               •	"Race {name} is created."
               */
            if (races.GetAll().Any(x => x.Name == name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }

            IRace race = new Race(name, laps);
            races.Add(race);
            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            /*Adds a Driver to the Race.
               If the Race does not exist in the RaceRepository, throw an InvalidOperationException with the message:
               •	"Race {name} could not be found."
               If the Driver does not exist in the DriverRepository, throw an InvalidOperationException with the message:
               •	"Driver {name} could not be found."
               If everything is successful, you should add the Driver to the Race and return the following message:
               •	"Driver {driver name} added in {race name} race."
               */
            if (!races.GetAll().Any(x => x.Name == raceName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            if (!drivers.GetAll().Any(x => x.Name == driverName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            IDriver driver = drivers.GetByName(driverName);
            IRace race = races.GetByName(raceName);
            race.AddDriver(driver);
            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            /*Gives the Car with a given name to the Driver with a given name (if exists).
               If the Driver does not exist in the DriverRepository, throw InvalidOperationException with the message 
               •	"Driver {name} could not be found."
               If the Car does not exist in the CarRepository, throw InvalidOperationException with the message 
               •	"Car {name} could not be found."
               If everything is successful you should add the Car to the Driver and return the following message:
               •	"Driver {driver name} received car {car name}."
               */
            if (!drivers.GetAll().Any(x => x.Name == driverName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            if (!cars.GetAll().Any(x => x.Model == carModel))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }
            IDriver driver = drivers.GetByName(driverName);
            ICar car = cars.GetByName(carModel);
            driver.AddCar(car);
            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string StartRace(string raceName)
        {
            if (!races.GetAll().Any(x => x.Name == raceName))
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));

            IRace race = races.GetByName(raceName);

            if (race.Drivers.Count < 3)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, 3));

            var drivers = race.Drivers
                .OrderByDescending(x => x.Car
                    .CalculateRacePoints(race.Laps))
                .Take(3);

            IDriver firstPlace = drivers.First();
            IDriver secondPlace = drivers.Skip(1).First();
            IDriver thirdPlace = drivers.Last();

            races.Remove(race);
            return string.Format(OutputMessages.DriverFirstPosition, firstPlace.Name, raceName) + Environment.NewLine +
                   string.Format(OutputMessages.DriverSecondPosition, secondPlace.Name, raceName) + Environment.NewLine +
                   string.Format(OutputMessages.DriverThirdPosition, thirdPlace.Name, raceName);
        }
    }
}
