using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectactularCLI.Commands
{
    public abstract class Command
    {
        private readonly string identifier;
        private readonly string description;
        public Command(string identifier, string description)
        {
            this.identifier = identifier;
            this.description = description;
        }

        public string GetIdentifier()
        {
            return identifier;
        }

        public string GetDescription()
        {
            return description;
        }

        public virtual bool Enabled()
        {
            return true;
        }

        public virtual bool Hidden()
        {
            return false;
        }

        public virtual bool Execute()
        {
            return true;
        }

        public virtual string GetPrefix()
        {
            return "";
        }
    }

}
