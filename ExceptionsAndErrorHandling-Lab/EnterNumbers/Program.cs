﻿namespace EnterNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            while (numbers.Count < 10)
            {
                try
                {
                    if (!numbers.Any())
                    {
                        numbers.Add(ReadNumber(1, 100));
                    }
                    else
                    {
                        numbers.Add(ReadNumber(numbers.Max(), 100));
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
            Console.WriteLine(string.Join(", ", numbers));
        }
        static int ReadNumber(int start, int end)
        {
            string input = Console.ReadLine();
            int number;
            try
            {
                number = int.Parse(input);
            }
            catch (FormatException fe)
            {
                throw new FormatException("Invalid Number!");
            }
            if (number <= start || number >= end)
            {
                throw new ArgumentException($"Your number is not in range {start} - {end}!");
            }
            return number;
        }
    }
}
