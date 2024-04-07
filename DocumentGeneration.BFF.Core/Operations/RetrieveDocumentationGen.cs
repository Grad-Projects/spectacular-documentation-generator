using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.Core.Operations
{
    public delegate String AnalyzeCode(string base64String);

    public delegate String AnalyzeFolder(ZipArchive zipArchive);

}
