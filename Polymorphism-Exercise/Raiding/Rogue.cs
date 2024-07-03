using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding
{
    public class Rogue : BaseHero
    {
        //	Rogue – power = 80
        private const int powerRogue = 80;
        public Rogue(string name) : base(name, powerRogue)
        {
        }

        public override string CastAbility()
        => $"{GetType().Name} - {Name} hit for {powerRogue} damage";
    }
}
