using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectactularCLI.Utilities
{
    public static class HelperMethods
    {
        public static string DecodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }
        public static Dictionary<string, string> QueryStringToDict(string queryString)
        {
            Dictionary<string, string> output = [];
            string[] responseSplit = queryString.Split("&");
            foreach (var s in responseSplit)
            {
                var sSplit = s.Split("=");
                output.Add(sSplit[0], sSplit[1]);
            }
            return output;
        }

        //checks to see if file is a c# file
        public static bool IsCSharpFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return string.Equals(extension, ".cs", StringComparison.OrdinalIgnoreCase);
        }

        //converts file to base64 string
        public static List<string> GetCSharpFilesAsBase64List(List<string> filePaths)
        {
            List<string> csharpFiles = new List<string>();

            foreach (string filePath in filePaths)
            {
                if (IsCSharpFile(filePath))
                {
                    csharpFiles.Add(ConvertFileToBase64(filePath));
                }
                else
                {
                    Console.WriteLine($"{filePath} is not a valid C# file!");
                }
            }

            return csharpFiles;
        }

        //converts html string to html file and saves them to a specified path
        public static void CreateHtmlFiles(List<(string name, string html)> files, string folderPath)
        {
            foreach (var file in files)
            {
                try
                {
                    string fileName = file.name + ".html";
                    string filePath = Path.Combine(folderPath, fileName);

                    // Ensure the directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        writer.Write(file.html);
                    }

                    Console.WriteLine($"Created HTML file: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating HTML file: {ex.Message}");
                }
            }
        }

        public static string ConvertFileToBase64(string filePath)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64String = Convert.ToBase64String(fileBytes);
                return base64String;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Files referenced cannot be converted to base64 strings");
                throw;
            }
        }
    }
}
