using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.Models.Contracts;
using FootballManager.Utilities.Messages;

namespace FootballManager.Models
{
    public class Team :ITeam
    {
        private string name;
        private int championshipPoints;

        public Team(string name)
        {
            Name = name;
            ChampionshipPoints = 0;
        }
        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }
                name = value;
            }
        }

        public int ChampionshipPoints
        {
            get=>championshipPoints;
            private set
            {
                championshipPoints = value;
            }
        }
        public IManager TeamManager { get; private set; }

        /*o	A calculated property that returns the present condition of the team, which is the result of multiplying ChampionshipPoints and TeamManager.
         Ranking, floored to the nearest integer
           o	If the team has no manager, return 0.
           o	If the team has no points, return the manager's Ranking value, floored to the nearest integer
           o	Example: 10 championship points * 18.75 manager ranking = 188 units present condition
           */
        public int PresentCondition {
            get
            {
                if (TeamManager == null)
                {
                    return 0;
                }

                if (ChampionshipPoints == 0)
                {
                    return (int)Math.Floor(TeamManager.Ranking);
                }

                return (int)Math.Floor(ChampionshipPoints * TeamManager.Ranking);
            }
        }
        public void SignWith(IManager manager)
        {
            TeamManager = manager;
        }

        public void GainPoints(int points)
            => ChampionshipPoints += points;

        public void ResetPoints()
            => ChampionshipPoints = 0;

        public override string ToString()
            => $"Team: {Name} Points: {ChampionshipPoints}";
    }
}
