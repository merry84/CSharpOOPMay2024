using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding
{
    public class Paladin : BaseHero
    {
        //	Paladin – power = 100
        private const int powerPaladin = 100;
        public Paladin(string name) : base(name, powerPaladin)
        {
        }

        public override string CastAbility()
        =>$"{GetType().Name} - {Name} healed for {powerPaladin}";
    }
}
