﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class Student : Client
    {
        //Has an initial interest of 2 percent.
        private const int interest = 2;
        public Student(string name, string id, double income)
            : base(name, id, interest, income)
        {
        }

        public override void IncreaseInterest()
        {
            //•	The method increases the client’s interest by 1 percent.
            base.Interest++;
        }
    }
}
