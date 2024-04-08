using DocumentGeneration.BFF.Core.Interfaces;
using DocumentGeneration.BFF.Core.Models;
using DocumentGeneration.BFF.Core.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Usecases
{
    internal class GenerateDocumentationUsecase : IGenerateDocumentationUsecase, IConvertToHtmlUsecase, IAnalyzeCode
    {
        private readonly AnalyzeCode _analyze;
        private readonly ILogger<GenerateDocumentationUsecase> _logger;
        private readonly ConvertToHtml _convertToHtml;

        public GenerateDocumentationUsecase(AnalyzeCode analyze , ILogger<GenerateDocumentationUsecase> logger, ConvertToHtml convertToHtml)
        {
            _analyze = analyze;
            _logger = logger;
            _convertToHtml = convertToHtml;
        }

        public documentBaseClass Analyze(string base64String)
        {
            documentBaseClass result = _analyze(base64String);

            _logger.LogInformation(result.Methods[0].Parameters[0].ToString());

            return result;
        }

        public string ToHtml(documentBaseClass fileInfo)
        {
           var html = _convertToHtml(fileInfo);
            return html;
        }

        public List<string> GenDocumentation(List<string> files)
        {
            List<documentBaseClass> fileInfo = new List<documentBaseClass>();
            foreach (var file in files)
            {
                fileInfo.Add(Analyze(file));
            }

            List<string> htmlForFiles = new List<string>();
            foreach (var file in fileInfo)
            {
                htmlForFiles.Add(ToHtml(file));
            }
            
            return htmlForFiles;
        }

    }
}
