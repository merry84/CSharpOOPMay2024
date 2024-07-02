
using MilitaryElite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private readonly List<IPrivate> privates;
        public IReadOnlyCollection<IPrivate>Privates => privates.AsReadOnly();
        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary)
            : base(id, firstName, lastName, salary)
        {
            privates = new List<IPrivate>();

        }
        public void AddPrivate(IPrivate priv)
        => privates.Add(priv);
        public override string ToString()
        {
            return base.ToString() + $"\nPrivates:\n  {string.Join("\n  ",Privates)}";
        }


    }
}
