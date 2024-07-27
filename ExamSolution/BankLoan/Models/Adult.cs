using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class Adult : Client
    {
        /*Has an initial interest of 4 percent.*/
        private const int interest = 4;
        public Adult(string name, string id,double income)
            : base(name, id, interest, income)
        {
        }

        public override void IncreaseInterest()
        {
            //•	The method increases the client’s interest by 2 percent.
            base.Interest += 2;
        }
    }
}
