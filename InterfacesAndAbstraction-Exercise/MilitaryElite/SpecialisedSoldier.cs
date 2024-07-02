using MilitaryElite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        private static readonly HashSet<string> validCorps = new HashSet<string>
        {
            "Airforses", "Marines"
        };
        public SpecialisedSoldier(int id, string firstName, string lastName, decimal salary,string corps)
            : base(id, firstName, lastName, salary)
        {
            if (!validCorps.Contains(corps))
            {
                throw new ArgumentException("Invalid corps!");
            }
            Corps = corps;
        }

        public string Corps { get; private set; }

        public override string ToString()
        {
            return base.ToString()+ $" Corps: {Corps}";
        }
    }
}
