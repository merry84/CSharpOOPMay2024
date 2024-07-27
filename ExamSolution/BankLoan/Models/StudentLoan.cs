using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class StudentLoan : Loan
    {
        //The student loan has an interest rate of 1 and an amount of 10 000.
        // Note: The Constructor should take no values upon initialization.
        private const int interestRate = 1;
        private const double amount = 10000.00;
        public StudentLoan()
            : base(interestRate, amount)
        {
        }
    }
}
