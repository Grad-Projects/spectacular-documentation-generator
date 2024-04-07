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
        public List<string> GenDocumentation(List<string> files);
    }
}
