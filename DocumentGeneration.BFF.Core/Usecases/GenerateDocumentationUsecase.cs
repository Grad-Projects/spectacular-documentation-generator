using DocumentGeneration.BFF.Core.Interfaces;
using DocumentGeneration.BFF.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Usecases
{
    internal class GenerateDocumentationUsecase : IGenerateDocumentationUsecase
    {
        private readonly AnalyzeCode _analyze;

        public GenerateDocumentationUsecase(AnalyzeCode analyze)
        {
            _analyze = analyze;
        }

        string IGenerateDocumentationUsecase.Analyze(byte[] bytes)
        {
            var result = _analyze(bytes);

            return result;
        }
    }
}
