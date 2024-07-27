using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        //The mortgage loan has an interest rate of 3 and an amount of 50 000.
        //Note: The Constructor should take no values upon initialization.

        private const int interestRate = 3;
        private const double amount = 50000;
        public MortgageLoan() : base(interestRate, amount)
        {
        }
    }
}
