using PersonInfo;

namespace BirthdayCelebrations
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
             * You will receive from the console an unknown number of lines. Until the command "End" is received,
             * each line will contain information in one of the following formats "Citizen <name> <age> <id> <birthdate>"
             * for Citizen, "C <model> <id>" for Robot or "Pet <name> <birthdate" for Pet.
             * After the "End" command on the next line, you will receive a single number representing a specific year, 
             * your task is to print all birthdates (of both Citizen and Pet) in that year in the format day/month/year in the order of input.
             */
            List<IBirthable> list = new List<IBirthable>();
            string input;

            while ((input = Console.ReadLine()) != "End") 
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (tokens[0] == "Citizen")
                {
                    list.Add(new Citizen(tokens[1], int.Parse(tokens[2]), tokens[3], tokens[4]));
                }
                if(tokens[0] == "Pet")
                {
                    list.Add(new Pet(tokens[1], tokens[2]));
                }
            }
            string outputYear = Console.ReadLine();

            var year = list.Where(x=>x.Birthdate.EndsWith(outputYear)).ToList();
            foreach (var currentYear in year) 
            {
                Console.WriteLine(currentYear.Birthdate);
            }
        }
    }
}
