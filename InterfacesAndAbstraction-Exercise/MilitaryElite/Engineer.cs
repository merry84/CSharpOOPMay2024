using MilitaryElite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private readonly List<IRepair> repairs;
        public IReadOnlyCollection<IRepair> Repairs => repairs.AsReadOnly();
        public Engineer(int id, string firstName, string lastName, decimal salary, string corps) 
            : base(id, firstName, lastName, salary, corps)
        {
            repairs = new List<IRepair>();
        }
        public void AddRepair(IRepair repair)=> repairs.Add(repair);

        public override string ToString()
        {
            return base.ToString() + $"\nRepairs:\n  {string.Join("\n  ",Repairs)}";
        }

    }
}
