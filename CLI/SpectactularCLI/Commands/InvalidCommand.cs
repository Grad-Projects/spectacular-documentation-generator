using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectactularCLI.Commands
{
    public class InvalidCommand : Command
    {
        public InvalidCommand(string identifier) : base("?", $"\"{identifier}\" is not a valid selection.\n")
        {
        }

        public override bool Execute()
        {
            Console.WriteLine(GetDescription());
            return base.Execute();
        }
    }
}
