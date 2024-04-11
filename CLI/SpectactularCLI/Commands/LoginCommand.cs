using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectactularCLI.Commands
{
    public class LoginCommand : Command
    {
        public LoginCommand() : base("L", "Login")
        {
        }

        public override bool Execute()
        {
            Console.WriteLine("Login TODO");
            return base.Execute();
        }
    }
}
