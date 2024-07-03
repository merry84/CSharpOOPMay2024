using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding
{
    public class Warrior : BaseHero
    {
        //	Warrior – power = 100
        private const int powerWarrior = 100;
        public Warrior(string name) : base(name, powerWarrior)
        {
        }

        public override string CastAbility()
        =>$"{GetType().Name} - {Name} hit for {powerWarrior} damage";
    }
}
