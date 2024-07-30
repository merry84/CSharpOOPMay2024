using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class IndustrialAssistant : Robot
    {
        /*Has BatteryCapacity of 40 000 mAh.
        The IndustrialAssistant will produce a capacity of 5000 mAh of energy for every minute of eating -
        (convertionCapacityIndex = 5 000).*/
        private const int batteryCapacity = 40000;
        private const int convertionCapacityIndex = 5000;
        public IndustrialAssistant(string model) 
            : base(model, batteryCapacity, convertionCapacityIndex)
        {
        }
    }
}
