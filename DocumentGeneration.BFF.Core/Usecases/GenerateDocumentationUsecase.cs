using DocumentGeneration.BFF.Core.Interfaces;
using DocumentGeneration.BFF.Core.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Usecases
{
    internal class GenerateDocumentationUsecase : IGenerateDocumentationUsecase
    {
        private readonly AnalyzeCode _analyze;
        private readonly ILogger<GenerateDocumentationUsecase> _logger;
        private readonly AnalyzeFolder _analyzeFolder;


        public GenerateDocumentationUsecase(AnalyzeCode analyze , ILogger<GenerateDocumentationUsecase> logger, AnalyzeFolder analyzeFolder)
        {
            _analyze = analyze;
            _logger = logger;
            _analyzeFolder = analyzeFolder;
        }


        string IGenerateDocumentationUsecase.Analyze(string base64String)
        {
            var result = _analyze(base64String);

            _logger.LogInformation(result);
            return result;
        }

        string IGenerateDocumentationUsecase.AnalyzeFolder(ZipArchive zipArchive)
        {
            var result = _analyzeFolder(zipArchive);

            return result;
        }
    }
}
