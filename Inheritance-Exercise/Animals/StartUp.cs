using System;
using System.Text;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var result = new StringBuilder();
            string input;

            while ((input = Console.ReadLine()) != "Beast!")
            {
                string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                string gender = null;
                if (tokens.Length > 2) { gender = tokens[2]; }
                try
                {
                    if (input == "Cat")
                    {
                        Cat cat = new Cat(name, age, gender);
                        result.AppendLine(cat.ToString());
                    }
                    if (input == "Dog")
                    {
                        Dog dog = new Dog(name, age, gender);
                        result.AppendLine(dog.ToString());
                    }
                    if (input == "Frog")
                    {
                        Frog frog = new Frog(name, age, gender);
                        result.AppendLine(frog.ToString());
                    }
                    if (input == "Tomcat")
                    {
                        Tomcat tomcat = new Tomcat(name, age);
                        result.AppendLine(tomcat.ToString());
                    }
                    if (input == "Kitten")
                    {
                        Kitten kitten = new Kitten(name, age);
                        result.AppendLine(kitten.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            Console.WriteLine(result.ToString());
        }
    }
}
