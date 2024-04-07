using System.IO.Compression;

namespace DocumentGeneration.BFF.DocumentationGen.Service.Utils
{
    internal class ZipFileHandler
    {
        public static void SaveZipFile(ZipArchive zipArchive, string filePath)
        {
            zipArchive.ExtractToDirectory(filePath);
        }
    }
}