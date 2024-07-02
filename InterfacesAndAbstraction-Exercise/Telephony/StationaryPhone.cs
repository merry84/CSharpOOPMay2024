using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class StationaryPhone : ICalling
    {
        public StationaryPhone() { }
        public void Calling(string phoneNumber)
        {
            /*•	If there is a character different from a digit in a number, 
             * print: "Invalid number!" and continue with the next number.*/

            if (!CallingIsDigit(phoneNumber))
            {
                Console.WriteLine("Invalid number!");
                return;
            }
            Console.WriteLine($"Dialing... {phoneNumber}");

        }
        private bool CallingIsDigit(string number)
            => number.All(x => char.IsDigit(x));
    }
}
