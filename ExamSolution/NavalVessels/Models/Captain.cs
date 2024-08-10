using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{

    public class Captain : ICaptain

    {
        private string fullName;
        private List<IVessel> vessels;
        private int combatExperiance;
        public Captain(string fullName)
        {
            FullName = fullName;
            vessels = new List<IVessel>();
            CombatExperience = 0;
        }
        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.InvalidCaptainName);
                }
                fullName = value;
            }
        }

        public int CombatExperience
        {
            get => combatExperiance;
            private set
            {
                combatExperiance= value;
            }
        }

        public ICollection<IVessel> Vessels => vessels.AsReadOnly();

        public void AddVessel(IVessel vessel)
        {
            /*Adds the provided vessel to the captain’s vessels. 
             * If the provided vessel is null throw NullReferenceException with the message: "Null vessel cannot be added to the captain."*/
            if (vessel == null)
            {
                string.Format(ExceptionMessages.InvalidVesselForCaptain);
            }
            vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            /*Increase the captain’s combat experience by 10 when a vessel that he commands attack or defend. 
             * There will be no case where the attacking vessel and the defending vessel will have the same captain. */
            CombatExperience += 10;
        }

        public string Report()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {Vessels.Count} vessels.");
            vessels.ForEach(v => report.AppendLine(v.ToString()));
            return report.ToString().TrimEnd();
        }
    }
}
