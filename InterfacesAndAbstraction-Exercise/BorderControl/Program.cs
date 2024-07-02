using System.Runtime.CompilerServices;

namespace BorderControl
{
    public class Program
    {
        static void Main(string[] args)
        {

            List<IId> output = new();
            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 3)
                {
                    output.Add(new Citizens(tokens[0], int.Parse(tokens[1]), tokens[2]));
                }
                else
                {
                    output.Add(new Robots(tokens[0], tokens[1]));
                }
            }
            string fakeId = Console.ReadLine();

            foreach (var id in output)
            {
                if (!id.IsValidId(fakeId))
                {
                    Console.WriteLine(id.Id);
                }
            }
        }
    }
}
