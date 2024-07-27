using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Client : IClient
    {
        private string name;
        private string id;
        private int interest;
        private double income;

        protected Client(string name, string id, int interest, double income)
        {
            Name = name;
            Id = id;
            Interest = interest;
            Income = income;
        }


        //o	If the Name is null or whitespace, throw a new ArgumentException with the message: 
        //Client name cannot be null or empty."
        public string Name
        {
            get
            => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.ClientNameNullOrWhitespace);
                }
                name = value;
            }
        }

        public string Id
        {
            get => id;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.ClientIdNullOrWhitespace);
                }
                id = value;
            }
        }
        
        //The client’s interest.Be careful with the access modifier.
        public int Interest
        {
            get=> interest;
            protected set
            {
                interest = value;
            }
        }
       
        //  The client’s income
        //o If the income is below or equal to 0, throw an ArgumentException with the message:
        //"Income cannot be below or equal to 0."
        public double Income
        {
            get
            =>income;
            private set
            {
                if (value <= 0) 
                {
                    string.Format(ExceptionMessages.ClientIncomeBelowZero);
                }
                income = value;
            }
        }
        //The IncreaseInterest() is an abstract method that increases the client’s interest.
        //Keep in mind that the child classes of Client will implement the method differently.
        public abstract void IncreaseInterest();
        
    }
}
