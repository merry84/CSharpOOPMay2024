using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding
{
    public abstract class BaseHero
    {
        //string Name, int Power, string CastAbility()
        private string name;
        private int power;
     

        protected BaseHero(string name, int power)
        {
            Name = name;
            Power = power;
        }

        public string Name 
        { get => name;
            set => name = value; }
        public int Power 
        { get => power;
            set => power = value; }

        public abstract string CastAbility();
    }
}
