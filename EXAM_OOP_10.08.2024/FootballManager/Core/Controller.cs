using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.Core.Contracts;
using FootballManager.Models;
using FootballManager.Models.Contracts;
using FootballManager.Repositories;
using FootballManager.Utilities.Messages;

namespace FootballManager.Core
{
    public class Controller : IController
    {
        private readonly TeamRepository championship;

        public Controller()
        {
            championship = new TeamRepository();
        }

        public string JoinChampionship(string teamName)
        {
            
            if (championship.Capacity <= championship.Models.Count)
            {
                return string.Format(OutputMessages.ChampionshipFull);
            }

            if (championship.Models.Any(x => x.Name == teamName))
            {
                return string.Format(OutputMessages.TeamWithSameNameExisting, teamName);
            }

            ITeam team = new Team(teamName);
            championship.Add(team);
            return string.Format(OutputMessages.TeamSuccessfullyJoined, teamName);
        }

        public string SignManager(string teamName, string managerTypeName, string managerName)
        {
           
            ITeam team = championship.Get(teamName);
            if (team == null)
            {
                return string.Format(OutputMessages.TeamDoesNotTakePart, teamName);

            }
            IManager manager;

            if (managerTypeName == "AmateurManager")

                manager = new AmateurManager(managerName);

            else if (managerTypeName == "SeniorManager")

                manager = new SeniorManager(managerName);

            else if (managerTypeName == "ProfessionalManager")

                manager = new ProfessionalManager(managerName);
            else
                return $"{managerTypeName} is an invalid manager type for the application.";


            if (team.TeamManager != null)
            {
                return string.Format(OutputMessages.TeamSignedWithAnotherManager, teamName, team.TeamManager.Name);
            }
            foreach (var currenTeam in championship.Models)
            {
                if (currenTeam.TeamManager != null && currenTeam.TeamManager.Name == managerName)
                {

                    return string.Format(OutputMessages.ManagerAssignedToAnotherTeam, managerName);
                }
            }
            team.SignWith(manager);
            return string.Format(OutputMessages.TeamSuccessfullySignedWithManager, managerName, teamName);

        }

        public string MatchBetween(string teamOneName, string teamTwoName)
        {
           
            ITeam teamOne = championship.Get(teamOneName);
            ITeam teamTwo = championship.Get(teamTwoName);

            if (teamOne == null || teamTwo == null)
            {
                return "This match does not meet the regulation rules of the Championship.";
            }

            int teamOneCondition = teamOne.PresentCondition;
            int teamTwoCondition = teamTwo.PresentCondition;

            if (teamOneCondition > teamTwoCondition)
            {
                teamOne.GainPoints(3);

                if (teamOne.TeamManager != null)
                {
                    teamOne.TeamManager.RankingUpdate(5);
                }

                if (teamTwo.TeamManager != null)
                {
                    teamTwo.TeamManager.RankingUpdate(-5);
                }

                return $"Team {teamOne.Name} wins the match against {teamTwo.Name}.";
            }
            else if (teamTwoCondition > teamOneCondition)
            {
                teamTwo.GainPoints(3);

                if (teamTwo.TeamManager != null)
                {
                    teamTwo.TeamManager.RankingUpdate(5);
                }

                if (teamOne.TeamManager != null)
                {
                    teamOne.TeamManager.RankingUpdate(-5);
                }

                return $"Team {teamTwo.Name} wins the match against {teamOne.Name}.";
            }
            else
            {
                teamOne.GainPoints(1);
                teamTwo.GainPoints(1);

                return $"The match between {teamOne.Name} and {teamTwo.Name} ends in a draw.";
            }
        }

        public string PromoteTeam(string droppingTeamName, string promotingTeamName, string managerTypeName, string managerName)
        {
          
            ITeam team = championship.Get(droppingTeamName);
            if (team == null)
            {
                return string.Format(OutputMessages.DroppingTeamDoesNotExist, droppingTeamName);
            }

            if (championship.Models.Any(x => x.Name == promotingTeamName))
            {
                return string.Format(OutputMessages.TeamWithSameNameExisting, promotingTeamName);
            }

            ITeam promotionTeam = new Team(promotingTeamName);

            bool managerAssigned = false;
            IManager manager = null;
            foreach (var currentTeam in championship.Models)
            {
                if (currentTeam.TeamManager != null && currentTeam.TeamManager.Name == managerName)
                {
                    managerAssigned = true;
                    break;
                }
            }
            if (!managerAssigned)
            {
                if (managerTypeName == nameof(AmateurManager))
                    manager = new AmateurManager(managerName);

                if (managerTypeName == nameof(SeniorManager))
                    manager = new SeniorManager(managerName);

                if (managerTypeName == nameof(ProfessionalManager))
                    manager = new ProfessionalManager(managerName);

                if (manager != null)
                {
                    promotionTeam.SignWith(manager);
                }
            }
            foreach (var currentsTeam in championship.Models)
            {
                currentsTeam.ResetPoints();
            }
            championship.Remove(droppingTeamName);

            
            championship.Add(promotionTeam);

            return string.Format(OutputMessages.TeamHasBeenPromoted, promotingTeamName);

        }

        public string ChampionshipRankings()
        {
            
            var orderedTeams = championship.Models
                .OrderByDescending(x => x.ChampionshipPoints)
                .ThenByDescending(x => x.PresentCondition)
                .ToList();
            var sb = new StringBuilder();
            sb.AppendLine("***Ranking Table***");
            for (int i = 0; i < orderedTeams.Count(); i++)
            {
                var team = orderedTeams[i];
                sb.AppendLine($"{i+1}. {team}/{(team.TeamManager != null ? team.TeamManager.ToString() : "No Manager")}");
                
            }
            return sb.ToString().TrimEnd();
        }
       
    }
}


