using MilitaryElite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        private static readonly HashSet<string> validStates = new HashSet<string>
        {
            "inProgress","Finished"
        };
        private readonly List<IMission> missions;

        public Commando(int id, string firstName, string lastName, decimal salary, string corps) 
            : base(id, firstName, lastName, salary, corps)
        {
            missions = new List<IMission>();
        }

        public IReadOnlyCollection<IMission> Missions => missions.AsReadOnly();

        public void AddMission(IMission mission)
        {
            if (validStates.Contains(mission.State))
            {
                missions.Add(mission);
            }
        }
        public override string ToString()
        {
            return base.ToString() + $"\nMission:\n  {string.Join("\n  ",Missions)}";
        }


    }
}
