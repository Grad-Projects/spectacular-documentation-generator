using SpectactularCLI.Utilities;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SpectactularCLI.Commands
{
    public class GenerateDocumentsCommand : Command
    {
        private const string API_PATH = "/api/generate/documentation";

        public GenerateDocumentsCommand() : base("G", "Generate Documentation")
        {
        }

        public override async Task<bool> Execute()
        {
            try
            {
                GetStyleString();

                string queryParam = $"?style={Global.SelectedStyle}&api-version=1";

                List<string> userFilePaths = new List<string>();
                while (true)
                {
                    Console.Write("Enter path to file you would like to document (Please ensure it is a .cs file, leave blank to stop): ");
                    string inputFilePath = Console.ReadLine().Trim();
                    if (!String.IsNullOrEmpty(inputFilePath))
                    {
                        userFilePaths.Add(inputFilePath);
                    }
                    else
                    {
                        break;
                    }
                }

                List<string> base64Strings = HelperMethods.GetCSharpFilesAsBase64List(userFilePaths);

                string requestBody = JsonSerializer.Serialize(base64Strings);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Global.API_DOMAIN + API_PATH + queryParam);

                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await Global.Client.SendAsync(request);


                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                        // Deserialize JSON document into Dictionary<string, object>
                        var dictionary = new Dictionary<string, object>();

                        foreach (var property in jsonDocument.RootElement.EnumerateObject())
                        {
                            dictionary[property.Name] = GetValue(property.Value);
                        }

                        // Now you have a dictionary with key-value pairs
                        List<(string name, string value)> files = new List<(string name, string value)>();
                        foreach (var kvp in dictionary)
                        {
                            files.Add((kvp.Key, kvp.Value.ToString()));
                        }
                        HelperMethods.CreateHtmlFiles(files);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Returned an error in the polling request! Catch me later in the CLI");
            }

            return await base.Execute();
        }

        private void GetStyleString()
        {
            int index = 1;
            int choice = -1;
            while (true)
            {
                Console.WriteLine("Please select a style");
                foreach (var StyleOption in Global.StyleList)
                {
                    Console.WriteLine($"{index}\t{StyleOption}");
                    index++;
                };

                Console.Write("> ");

                string input = Console.ReadLine().Trim();

                if (int.TryParse(input, out choice) && choice >= 0 && choice <= Global.StyleList.Count)
                {
                    Global.SelectedStyle = Global.StyleList[choice - 1];
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer index.");
                    index = 1;
                }
            }
        }

        public class YourObjectType
        {
            public string App { get; set; }
        }

        // Helper method to convert JSON values to proper types
        static object GetValue(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Number:
                    return element.GetInt32(); // Adjust to appropriate type if necessary
                case JsonValueKind.String:
                    return element.GetString();
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
                default:
                    return null; // Handle other value kinds as needed
            }
        }
    }
}
