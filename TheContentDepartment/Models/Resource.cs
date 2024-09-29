using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public abstract class Resource : IResource
    {
        private string name;
        private string creator;
        private int priority;
        protected Resource(string name, string creator, int priority)
        {
            Name = name;
            Creator = creator;
            Priority = priority;
            IsTested = false;
            IsApproved = false;

        }
        //o	If the Name is null or whitespace, throw a new ArgumentException with the message: 
        // "Name cannot be null or whitespace."
        // 
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    string.Format(ExceptionMessages.NameNullOrWhiteSpace);
                name = value;
            }
        }

        public string Creator
        {
            get => creator;
            private set
            {
                creator = value;
            }
        }

        public int Priority { get; protected set; }
        public bool IsTested { get; private set; }
        public bool IsApproved { get; private set; }

        public void Test()
            => IsTested=true;

        public void Approve()
        {
            IsApproved = true;
        }

        public override string ToString()
        =>$"{Name} ({typeof(Resource)}), Created By: {Creator}";
    }
}
