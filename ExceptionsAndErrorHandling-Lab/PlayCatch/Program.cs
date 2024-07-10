namespace PlayCatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int exceptionCount = 0;
            while (exceptionCount < 3)
            {
                string[] commands = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    string action = commands[0];
                    int index = int.Parse(commands[1]);
                    if (action == "Replace")
                    {
                        int nextIndex = int.Parse(commands[2]);
                        numbers[index] = nextIndex;
                    }
                    else if (action == "Print")
                    {
                        int endIndex = int.Parse(commands[2]);
                        List<int> result = new List<int>();
                        for (int i = index; i <= endIndex; i++)
                        {
                            result.Add(numbers[i]);

                        }
                        Console.WriteLine(string.Join(", ", result));
                    }
                    else if (action == "Show") { Console.WriteLine(numbers[index]); }

                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The index does not exist!");
                    exceptionCount++;
                }
                catch (FormatException)
                {
                    Console.WriteLine("The variable is not in the correct format!");
                    exceptionCount++;
                }
            }
            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}
