using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding
{
    public class Druid : BaseHero
    {
        //	Druid – power = 80
        private const int powerDruid = 80;
        public Druid(string name) : base(name, powerDruid)
        {
        }

        public override string CastAbility()

            => $"{GetType().Name} - {Name} healed for {powerDruid}";
            
        
    }
}
