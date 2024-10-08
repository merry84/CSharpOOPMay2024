﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;


namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private FormulaOneCarRepository formulaOneCarRepository;
        private RaceRepository raceRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            formulaOneCarRepository = new FormulaOneCarRepository();
            raceRepository = new RaceRepository();
        }
        public string CreatePilot(string fullName)//
        {
            if (pilotRepository.Models.Any(x => x.FullName == fullName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }

            Pilot pilot = new Pilot(fullName);
            pilotRepository.Add(pilot);
            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement) //
        {
            if (formulaOneCarRepository.Models.Any(x => x.Model == model))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            if (type != "Ferrari" && type != "Williams")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            var car = type == "Ferrari"
                ? (IFormulaOneCar)new Ferrari(model, horsepower, engineDisplacement)
                : new Williams(model, horsepower, engineDisplacement);

            formulaOneCarRepository.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps) // 
        {
            if (raceRepository.Models.Any(x => x.RaceName == raceName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            Race race = new Race(raceName, numberOfLaps);
            raceRepository.Add(race);
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel) //
        {
            if (!pilotRepository.Models.Any(x => x.FullName == pilotName) || pilotRepository.Models.First(x => x.FullName == pilotName).CanRace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            if (!formulaOneCarRepository.Models.Any(x => x.Model == carModel))
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage,
                    carModel));
            }

            IPilot pilot = pilotRepository.Models.First(x => x.FullName == pilotName);
            IFormulaOneCar car = formulaOneCarRepository.Models.First(x => x.Model == carModel);
            pilot.AddCar(car);
            formulaOneCarRepository.Remove(car);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName) //
        {
            if (!raceRepository.Models.Any(x => x.RaceName == raceName))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));

            }

            IRace race = raceRepository.Models.First(x => x.RaceName == raceName);
            if (!pilotRepository.Models.Any(x => x.FullName == pilotFullName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            IPilot pilot = pilotRepository.Models.First(x => x.FullName == pilotFullName);
            if (!pilot.CanRace || race.Pilots.Any(x => x.FullName == pilotFullName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage,
                    pilotFullName));
            }
            race.AddPilot(pilot);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string StartRace(string raceName)
        {
            IRace race = raceRepository.FindByName(raceName);

            if (race == null)
            {
                string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName);
            }

            if (race!.Pilots.Count < 3)
            {
                string.Format(ExceptionMessages.InvalidRaceParticipants, raceName);
            }

            if (race.TookPlace)
            {
                string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName);
            }

            var fastestPilots = race.Pilots.OrderByDescending(p => p.Car.RaceScoreCalculator(race.NumberOfLaps))
                .Take(3).ToList();
            race.TookPlace = true;

            fastestPilots.First().WinRace();

            StringBuilder raceResult = new StringBuilder();

            for (int i = 0; i < fastestPilots.Count; i++)
            {
                if (i == 0)
                {
                    raceResult.AppendLine($"Pilot {fastestPilots[i].FullName} wins the {race.RaceName} race.");
                }
                else if (i == 1)
                {
                    raceResult.AppendLine($"Pilot {fastestPilots[i].FullName} is second in the {race.RaceName} race.");
                }
                else
                {
                    raceResult.AppendLine($"Pilot {fastestPilots[i].FullName} is third in the {race.RaceName} race.");
                }
            }

            return raceResult.ToString().TrimEnd();


        }

        public string RaceReport()
        {
            IEnumerable<IRace> racesToReport = raceRepository.Models.Where(x => x.TookPlace);
            return string.Join(Environment.NewLine, racesToReport.Select(x => x.RaceInfo()));
        }

        public string PilotReport()
        {
            IOrderedEnumerable<IPilot> pilotsToReport = pilotRepository.Models.OrderByDescending(x => x.NumberOfWins);
            return string.Join(Environment.NewLine, pilotsToReport);
        }
    }
}