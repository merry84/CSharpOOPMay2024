using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
      

        private UnitRepository units;
        private WeaponRepository weapons;

        public Planet(string name,double budget)
        {
            Name = name;

            Budget = budget;
            units = new UnitRepository();
            weapons = new WeaponRepository();
        }

        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }
        //o	The initial available budget in billions QUID (Quasi Universal Intergalactic Denomination)
        // o	If the budget is less than 0, throw an ArgumentException with the message:
        //  "Budget's amount cannot be negative."

        public double Budget
        {
            get=>budget;
            private set
            {
                if (value < 0)
                {
                    string.Format(ExceptionMessages.InvalidBudgetAmount);
                }
                budget = value;
            }
        }
        /*•	MilitaryPower – double, rounded to the third decimal places. A calculated property.  In order to calculate the precise value,
         follow the sequence of the following operations:
           o	Total amount = (sum of unit endurances + sum of weapon destruction levels)
           o	If the planet has AnonymousImpactUnit in its military units (Army),  total amount increases by 30%
           o	If the planet has NuclearWeapon in its Weapons, the total amount increases by 45%
           o	First check for AnonymousImpactUnit and then for NuclearWeapon
           o	Remember to keep the property’s setter private
           HINT: The property may be calculated in a separate private method. In order to fulfill the requirements, use Math.Round(value, 3) for the returned value.
           */
        private double SumMilitaryPower()
        {
            double result = units.Models.Sum(x => x.EnduranceLevel) + weapons.Models.Sum(x => x.DestructionLevel);
            if (units.Models.Any(x=>x.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                result *= 1.30;//30%
            }
            else if (units.Models.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
            {
                result *= 1.45;
            }
            return result;

        }

        public double MilitaryPower => Math.Round(SumMilitaryPower(),3);
        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;
        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
            => units.AddItem(unit);

        public void AddWeapon(IWeapon weapon)
            => weapons.AddItem(weapon);

        public void TrainArmy()
        {
            foreach (var solder in units.Models)
            {
               solder.IncreaseEndurance();
            }
        }

        public void Spend(double amount)
        {
            //The  Spend() method should decrease the Budget by the given amount.
            // •	If the Budget is not enough to finish the purchase, throw an InvalidOperationException with the message: "Budget too low!"
            if (Budget < amount)
            {
                string.Format(ExceptionMessages.UnsufficientBudget);
            }
            Budget-=amount;
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public string PlanetInfo()
        {
            /*//Returns a string with information about the planet in the format below:
               // "Planet: {planetName}
               // --Budget: {budgetAmount} billion QUID
               // --Forces: {militaryUnitName1}, {militaryUnitName2}, {militaryUnitName3} (…) / No units
               // --Combat equipment: {weaponName1}, {weaponName2}, {weaponName3} (…) / No weapons
               // --Military Power: {militaryPower}"*/
            var sb = new StringBuilder();
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            sb.Append($"--Forces: ");
            if(Army.Count == 0)
            {
                sb.AppendLine("No units");
            }
            else
            {
                var units = new Queue<string>();
                foreach (var unit in Army)
                {
                    units.Enqueue(unit.GetType().Name);
                    
                }
                sb.AppendLine(string.Join(",",units));
            }

            sb.Append($"--Combat equipment: ");
            if (Weapons.Count == 0)
            {
                sb.AppendLine("No weapons");
            }
            else
            {
                var weapons = new Queue<string>();
                foreach (var weapon in Weapons)
                {
                    weapons.Enqueue(weapon.GetType().Name);

                }
                sb.AppendLine(string.Join(",", weapons));
            }

            sb.AppendLine($"--Military Power: {MilitaryPower}");
            return sb.ToString().TrimEnd();

        }
    }
}
