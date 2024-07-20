using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = CommandPattern.Core.Contracts.ICommand;

namespace CommandPattern
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {//Each command line will look as it follows: "{CommandName} {CommandArgs}".
         //CommandName will be as follows: "Hello" -> executing HelloCommand and so on.

            string[] arguments = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = $"{arguments[0]}Command";//CommandName
            var commandArgs = arguments.Skip(1).ToArray();//CommandArgs

            var assembly = Assembly.GetEntryAssembly();

            Type type = assembly.GetTypes()
                .FirstOrDefault(n => n.Name == command);// get type in assembly with this name

            if (type == null)
            {
                throw new ArgumentException("Invalid type");
            }
            var instance = (ICommand)Activator.CreateInstance(type);
            return instance.Execute(commandArgs);
        }

    }
}
