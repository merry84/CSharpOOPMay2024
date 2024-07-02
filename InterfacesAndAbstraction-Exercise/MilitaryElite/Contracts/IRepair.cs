using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Contracts
{
    public interface IRepair
    {
        string PartName { get; }
        int HourWorked { get; }
    }
}
