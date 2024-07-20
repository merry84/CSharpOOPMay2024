using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern
{
    public class Engine : IEngine
    {
        private readonly ICommandInterpreter commandInterpreter;
        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = new CommandInterpreter();
        }
        public void Run()
        {
            //The Run() method should accept input from the console and pass it to the proper class,
            //as well as print the output from the commands. 
            while (true)
            {
                string input = Console.ReadLine();
                string output = string.Empty;
                try
                {
                    output = commandInterpreter.Read(input);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine(output);
            }

        }
    }
}
