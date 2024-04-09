using DocumentGeneration.BFF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Interfaces
{
    public interface IGenerateDocumentationUsecase
    {
        public Task<List<(string, string)>> GenDocumentation(List<string> files, string styleName, string userName);
    }
}
