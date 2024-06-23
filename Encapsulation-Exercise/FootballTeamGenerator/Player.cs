using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Player
    {
        /*A Player has a name and stats, which are the basis for his skill level. The stats a player has are endurance, sprint, dribble, passing, and shooting.
         * Each stat can be an integer in the range [0..100]. The overall skill level of a player is calculated as the average of his stats. 
         * Only the name of a player and his stats should be visible to the entire outside world. Everything else should be hidden.*/
        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
        }
        //•	A name cannot be null, empty, or white space. If not, print "A name should not be empty."
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
        //•	Stats should be in the range 0..100. If not, print "[Stat name] should be between 0 and 100."
        public int Endurance
        {
            get => endurance;
            set
            {
                if (OutTheRange(value))
                {
                    throw new ArgumentException("Endurance should be between 0 and 100.");
                }
                endurance = value;
            }
        }
        public int Sprint
        {
            get
            => sprint;
            set
            {
                if (OutTheRange(value))
                {
                    throw new ArgumentException("Sprint should be between 0 and 100.");
                }
                sprint = value;
            }
        }
        public int Dribble
        {
            get => dribble;
            set
            {
                if (OutTheRange(value))
                {
                    throw new ArgumentException("Dribble should be between 0 and 100.");
                }
                dribble = value;
            }
        }
        public int Passing
        {
            get => passing;
            set
            {

                if (OutTheRange(value))
                {
                    throw new ArgumentException("Passing should be between 0 and 100.");
                }
                passing = value;

            }
        }
        public int Shooting
        {
            get => shooting;
            set
            {

                if (OutTheRange(value))
                {
                    throw new ArgumentException("Shooting should be between 0 and 100.");
                }
                shooting = value;

            }
        }

        public double AverageStats
            => (Endurance + Sprint + Dribble + Passing + Shooting) / 5.00;
        public bool OutTheRange(int value)
            => value < 0 || value > 100;
    }
}
