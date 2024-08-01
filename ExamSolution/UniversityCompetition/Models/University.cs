using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class University :IUniversity
    {
        private int id;
        private string name;
        private string category;
        private int  capacity;
        private  List <int> requiredSubjects;

        public University(int id, string name, string category, int capacity,
            List<int> requiredSubjects
        )
        {
            
            Id = id;
            Name = name;
            Category = category;
            Capacity = capacity;
            this.requiredSubjects = requiredSubjects;
        }
        public int Id
        {
            get=>id;
            private set
            {
                id=value;
            }
        }

        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.NameNullOrWhitespace);
                }
                name=value;
            }
        }

        public string Category
        {
            get=>category;
            private set
            {
                //•	Category – string available categories are: Technical, Economical, Humanity
                if (value != "Technical" && value != "Economical" && value != "Humanity")
                {
                    string.Format(ExceptionMessages.CategoryNotAllowed, value);
                }
                category=value;
            }
        }

        public int Capacity
        {
            get=>capacity;
            private set
            {
                if (value < 0)
                {
                    string.Format((ExceptionMessages.CapacityNegative));
                }
                capacity=value;
            }
        }

        public IReadOnlyCollection<int> RequiredSubjects => this.requiredSubjects;
    }
}
