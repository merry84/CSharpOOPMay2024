using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class SmartPhone : ICalling, IBrowsing
    {
        public SmartPhone() { }
        public void BrowsingURl(string url)
        {
            /*•	If there is a number in the input of the URLs, print: "Invalid URL!" 
             * and continue with the next URLs.
            •	If the URL is valid, print on the console the website in the format: "Browsing: {site}!"
            */
            if (!BrowsingUrl(url))
            {
                Console.WriteLine("Invalid URL!");
                return;
            }
            Console.WriteLine($"Browsing: {url}!");
        }

        public void Calling(string phoneNumber)
        {
            /*•	If there is a character different from a digit in a number, print: "Invalid number!" 
             * and continue with the next number.   */
            if (!CallingIsDigit(phoneNumber))
            {
                Console.WriteLine("Invalid number!");
                return;
            }
            Console.WriteLine($"Calling... {phoneNumber}");
        }
        private bool CallingIsDigit(string number)
           => number.All(x => char.IsDigit(x));
        //•	Each site's URL should consist only of letters and symbols (No digits are allowed in the URL address).
        private bool BrowsingUrl(string url)
          => url.All(x => !char.IsDigit(x));
    }
}
