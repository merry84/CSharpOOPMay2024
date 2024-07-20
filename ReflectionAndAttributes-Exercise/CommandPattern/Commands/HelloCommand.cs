using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommandPattern.Core.Contracts;
using ICommand = CommandPattern.Core.Contracts.ICommand;

namespace CommandPattern.Commands
{
    //HelloCommand - The result from its execution should be: $"Hello, {args[0]}".

    public class HelloCommand : ICommand
    {
        public string Execute(string[] args)
        => $"Hello, {args[0]}";
    }
}
