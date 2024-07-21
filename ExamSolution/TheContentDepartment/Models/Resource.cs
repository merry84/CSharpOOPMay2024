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
        //A Resource should take the following values upon initialization: 
        //string name, string creator, int priority

        private string name;
        private string creator;
        private int priority;
        private bool isTested;
        private bool isApproved;

        protected Resource(string name, string creator, int priority)
        {
            Name = name;
            Creator = creator;
            Priority = priority;
            isTested = false;
            isApproved = false;
        }

        public string Name
        {
            get
            => name;
            private set
            {
                /*o	If the Name is null or whitespace, throw a new ArgumentException with the message: 
                "Name cannot be null or whitespace."
                */
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.NameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public string Creator
        {
            get
            => creator;
            private set
            {
                creator = value;
            }
        }

        public int Priority
        {
            get => priority;
            private set
            { priority = value; }

        }

        public bool IsTested => isTested;

        public bool IsApproved => isApproved;

        public void Approve()
        {
            /*Sets the IsApproved flag to true,
             * indicating that the resource has passed all necessary checks and is approved for use or publication.*/
            isApproved = true;
        }

        public void Test()
        {
            /*Toggles the IsTested status of the resource.
             * This method allows the testing status to be changed,
             * reflecting the resource's progression through the testing phase.*/
            isTested = !isTested;
        }
        public override string ToString()
        => $"{Name} ({GetType().Name}), Created By: {Creator}";
 // return $"{Name} ({GetType().Name}), Created By: {Creator}";

    }
}
