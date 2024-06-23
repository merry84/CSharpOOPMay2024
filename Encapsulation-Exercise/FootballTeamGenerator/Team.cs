using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Team
    {
        /*A Team should expose a name, a rating (calculated by the average skill level of all players in the team and rounded to the integer part only),
         * and methods for adding and removing players.Your task is to model the Team and the Player classes following the proper principles of Encapsulation.
         * Expose only the properties that need to be visible and validate data appropriately.
         */
        private string name;
        private List<Player> players = new List<Player>();

        public Team(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("A name should not be empty.");
                }
                name = value;
            }
        }
        public int Rating()
            => (int)Math.Round(CalculateRating());

        public double CalculateRating()
        {
            if (!players.Any())
            { return 0; }
            return players.Sum(x=>x.AverageStats)/players.Count;
        }
        public IReadOnlyList<Player> Players=> players.AsReadOnly();
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }
        //If you receive a command to remove a missing Player, print "Player [Player name] is not in [Team name] team."
        public void RemovePlayer(string playerName)
        {
           Player player = players.FirstOrDefault(x => x.Name == playerName);
            if (player == null) 
            {
                Console.WriteLine($"Player {playerName} is not in {Name} team.");
                    return;
            }
                players.Remove(player);
        }
        public override string ToString()
        {
            return $"{Name} - {Rating()}";
        }
    }
}
