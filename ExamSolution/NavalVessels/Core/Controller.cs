using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Repositories.Contracts;
using NavalVessels.Utilities.Messages;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        /*•	vessels - VesselRepository
           •	captains - a collection of ICaptain
           */
        private IRepository<IVessel> vessels;
        private readonly List<ICaptain> captains;

        public Controller()
        {
            vessels = new VesselRepository();
            captains = new List<ICaptain>();
        }
        public string HireCaptain(string fullName)
        {
            /*Creates a captain with the provided full name and adds him/her to the collection of captains. The method should return one of the following messages:
               •	If the captain is hired successfully return: "Captain {fullName} is hired." and add him/her to the collection of captains.
               •	If a captain with the given name already exists return: "Captain {fullName} is already hired.", and the given captain should not be hired.
               */
            ICaptain captain = captains.FirstOrDefault(x => x.FullName == fullName);
            if (captain != null)
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }
            captains.Add(new Captain(fullName));
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            /*Creates a Vessel of the given type (Submarine or Battleship) with a given name, main weapon caliber, and speed points.
             The method should return one of the following messages:
               •	If the vessel with the given name exists return: "{typeVessel} vessel {name} is already manufactured."
               •	If the vesselType is invalid return: "Invalid vessel type."
               •	If the vessel is successfully produced return: 
            "{typeVessel} {name} is manufactured with the main weapon caliber of {mainWeapon} inches and a maximum speed of {speed} knots."
            and adds the vessel to the VesselRepository.
               */
            IVessel vessel = vessels.FindByName(name);
            if (vessel != null)
            {
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);
            }

            if (vesselType == nameof(Submarine))
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == nameof(Battleship))
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else
            {
                return string.Format(OutputMessages.InvalidVesselType);
            }
            vessels.Add(vessel);
            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            /*Searches for a captain and vessel by given names.
               As a result, the command returns one of the following messages: 
               •	If the captain does not exist return: "Captain {selectedCaptainName} could not be found."
               •	If the vessel does not exist return: "Vessel {selectedVesselName} could not be found."
               •	If the vessel has a captain return: "Vessel {selectedVesselName} is already occupied."
               •	If the captain is successfully assigned to the vessel return: "Captain {selectedCaptainName} command vessel {selectedVesselName}." 
            and add the vessel to the captain's list of vessels and set the vessel's captain to the selectedCaptainFullName
               NOTE: Follow the exact order of messages
               */
            ICaptain captain = captains.FirstOrDefault(x => x.FullName == selectedCaptainName);
            IVessel vessel = vessels.FindByName(selectedVesselName);
            if (captain == null)
            {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }

            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }

            if (vessel.Captain != null)
            {
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }
            captain.AddVessel(vessel);
            vessel.Captain = captain;
            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string CaptainReport(string captainFullName)
        {
            //Searches for an assigned captain with a given name and returns the ICaptain.Report() method result.
            return captains.FirstOrDefault(x => x.FullName == captainFullName)?.Report();
        }

        public string VesselReport(string vesselName)
        {
            //Searches for an existing vessel with a given name and returns ToString() method result.
            return vessels.FindByName(vesselName).ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            /*Searches for a vessel with a given name and toggles its special mode. As a result, the command returns one of the following messages:
               •	If the vessel is a battleship and does exist, execute ToggleSonarMode() and return: "Battleship {name} toggled sonar mode."
               •	If the vessel is submarine and does exist, execute ToggleSubmergeMode() and return:  "Submarine {name} toggled submerge mode."
               •	If the vessel does not exist return: "Vessel {name} could not be found."
               */
            IVessel vessel = vessels.FindByName(vesselName);


            if (vessel is ISubmarine)
            {
                ISubmarine submarine = (ISubmarine)vessel;
                submarine.ToggleSubmergeMode();
                return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
            }
            if (vessel is IBattleship)
            {
                IBattleship battleship = (IBattleship)vessel;
                battleship.ToggleSonarMode();
                return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
            }
            //If the vessel does not exist return: "Vessel {name} could not be found."
            return string.Format(OutputMessages.VesselNotFound, vesselName);

        }


        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            /*Searches for two vessels by given names and the first one attacks the second one. As a result, the command returns one of the following messages:
               •	If one of the vessels doesn't exist, the attacking vessel is with priority return: "Vessel {name} could not be found." 
               •	If one of the vessels has armor thickness equal to zero, the attacking vessel is with priority return:
            "Unarmored vessel {name} cannot attack or be attacked."
               •	If all the criteria are matched invoke the attacking vessel Attack() method, increase the combat experience of both vessel's captains and return:
                "Vessel {defendingVessleName} was attacked by vessel {attackVessleName} - current armor thickness: {defenderArmorThinckness}."
               NOTE: Both the attacking vessel and the defending vessel will always have captains
               */
            IVessel firstVessel = vessels.FindByName(attackingVesselName);
            IVessel secondVessel = vessels.FindByName(defendingVesselName);
            if (firstVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound,attackingVesselName);
            }

            if (secondVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, defendingVesselName);
            }

            if (firstVessel.ArmorThickness == 0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }

            if (secondVessel.ArmorThickness == 0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);
            }
            firstVessel.Attack(secondVessel);
            firstVessel.Captain.IncreaseCombatExperience();
            secondVessel.Captain.IncreaseCombatExperience();
            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName,
                secondVessel.ArmorThickness);
        }

        public string ServiceVessel(string vesselName)
        {
            /*Search for a vessel with the given name and invoke its RepairVessel() method.  As a result, the command returns one of the following messages:
               •	If the vessel is successfully repaired return:  "Vessel {name} was repaired."
               •	If the vessel does not exist return: "Vessel {name} could not be found."
               */;
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound,vesselName);
            }
            vessel.RepairVessel();
            return string.Format(OutputMessages.SuccessfullyRepairVessel,vesselName);
        }
    }
}
