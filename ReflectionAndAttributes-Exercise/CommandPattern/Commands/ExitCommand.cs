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
    //ExitCommand - It should exit the program and return null.
    public class ExitCommand : ICommand
    {
        private const int IsOKCode = 0;
        public string Execute(string[] args)
        {
            Environment.Exit(IsOKCode);
            return null;
        }

    }
}

