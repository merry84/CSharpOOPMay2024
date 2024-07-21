using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> players;
        private IRepository<ITeam> teams;
        public Controller()
        {
            players = new PlayerRepository();
            teams = new TeamRepository();
        }
        public string LeagueStandings()
        {
           /*Returns information about each team from the TeamRepository. 
            * Arrange the teams by PointsEarned - descending, then by OverallRating – descending, then by teamName - alphabetically.
            * In order to receive the correct output, use the ToString() method of each team:
            "***League Standings***
            {team1} 
            {team2}
            ...
            {teamn}"
            */
           var team = teams.Models.OrderByDescending(x=>x.PointsEarned).ThenByDescending(x=>x.OverallRating).ThenBy(x=>x.Name);
           var sb = new StringBuilder();
            sb.AppendLine("***League Standings***");
            foreach (var player in team)
            {
                sb.AppendLine($"{player.ToString()}");
            }
            return sb.ToString().TrimEnd();
        }

        public string NewContract(string playerName, string teamName)//??
        {
            /*•
          •	If none of the above cases is reached, update the player.Team property with the name of the team, 
            indicating that the player is now a part of that team. Add the player to the Players collection of the team using the appropriate method.
          •	Return the following message to confirm the successful signing of the contract: "Player {playerName} signed a contract with {teamName}."
          */
            if (!players.ExistsModel(playerName))
            {
                return string.Format(OutputMessages.PlayerNotExisting, playerName, nameof(PlayerRepository));
            }
            if (!teams.ExistsModel(teamName))
            {
                return string.Format(OutputMessages.TeamNotExisting, teamName, nameof(TeamRepository));
            }
            IPlayer player = players.GetModel(playerName);
            ITeam team = teams.GetModel(teamName);
            if (player.Team != null)
            {
                return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, teamName);
            }
            player.JoinTeam(playerName);
            team.SignContract(player);
            return string.Format(OutputMessages.SignContract, playerName, teamName);
        }

        public string NewGame(string firstTeamName, string secondTeamName)//Sequence contains no matching element
        {

            ITeam firtsTeam = teams.GetModel(firstTeamName);
            ITeam secondTeam = teams.GetModel(secondTeamName);
            if (firtsTeam.OverallRating != secondTeam.OverallRating)
            {
                ITeam win;
                ITeam lose;
                if (firtsTeam.OverallRating > secondTeam.OverallRating)
                {
                    win = firtsTeam;
                    lose = secondTeam;
                }
                else
                {
                    win = secondTeam;
                    lose = firtsTeam;
                }
                win.Win();
                lose.Lose();

                return string.Format(OutputMessages.GameHasWinner, win.Name, lose.Name);
            }
            else
            {
                firtsTeam.Draw();
                secondTeam.Draw();
                return string.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);
            }
        }

        public string NewPlayer(string typeName, string name)
        {

            if (typeName != nameof(Goalkeeper) && typeName != nameof(CenterBack) && typeName != nameof(ForwardWing))
            {
                return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }
            if (players.ExistsModel(name))
            {
                var existingPlayerTypeName = players.GetModel(name).GetType().Name;
                return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, nameof(PlayerRepository), existingPlayerTypeName);
            }
            IPlayer player;
            if (typeName == nameof(Goalkeeper))
            { player = new Goalkeeper(name); }
            else if (typeName == nameof(CenterBack))
            { player = new CenterBack(name); }
            else
            { player = new ForwardWing(name); }
            players.AddModel(player);
            return string.Format(OutputMessages.PlayerAddedSuccessfully, name);
        }

        public string NewTeam(string name)
        {

            if (teams.ExistsModel(name))
            {
                return string.Format(OutputMessages.TeamAlreadyExists, name, nameof(TeamRepository));
            }
            teams.AddModel(new Team(name));
            return string.Format(OutputMessages.TeamSuccessfullyAdded, name, nameof(TeamRepository));
        }

        public string PlayerStatistics(string teamName)
        { /*Returns information about each player from the team with the given name
           * (every name passed as a parameter will be a name of an existing team in the application). 
           * Arrange the players by Rating - descending, then by Name - alphabetically.
           * In order to receive the correct output, use the ToString() method of each player:
            "***{teamName}***
            {player1} 
            {player2}            
            {playern}"
            */
            var sb = new StringBuilder();
            var team = teams.GetModel(teamName);
            var playersFiltred = team.Players.OrderByDescending(x=>x.Rating).ThenBy(x=>x.Name);

            sb.AppendLine($"***{teamName}***");
            foreach (var player in playersFiltred) 
            {
                sb.AppendLine($"{player.ToString()}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
