using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public class TeamLead :TeamMember
    {
        private const string path = "Master";
        public TeamLead(string name) : base(name, path)
        {
            Path = path;
        }

        public string Path
        {
            get => path;
            private set
            {
                if (value != "Master")
                {
                    string.Format(ExceptionMessages.PathIncorrect,value);
                }
                value = path;
                    
            }
        }

        public override string ToString()
            => $"{Name} ({nameof(TeamLead)}) – Currently working on {InProgress.Count} tasks.";
    }
}
