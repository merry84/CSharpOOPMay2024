using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    //140/150
    public class Controller : IController
    {
        //supplements - SupplementRepository
        // robots - RobotRepository
        private SupplementRepository supplements;
        private RobotRepository robots;

        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }
        public string CreateRobot(string model, string typeName)
        {
            /*The method should create and add a new IRobot to the RobotRepository.
             If the given typeName is NOT presented as a valid Robot’s child class (DomesticAssistant or
            IndustrialAssistant), return the following message: "Robot type {typeName} cannot be created."
             If the above case is NOT reached, create an IRobot from the valid child type and add it to the
            RobotRepository. Return the following message: "{typeName} {model} is created and added
            to the RobotRepository."  */
            if (typeName != nameof(DomesticAssistant) && typeName != nameof(IndustrialAssistant))
            {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }
            IRobot robot;
            if (typeName == nameof(DomesticAssistant))
            {
                robot = new DomesticAssistant(model);
            }
            else { robot = new IndustrialAssistant(model); }
            robots.AddNew(robot);
            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            /*The method should create and add a new ISupplement to the SupplementRepository.
             If the given typeName is NOT presented as a valid Supplement’s child class (SpecializedArm or
            LaserRadar), return the following message: "{typeName} is not compatible with our robots."
           
             If the above case is NOT reached, create a new ISupplement and add it to the SupplementRepository.
            Return the following message: "{typeName} is created and added to the
            SupplementRepository."
            */
            ISupplement supplement;
            if (typeName == nameof(SpecializedArm))
            {
                supplement = new SpecializedArm();
            }
            else if (typeName == nameof(LaserRadar))
            {
                supplement = new LaserRadar();
            }
            else
            {
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }
            supplements.AddNew(supplement);
            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);

        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {

            var robotFiltred = robots.Models()
                .Where(x => x.InterfaceStandards.Any(y => y == intefaceStandard))
                .OrderByDescending(y => y.BatteryLevel);
            if (robotFiltred.Count() == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }
            var sumBatteryLevel = robotFiltred.Sum(x => x.BatteryLevel);

            if (sumBatteryLevel < totalPowerNeeded)
            {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - sumBatteryLevel);
            }
            int countRobots = 0;
            foreach (var robot in robotFiltred)
            {
                countRobots++;
                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    break;
                }
                else
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                }
            }
            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, countRobots);

        }

        public string Report()
        {
            /*Report Command
          Functionality
          Returns information about each robot from the RobotRepository. Arrange the robots by BatteryLevel,
          descending, then by BatteryCapacity, ascending. In order to receive correct output, use the ToString() method
          of each robot:
          "{robot1}
          {robot2}
          ...
          {robotn}"*/
            var selectedRobots = robots.Models().OrderByDescending(x => x.BatteryLevel)
                .ThenBy(x => x.BatteryCapacity);
            var sb = new StringBuilder();
            foreach (var robot in selectedRobots)
            {
                sb.AppendLine(robot.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            /*Feed all robots in the RobotRepository from the given model for the given count of minutes. Choose only those
          robots that have BatteryLevel under 50% from the total BatteryCapacity.
          Remember that when feeding a robot, it will be in fluidization mode and it will convert food into energy. That means
          that after feeding, the robot’s BatteryLevel should be increased. Use the built-in Eating() method of each robot.
          Return a string with information about how many robots were successfully fed, in the following format:
           "Robots fed: {fedCount}"
          */
            var selectetRobots = robots.Models().Where(x => x.Model == model && x.BatteryLevel * 2 < x.BatteryCapacity);
            int fedCount = 0;

            foreach (var robot in selectetRobots)
            {
                robot.Eating(minutes);
                fedCount++;
            }
            return string.Format(OutputMessages.RobotsFed, fedCount);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            /*This method will upgrade a robot with a new supplement. There will always be at least one supplement from the
            correct type already added to the SupplementRepository. There will always be at least one robot from the given
            model already added to the RobotRepository:
            1. Find the first ISupplement with the given supplementTypeName in the SupplementRepository and
            take its interface value.
            2. From the RobotRepository, take only the robots, NOT supporting the interface value (check if every
            robot’s InterfaceStandards collection NOT containing the interface value).
            3. Select only the robots, from the given model (check if every robot’s Model is equal to the given model).
            4. If the collection is empty, that means all of the robots in the RobotRepository from the given model, are
            already upgraded with a Supplement from the given supplementTypeName,
            o return the following message: "All {model} are already upgraded!"
            5. If there are still not upgraded robots, take the first IRobot from the previous selected robots and use the
            built-in InstallSupplement() method to upgrade the robot with the new supplement.
            o Remove the ISupplement from the SupplementRepository.
            o Return the following message: "{model} is upgraded with {supplementTypeName}.*/

            var supplement = supplements.Models().FirstOrDefault(x => x.GetType().Name == supplementTypeName);
            var selectedModels = this.robots.Models().Where(r => r.Model == model);

            var stillNotUpgraded = selectedModels.Where(r => r.InterfaceStandards.All(s => s != supplement.InterfaceStandard));
            var robotForUpgrade = stillNotUpgraded.FirstOrDefault();

            if (robotForUpgrade == null) return string.Format(OutputMessages.AllModelsUpgraded, model);
            robotForUpgrade.InstallSupplement(supplement);
            robots.RemoveByName(supplementTypeName);
            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);

        }
    }
}
