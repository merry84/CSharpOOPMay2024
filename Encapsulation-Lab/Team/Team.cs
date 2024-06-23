
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfo
{
    public class Team
    {
        private string name;
        private List<Person> firstTeam;
        private List<Person> reserveTeam;

        public string Name { get; private set; }
        public IReadOnlyList<Person> FirstTeam => firstTeam.AsReadOnly();
        public IReadOnlyList<Person> ReserveTeam => reserveTeam.AsReadOnly();
        public Team(string name)
        {
            Name = name;
            firstTeam = new List<Person>();
            reserveTeam = new List<Person>();
        }
        public void AddPlayer(Person person)
        {
            if (person.Age > 40)
            {
                reserveTeam.Add(person);
            }
            else
            {
                firstTeam.Add(person);
            }
        }
        public override string ToString()
        {
            return $"First team has {firstTeam.Count} players.\r\nReserve team has {reserveTeam.Count} players.";
        }

    }
}
