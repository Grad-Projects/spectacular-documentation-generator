using DocumentGeneration.BFF.Core.Interfaces;
using DocumentGeneration.BFF.Core.Operations;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GenerateDocumentationUsecase> _logger;

        public GenerateDocumentationUsecase(AnalyzeCode analyze , ILogger<GenerateDocumentationUsecase> logger)
        {
            _analyze = analyze;
            _logger = logger;
        }

        string IGenerateDocumentationUsecase.Analyze(string base64String)
        {
            var result = _analyze(base64String);

            _logger.LogInformation(result);
            return result;
        }
    }
}
