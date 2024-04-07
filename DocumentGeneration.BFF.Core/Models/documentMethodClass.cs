using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Models
{
    public class documentMethodClass
    {
        public string MethodName { get; set; }

        public string type { get; set; }

        public string accessModifier { get; set; }
        public List<(string type, string name)> parameters { get; set; } = new List<(string type, string name)>();


    }
}
