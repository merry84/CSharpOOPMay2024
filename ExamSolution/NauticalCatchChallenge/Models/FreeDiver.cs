using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class FreeDiver : Diver
    {
        private const int oxygenLevel = 120;
        private const double decresedOxigen = 0.6;
        public FreeDiver(string name) 
            : base(name, oxygenLevel)
        {
        }

        public override void Miss(int TimeToCatch)
        {
            /*FreeDiver will decrease the OxygenLevel property by 60% (using the Miss() method)
             * of the TimeToCatch value of the missed fish. 
            •	If the calculated value is not a whole number, round it to the nearest whole integer.
            //OxygenLevel -= (int) Math.Round(…, MidpointRounding.AwayFromZero);
            */
            int usedOxygen = (int)Math.Round(TimeToCatch * decresedOxigen);
            base.OxygenLevel -= usedOxygen;
           
        }

        public override void RenewOxy()
        {
            base.OxygenLevel = oxygenLevel;
        }
    }
}
