using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectactularCLI.Commands
{
    internal class CommandHandler
    {
        private List<Command> commands;

        public void SetCommands(List<Command> commands)
        {
            this.commands = commands;
        }

        public Command GetCommand(string identifier)
        {
            foreach (Command command in commands)
            {
                if (command.Enabled() && identifier.Equals(command.GetIdentifier(), StringComparison.OrdinalIgnoreCase))
                {
                    return command;
                }
            }
            return new InvalidCommand(identifier);
        }
    }
}
