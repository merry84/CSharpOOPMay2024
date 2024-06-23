namespace FootballTeamGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*Your application will receive commands until the "END" command is given. The command can be one of the following:
            •	"Team;{TeamName}" - add a new Team;
            •	"Add;{TeamName};{PlayerName};{Endurance};{Sprint};{Dribble};{Passing};{Shooting}" - add a new Player to the Team;
            •	"Remove;{TeamName};{PlayerName}" - remove the Player from the Team;
            •	"Rating;{TeamName}" - print the Team rating, rounded to an integer.
            */
            List<Team> teams = new();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] teamInfo = input.Split(";");
                try
                {
                    //"Team;{TeamName}" - add a new Team;
                    if (teamInfo[0] == "Team")
                    {
                        teams.Add(new Team(teamInfo[1]));
                        continue;
                    }
                    Team team = teams.FirstOrDefault(t => t.Name == teamInfo[1]);

                    if (teamInfo[0] == "Add")
                    {
                        //•	If you receive a command to add a Player to a missing Team, print "Team [team name] does not exist."
                        if (team == null)
                        {
                            Console.WriteLine($"Team {teamInfo[1]} does not exist.");
                            continue;
                        }
                        //"Add;{TeamName};{PlayerName};  {Endurance};         {Sprint};                {Dribble};              {Passing};             {Shooting}" - add a new Player to the Team;
                        team.AddPlayer(new Player(teamInfo[2], int.Parse(teamInfo[3]), int.Parse(teamInfo[4]), int.Parse(teamInfo[5]), int.Parse(teamInfo[6]), int.Parse(teamInfo[7])));
                    }
                    else if (teamInfo[0] == "Remove")
                    {
                        Player player = team.Players.FirstOrDefault(x => x.Name == teamInfo[2]);
                        if (player == null)
                        {
                            Console.WriteLine($"Player {teamInfo[2]} is not in {teamInfo[1]} team.");
                            continue;
                        }
                        team.RemovePlayer(teamInfo[2]);
                    }
                    else//"Rating;{TeamName}" - print the Team rating, rounded to an integer.
                    {
                        if (team == null)
                        {
                            Console.WriteLine($"Team {teamInfo[1]} does not exist.");
                            continue;
                        }
                        Console.WriteLine(team);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
