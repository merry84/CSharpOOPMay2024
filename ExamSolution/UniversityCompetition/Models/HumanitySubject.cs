using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCompetition.Models
{
    public class HumanitySubject :Subject
    {
        //HumanitySubject has a constant value for subjectRate = 1.15
        private const double HumanityRate = 1.15;
        public HumanitySubject(int id, string name)
            : base(id, name, HumanityRate)
        {
        }
    }
}
