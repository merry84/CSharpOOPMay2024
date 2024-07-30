using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models.Contracts;

namespace RobotService.Models
{
    public abstract class Supplement : ISupplement
    {
        private int interfaceStandard;
        private int batteryUsage;

        protected Supplement(int interfaceStandard, int batteryUsage)
        {
            this.interfaceStandard = interfaceStandard;
            this.batteryUsage = batteryUsage;
        }

        public int InterfaceStandard
        {
            get
            {
                return interfaceStandard;
            }
        }

        public int BatteryUsage
        {
            get
            {
                return batteryUsage;
            }
        }
    }
}
