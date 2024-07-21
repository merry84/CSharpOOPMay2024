using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber
    {
        private string name;
        private int stamina;
        private readonly List<string> conqueredPeaks;

        protected Climber(string name, int stamina)
        {
            Name = name;
            Stamina = stamina;
            conqueredPeaks = new List<string>();
        }

        public string Name
        {
            get
            => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClimberNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Stamina
        {
            get
            => stamina;
            protected set
            {
                if (value < 0)
                {
                    stamina = 0;
                }
                else if (value > 10)
                { 
                    stamina = 10;
                }
                else
                {

                    stamina = value;
                }
            }
        }

        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks.AsReadOnly();

        public void Climb(IPeak peak)
        {
            
            if (!ConqueredPeaks.Contains(peak.Name))
            {
                conqueredPeaks.Add(peak.Name);
            }
            int currentStamina = 0;
            if(peak.DifficultyLevel == "Extreme")
            {
                currentStamina += 6;
            }
            else if(peak.DifficultyLevel == "Hard")
            {
                currentStamina += 4;
            }
            else { currentStamina += 2; }

            Stamina -= currentStamina;           
        }

        public abstract void Rest(int daysCount);
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name} - Name: {Name}, Stamina: {Stamina}");
            sb.Append("Peaks conquered: ");

            if (conqueredPeaks.Count >0 )
            {
                sb.AppendLine($"{ConqueredPeaks.Count}");
            }
            else
            {
                sb.AppendLine("no peaks conquered");
            }
            return sb.ToString().Trim();
        }

    }
}
