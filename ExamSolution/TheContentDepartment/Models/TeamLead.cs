using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public class TeamLead : TeamMember
    {
        public TeamLead(string name, string path) 
            : base(name, path)
        {
            /*Is only allowed to have a value of Path property: "Master"
            •	Check the second parameter path: 
            o	If the path value does not match the TeamLead validations, 
            a new ArgumentException is thrown. The following message should be returned: "{path} path is not valid."
            */
            if (path != "Master")
            {
                string.Format(ExceptionMessages.PathIncorrect, path);
            }
        }
        public override string ToString()
       => $"{Name} ({GetType().Name}) – Currently working on {InProgress.Count} tasks.";
 //return $"{Name} ({GetType().Name}) - Currently working on {InProgress.Count} tasks.";
    }
}
