using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectactularCLI.Commands
{
    public class QuitCommand : Command
    {
        public QuitCommand() : base("Q", "Quit")
        {
        }

        public override Task<bool> Execute()
        {
            return Task.FromResult(false);
        }

        public override string GetPrefix()
        {
            return "\n";
        }
    }
}
