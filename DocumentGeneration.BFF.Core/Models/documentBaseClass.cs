using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Models
{
    public class documentBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string AccessModifier { get; set; }
        
        public List<documentFeildClass> Fields { get; set; } = new List<documentFeildClass>();

        public List<documentMethodClass> Methods { get; set; } = new List<documentMethodClass>();

        public List<(string name, string type)> InheritsFrom { get; set; } = new List<(string name, string type)>();

        public List<string> Dependency { get; set; } = new List<string>();

    }
}
