using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public abstract class TeamMember :ITeamMember
    {
        private string name;
        private string path;
        private List<string> inProgress;

        public TeamMember(string name,string path)
        {
            Name = name;
            
            inProgress = new List<string>();

        }
        public string Name
        {
            get=>name;
            private set
            {
                //o	If the Name is null or whitespace, throw a new ArgumentException with the message: 
                // "Name cannot be null or whitespace."
                if (string.IsNullOrWhiteSpace(value))
                    string.Format(ExceptionMessages.NameNullOrWhiteSpace);
                name = value;
            }
        }

        public string Path=> path;
       

        public IReadOnlyCollection<string> InProgress => inProgress.AsReadOnly();

        public void WorkOnTask(string resourceName)
            => inProgress.Add(resourceName);

        public void FinishTask(string resourceName)
            => inProgress.Remove(resourceName);
    }
}
