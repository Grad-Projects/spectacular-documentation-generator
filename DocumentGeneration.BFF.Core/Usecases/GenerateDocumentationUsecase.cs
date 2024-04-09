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
    internal class GenerateDocumentationUsecase : IGenerateDocumentationUsecase 
    {
        private readonly AnalyzeCode _analyze;
        private readonly ILogger<GenerateDocumentationUsecase> _logger;
        private readonly ConvertToHtml _convertToHtml;
        private readonly getStyleFromDB _getStyleFromDB;
        private readonly postDocumentToDB _postDocumentToDB;
        private readonly CheckOrAddUser _checkOrAddUserToDB;

        public GenerateDocumentationUsecase(AnalyzeCode analyze , ILogger<GenerateDocumentationUsecase> logger, ConvertToHtml convertToHtml, getStyleFromDB databaseService, postDocumentToDB postDocumentToDB, CheckOrAddUser checkOrAddUserToDB)
        {
            _analyze = analyze;
            _logger = logger;
            _convertToHtml = convertToHtml;
            _getStyleFromDB = databaseService;
            _postDocumentToDB = postDocumentToDB;
            _checkOrAddUserToDB = checkOrAddUserToDB;   
        }

        public documentBaseClass Analyze(string base64String)
        {
            documentBaseClass result = _analyze(base64String);

            _logger.LogInformation(result.Methods[0].Parameters[0].ToString());

            return result;
        }

        public string ToHtml(documentBaseClass fileInfo, string style)
        {
           var html = _convertToHtml(fileInfo, style);
           return html;
        }

        public async Task<List<(string, string)>> GenDocumentation(List<string> files, string styleName, string userName)
        {
            var style = await getStyleFromDB(styleName);
            List<documentBaseClass> fileInfo = new List<documentBaseClass>();
            foreach (var file in files)
            {
                fileInfo.Add(Analyze(file));
            }

            List<(string name, string html)> htmlForFiles = new List<(string name, string html)>();
            foreach (var file in fileInfo)
            {
                htmlForFiles.Add((file.Name, ToHtml(file, style)));
            }

            foreach (var file in htmlForFiles)
            {
                await _postDocumentToDB(file.html, style, userName, file.name);
            }


            return htmlForFiles;
        }

        public async Task<string> getStyleFromDB(string styleName)
        {
            var result = await _getStyleFromDB(styleName);
            return result;
        }
    }
}
