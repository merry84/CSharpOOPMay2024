
using System;
using System.Linq;
using System.Numerics;
using System.Text;
using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private IRepository<IPlanet> planets;

        public Controller()
        {
            planets = new PlanetRepository();
        }

        public string CreatePlanet(string name, double budget)
        {
            /*Creates a planet with the provided name and budget.
               •	If a Planet with the same name is already created, return the following message: "Planet {planetName} is already added!"
               •	If the planet is valid, keep it in the repository of planets and return the following message: "Successfully added Planet: {planetName}"
               */
            IPlanet planet = new Planet(name, budget);

            if (planets.FindByName(name) != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            planets.AddItem(planet);
            return string.Format(OutputMessages.NewPlanet, name);

        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            /*Creates a MilitaryUnit from the given type and adds it to the Army of the Planet with the given name. Every unit is unique.
             A Planet can have only one MilitaryUnit from a specific type:
               o	If a Planet with the given name doesn’t exist in the PlanetReposotiry, throw an InvalidOperationException with the following message:
            "Planet {planetName} does not exist!"
               o	If the MilitaryUnit is not available in our application (no such type of MilitaryUnit exists in the child classes),
            throw an InvalidOperationException with the following message: "{unitTypeName} still not available!"
               o	If the same MilitaryUnit is already added, throw an InvalidOperationException with the following message:
            "{unitTypeName} already added to the Army of {planetName}!"
               o	If the MilitaryUnit is valid, add it to the UnitRepository of the planet.
            Planet’s Budget is reduced with the price of the unit and the following message is returned:
            "{unitTypeName} added successfully to the Army of {planetName}!"
               */
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)
            {
                return string.Format(ExceptionMessages.UnexistingPlanet, planetName);
            }

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
            {
                return string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName);
            }

            IMilitaryUnit unit;
            if (unitTypeName == nameof(SpaceForces))
            {
                unit = new SpaceForces();
            }
            else if (unitTypeName == nameof(StormTroopers))
            {
                unit = new StormTroopers();
            }
            else if(unitTypeName == nameof(AnonymousImpactUnit))
            {
                unit = new AnonymousImpactUnit();
            }
            else
            {
                return string.Format(ExceptionMessages.ItemNotAvailable,unitTypeName);
            }

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);

        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            /*Creates a Weapon from the given type and adds it to the Weapons of the Planet with the given name. Every weapon is unique.
             A Planet can have only one Weapon from a specific type:
               o	If a Planet with the given name doesn’t exist in the PlanetRepository, throw an InvalidOperationException with the following message:
            "Planet {planetName} does not exist!"
               o	If the same Weapon is already added, throw an InvalidOperationException with the following message:
            "{weaponTypeName} already added to the Weapons of {planetName}!"
               o	If the Weapon is not available in our application (no such type of Weapon exists in the child classes),
            throw an InvalidOperationException with the following message: "{weaponTypeName} still not available!"
               o	If the Weapon is valid, add it to the WeaponRepository of the planet.
            Planet’s Budget is reduced with the price of the weapon and the following message is returned: "{planetName} purchased {weaponTypeName}!"
               */
            if (planets.Models.All(w => w.Name != planetName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            IPlanet planet = planets.FindByName(planetName);

            if (planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            IWeapon weapon;

            switch (weaponTypeName)
            {
                case "BioChemicalWeapon":
                    weapon = new BioChemicalWeapon(destructionLevel);
                    break;
                case "NuclearWeapon":
                    weapon = new NuclearWeapon(destructionLevel);
                    break;
                case "SpaceMissiles":
                    weapon = new SpaceMissiles(destructionLevel);
                    break;
                default:
                    throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable,
                        weaponTypeName));
            }

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);

        }

        public string SpecializeForces(string planetName)
        {
            /*Increases the EnduranceLevel of the Army of the specific Planet:
               o	If a Planet with the given name doesn't exist, throw an InvalidOperationException with the following message:
            "Planet {planetName} does not exist!"
               o	If there are no Military units added still, throw an InvalidOperationException with the following message:
            "No units available for upgrade!"
               o	If the action is completed successfully, reduce the Budget by 1.25 billion QUID,
            train the army of the given Planet and return the following message: "{planetName} has upgraded its forces!".
               */
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                return string.Format(ExceptionMessages.UnexistingPlanet, planetName);

            }

            if (planet.Army.Count == 0)
            {
                return string.Format(ExceptionMessages.NoUnitsFound);
            }

            planet.Spend(1.25);
            planet.TrainArmy();
            return string.Format(OutputMessages.ForcesUpgraded, planetName);

        }


        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = planets.FindByName(planetOne);
            IPlanet secondPlanet = planets.FindByName(planetTwo);
            IPlanet winner;
            IPlanet loser;

            if (firstPlanet.MilitaryPower.Equals(secondPlanet.MilitaryPower))
            {
                bool firstPlanetHasNuclearWeapon =
                    firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon));
                bool secondPlanetHasNuclearWeapon =
                    secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon));
                if (firstPlanetHasNuclearWeapon && !secondPlanetHasNuclearWeapon)
                {
                    winner = firstPlanet;
                    loser = secondPlanet;
                }
                else if (!firstPlanetHasNuclearWeapon && secondPlanetHasNuclearWeapon)
                {
                    winner = secondPlanet;
                    loser = firstPlanet;
                }
                else
                {
                    firstPlanet.Spend(firstPlanet.Budget / 2);
                    secondPlanet.Spend(secondPlanet.Budget / 2);
                    return OutputMessages.NoWinner;
                }
            }
            else if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                winner = firstPlanet;
                loser = secondPlanet;
            }
            else
            {
                winner = secondPlanet;
                loser = firstPlanet;
            }

            winner.Spend(winner.Budget / 2);
            winner.Profit(loser.Budget / 2);
            winner.Profit(loser.Army.Sum(mu => mu.Cost) + loser.Weapons.Sum(w => w.Price));
            planets.RemoveItem(loser.Name);
            return string.Format(OutputMessages.WinnigTheWar, winner.Name, loser.Name);

        }


        public string ForcesReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in planets.Models.OrderByDescending(x => x.MilitaryPower).ThenBy(x => x.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}


