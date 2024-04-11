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
                    string inputFilePath = Console.ReadLine();
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

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Global.API_DOMAIN + API_PATH + queryParam);

                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await Global.Client.SendAsync(request);


                if (response.IsSuccessStatusCode)
                {
                    var responseDict = HelperMethods.QueryStringToDict(await response.Content.ReadAsStringAsync());

                    Console.WriteLine(responseDict);
                }


            }
            catch (HttpRequestException e)
            {
                throw new Exception("Returned an error in the polling request! Catch me later in the CLI");
            }


            Console.WriteLine(GetDescription());
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

                string input = Console.ReadLine();

                if (int.TryParse(input, out choice) && choice >= 0 && choice < Global.StyleList.Count)
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
    }
}
