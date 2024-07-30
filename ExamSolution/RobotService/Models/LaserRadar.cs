using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class LaserRadar : Supplement
    {
        //A LaserRadar has an InterfaceStandard of 20082 and a BatteryUsage of 5 000 mAh.
        private const int interfaceStandard = 20082;
        private const int batteryUsage = 5000;
        public LaserRadar()
            : base(interfaceStandard, batteryUsage)
        {
        }
    }
}
