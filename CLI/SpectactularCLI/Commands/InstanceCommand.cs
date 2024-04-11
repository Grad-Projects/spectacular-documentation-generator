using SpectactularCLI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectactularCLI.Commands
{
    public class InstanceCommand : Command
    {
        public InstanceCommand(string identifier, string description) : base(identifier, description)
        {
        }

        public override async Task<bool> Execute()
        {
            Global.SelectedStyle = this.GetDescription();
            return await base.Execute();
        }
    }
}
