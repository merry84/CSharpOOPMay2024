using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class SpecializedArm : Supplement
    {
        /*A SpecializedArm has an InterfaceStandard of 10045 and a BatteryUsage of 10 000 mAh.
            Note: The Constructor should take no values upon initialization.*/
        private const int interfaceStandard = 10045;
        private const int batteryUsage = 10000;
        public SpecializedArm()
            : base(interfaceStandard, batteryUsage)
        {
        }
    }
}
