using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class DomesticAssistant : Robot
    {
        //Has BatteryCapacity of 20 000 mAh.
        //  The DomesticAssistant will produce a capacity of 2000 mAh of energy for every minute of eating -
        //(convertionCapacityIndex = 2 000).
        private const int batteryCapacity = 20000;
        private const int convertionCapacityIndex = 2000;
        public DomesticAssistant(string model)
            : base(model, batteryCapacity, convertionCapacityIndex)
        {
        }
    }
}
