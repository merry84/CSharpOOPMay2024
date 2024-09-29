using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public class ContentMember :TeamMember
    {
        private string path;
        public ContentMember(string name, string path) : base(name, path)
        {
            Path = path;
        }

        public string Path
        {
            get => path;
            private set
            {
                if (value != "CSharp" ||
                    value != "JavaScript"
                    || value != "Python"
                    || value != "Java")
                    string.Format(ExceptionMessages.PathIncorrect, value);
                path = value;
            }
        }
        public override string ToString()
            => $"{Name} ({nameof(ContentMember)}) – Currently working on {InProgress.Count} tasks.";
    }
}
