using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int convertionCapacityIndex;
        private List<int> interfaceStandards;

        protected Robot(string model, int batteryCapacity, int convertionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            this.batteryLevel = batteryCapacity;
            this.convertionCapacityIndex = convertionCapacityIndex;
            interfaceStandards = new List<int>();
        }

        public string Model
        {
            get
           => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.ModelNullOrWhitespace);
                }
                model = value;
            }
        }

        public int BatteryCapacity
        {
            get
            => batteryCapacity;
            private set
            {
                if (value < 0)
                {
                    string.Format(ExceptionMessages.BatteryCapacityBelowZero);
                }
                batteryCapacity = value;
            }
        }
        //The current level of the battery. When creating a new Robot, set its initial value, equal to the BatteryCapacity.
        public int BatteryLevel => batteryLevel;

        public int ConvertionCapacityIndex => convertionCapacityIndex;

        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards.AsReadOnly();

        public virtual void Eating(int minutes)
        {
            /*The Robot will be in fluidization mode, so it will convert the food into electrical energy. For every minute of eating,
            it will produce energy equal to the ConvertionCapacityIndex multiplied by the given minutes.
             The Eating() method increases the Robot’s BatteryLevel, with the produced energy.
             If the battery is fully charged (BatteryLevel = BatteryCapacity), the eating stops earlier.*/
            int produceEnergy = minutes * convertionCapacityIndex;
            if (produceEnergy > BatteryCapacity - BatteryLevel)
            {
                batteryLevel = batteryCapacity;
            }
            else
            {
                batteryLevel += produceEnergy;
            }

        }

        public bool ExecuteService(int consumedEnergy)
        {
            /*The ExecuteService() method decreases the Robot’s BatteryLevel, with the given amount of consumed
              energy.
               If the BatteryLevel is equal or greater than the given consumedEnergy, decrease the BatteryLevel
              with the given amount of consumedEnergy and return True.
               If the BatteryLevel is less than the given consumedEnergy, it means that it is NOT enough. Skip the
              execution and return False*/
            if(batteryLevel>= consumedEnergy)
            {
                batteryLevel-=consumedEnergy;
                return true;
            }
            return false;
        }

        public void InstallSupplement(ISupplement supplement)
        {
            /*The InstallSupplemet() method takes the given supplement’s InterfaceStandard and adds it to the
           list of InterfaceStandards of the Robot.
            Decreases the BatteryCapacity of the robot by the BatteryUsage of the supplement.
            Decreases the BatteryLevel of the robot by the BatteryUsage of the supplement.*/
            BatteryCapacity -= supplement.BatteryUsage;
            batteryLevel -= supplement.BatteryUsage;
            interfaceStandards.Add(supplement.InterfaceStandard);

        }
        public override string ToString()
        {
            /*Override the existing method ToString() and modify it, so the returned string must be in the following format:
            "{robotTypeName} {Model}:
            --Maximum battery capacity: {BatteryCapacity}
            --Current battery level: {BatteryLevel}
            --Supplements installed: {standard1} {standard2}…/none"
            */
            var sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            sb.Append("--Supplements installed: ");
            if (InterfaceStandards.Count == 0)
            {
                sb.Append("none");
            }
            else 
            {
                sb.Append(string.Join(" ",InterfaceStandards));
            }
            return sb.ToString().TrimEnd();
        }
    }
}
