using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private IRepository<IDiver> divers;
        private IRepository<IFish> fishes;
        public Controller()
        {
            divers = new DiverRepository();
            fishes = new FishRepository();
        }
        public string ChaseFish(string diverName, string fishName, bool isLucky)//!?
        {
            /*•	Validates if a diver with the given diverName exists in the DiverRepository.
             * If no diver with the provided name is found, return the following message:
             * "{correctRepositoryTypeName} has no {diverName} registered for the competition."
             •	Validates if a fish with the given fishName exists in the FishRepository. 
            If no fish with the provided name is found, return the following message: 
            "{fishName} is not allowed to be caught in this competition."
             •	HealthCheck - If the diver's HasHealthIssues property is True, the diver is not allowed to dive. 
            //Return the following message: "{diverName} will not be allowed to dive, due to health issues."
            // •	If the diver’s OxygenLevel is less than the fish's TimeToCatch value, the fish will escape, 
            the diver will Miss with the harpoon (method Miss(int timeToCatch) should be used),
            and the following message should be returned: "{diverName} missed a good {fishName}."
             •	If the diver's OxygenLevel is equal to the fish's TimeToCatch value then:
             //o	If isLucky is True, the diver successfully catches the fish by invoking the Hit method with the targeted fish. 
            The following message is returned: "{diverName} hits a {fishPoints}pt. {fishName}."
             o	If isLucky is False, the diver misses the fish. Invoke the Miss method with the time to catch of the targeted fish. 
            The following message is returned: "{diverName} missed a good {fishName}."
             •	If the diver’s OxygenLevel is more than the fish's TimeToCatch value, the fish is caught,
            the diver will Hit with the harpoon, and the following message should be returned: 
            "{diverName} hits a {fishPoints}pt. {fishName}."
             •	Please note that if, at any point during the chase, 
            the diver's OxygenLevel drops to 0, the diver's HasHealthIssues property is set to True.
             */
            if (divers.GetModel(diverName) == null)
            {
                return string.Format(OutputMessages.DiverNotFound, nameof(DiverRepository), diverName);
            }
            if (fishes.GetModel(fishName) == null)
            {
                return string.Format(OutputMessages.FishNotAllowed, fishName);
            }
            IDiver diver = divers.GetModel(diverName);
            if (diver.HasHealthIssues)
            {
                return string.Format(OutputMessages.DiverHealthCheck, diverName);
            }
            IFish fish = fishes.GetModel(fishName);
            if (diver.OxygenLevel < fish.TimeToCatch)
            {
                diver.Miss(fish.TimeToCatch);
                if (diver.OxygenLevel <= 0)
                {
                    diver.UpdateHealthStatus();
                }
                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }
            if (diver.OxygenLevel == fish.TimeToCatch )
            {
                if (isLucky)
                {
                    diver.Hit(fish);
                    diver.UpdateHealthStatus();

                    return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);
                }

                diver.Miss(fish.TimeToCatch);
                if (diver.OxygenLevel <= 0)
                {
                    diver.UpdateHealthStatus();
                }
                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }

            diver.Hit(fish);
            if (diver.OxygenLevel <= 0)
            {
                diver.UpdateHealthStatus();
            }
            return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);

        }

        public string CompetitionStatistics()
        {
            /*Returns information about each diver from the DiverRepository. 
             * Arrange the divers by CompetitionPoints - descending, then by Catch.Count – descending, 
             * then by Name - alphabetically. Return only divers that are in good health condition. 
             * To receive the correct output, use the ToString() method of each diver:
            "**Nautical-Catch-Challenge**
            {diver1} 
            {diver2}
            ...
            {divern}"
            */
            var filtredDivers = divers.Models
                .Where(x => x.HasHealthIssues==false)
                .OrderByDescending(x => x.CompetitionPoints)
                .ThenByDescending(x => x.Catch.Count)
                .ThenBy(x => x.Name);
            var sb = new StringBuilder();
            sb.AppendLine("**Nautical-Catch-Challenge**");

            foreach(var diver in filtredDivers)
            {
                sb.AppendLine($"{diver.ToString()}");
            }
            return sb.ToString().TrimEnd();
        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {
            /*•	If the given diverType  is NOT presented as a valid Diver’s child class
             * (FreeDiver, ScubaDiver), return the following message: "{diverType} is not allowed in our competition."
       •	If a diver with the same Name is already added to the repository, do not duplicate records, 
            return the following message: "{diverName} is already a participant -> {correctRepositoryTypeName}."
       •	If none of the above cases is reached, the IDiver is successfully created.
            Store the diver to the appropriate collection and return: 
            "{diverName} is successfully registered for the competition -> {correctRepositoryTypeName}."
       */
            if (diverType != nameof(FreeDiver) && diverType != nameof(ScubaDiver))
            {
                return string.Format(OutputMessages.DiverTypeNotPresented, diverType);
            }

            else if (divers.GetModel(diverName) != null)
            {
                return string.Format(OutputMessages.DiverNameDuplication, diverName, nameof(DiverRepository));
            }
            else
            {
                IDiver diver;
                if (diverType == nameof(FreeDiver))
                {
                    diver = new FreeDiver(diverName);
                }
                else
                {
                    diver = new ScubaDiver(diverName);
                }

                divers.AddModel(diver);
                return string.Format(OutputMessages.DiverRegistered, diverName, nameof(DiverRepository));
            }


        }

        public string DiverCatchReport(string diverName)
        {
            /*Returns detailed information about a specific diver and his catch so far.
             * To receive the correct output, use the ToString() method of each fish:
            "Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {count}, Points earned: {CompetitionPoints} ]
            Catch Report:
            {fish1} //{typeName}: {Name} [ Points: {Points}, Time to Catch: {TimeToCatch} seconds ]
            {fish2}
            …
            {fishn}
            */
            var sb = new StringBuilder();
            IDiver diver = divers.GetModel(diverName);
            //"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {count}, Points earned: {CompetitionPoints} ]
            sb.AppendLine(diver.ToString());
            sb.AppendLine("Catch Report:");

            foreach (var fishName in diver.Catch)
            {
                var fish = fishes.GetModel(fishName);
                sb.AppendLine(fish.ToString());
            }
            return sb.ToString().Trim();   
        }

        public string HealthRecovery()
        {
            /*The method doesn't receive any parameters. 
             * Its main purpose is to scan through the collection of divers and identify those facing health issues:
            Once the method identifies a diver with the HasHealthIssues property set to True,
            it initiates a series of actions to stabilize the diver:
            •	First, it sets the HasHealthIssues property of the diver to False,
            indicating that the diver is now in a stable condition.
            •	Secondly, it replenishes the diver's OxygenLevel back to its maximum, 
            ensuring the divers are ready for the next dive when they feel comfortable.
            •	Returns the following message: "Divers recovered: {count}"
            */
            List<IDiver> unwellDivers = divers.Models.Where(d => d.HasHealthIssues).ToList();

            foreach (var diver in unwellDivers)
            {
                diver.UpdateHealthStatus();
                diver.RenewOxy();
            }

            return string.Format(OutputMessages.DiversRecovered, unwellDivers.Count);
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            /*The method should create and add a new IFish to the FishRepository. 
             * The SwimIntoCompetition method is responsible for allowing a new fish to chase into the competition.
             •	If the given typeName  is NOT presented as a valid Fish's child class 
            (ReefFish, DeepSeaFish, or PredatoryFish), return the following message: 
            "{fishType} is forbidden for chasing in our competition."
             •	If a fish with the same Name is already added to the repository, 
            do not duplicate records, return the following message: "{fishName} is already allowed -> {correctRepositoryTypeName}."
             •	If the above case is not reached,
            create the correct type of IFish and add it to the appropriate collection.
            Return the following message: "{fishName} is allowed for chasing."
             */
            if (fishType != nameof(ReefFish) && fishType != nameof(DeepSeaFish) && fishType != nameof(PredatoryFish))
            {
                return string.Format(OutputMessages.FishTypeNotPresented, fishType);
            }
            if (fishes.GetModel(fishName) != null)
            {
                return string.Format(OutputMessages.FishNameDuplication, fishName, nameof(FishRepository));
            }
            IFish fish;
            if (fishType == nameof(DeepSeaFish))
            {
                fish = new DeepSeaFish(fishName, points);
            }
            else if (fishType == nameof(PredatoryFish))
            {
                fish = new PredatoryFish(fishName, points);
            }
            else
            {
                fish = new ReefFish(fishName, points);
            }
            fishes.AddModel(fish);
            return string.Format(OutputMessages.FishCreated, fishName);

        }
    }
}
