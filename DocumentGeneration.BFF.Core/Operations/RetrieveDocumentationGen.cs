using DocumentGeneration.BFF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Operations
{
    public delegate Task<List<(string, string)>> GenerateDocumentation(List<string> files, string styleName, string userName);
}
