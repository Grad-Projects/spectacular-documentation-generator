using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Interfaces
{
    public interface IGenerateDocumentationUsecase
    {
        public string Analyze(string base64String);
        public string AnalyzeFolder(ZipArchive zipArchive);

    }
}
