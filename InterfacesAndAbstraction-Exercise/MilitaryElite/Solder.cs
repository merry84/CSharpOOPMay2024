using MilitaryElite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Solder : ISolder
    {
        public Solder(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; set ; }
        public string FirstName { get ; set; }
        public string LastName { get; set ; }
        public override string ToString()
       => $"Name {FirstName} {LastName} Id: {Id}";
    }
}
