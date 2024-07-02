using MilitaryElite.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Repair :IRepair
    {
        public Repair(string partName,int hourWorked)
        {
            this.PartName = partName;
            this.HourWorked = hourWorked;
        }

      public  string PartName { get; private set; }
        public int HourWorked { get;private set; }

        public override string ToString()
        {
            return $"{PartName} - {HourWorked}";
        }

    }
}
