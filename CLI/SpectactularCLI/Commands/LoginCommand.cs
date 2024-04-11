using SpectactularCLI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SpectactularCLI.Commands
{
    public class LoginCommand : Command
    {
        static readonly HttpClient client = new();
        string _deviceCode = "";

        public LoginCommand() : base("L", "Login")
        {
        }

        public override async Task<bool> Execute()
        {
            // Initialize
            var loginRequestDict = new Dictionary<string, string>
            {
                { "client_id", Global.githubClientID }
            };
            string loginURL = "https://github.com/login/device/code/";
            int interval = 5;

            try
            {
                var response = await client.PostAsJsonAsync(loginURL, loginRequestDict);

                var responseDict = QueryStringToDict(await response.Content.ReadAsStringAsync());
                _deviceCode = responseDict["device_code"];
                interval = int.Parse(responseDict["interval"]);
                string userCode = responseDict["user_code"];
                var verificationURL = DecodeUrlString(responseDict["verification_uri"]);
                await Console.Out.WriteLineAsync($"Please navigate to {verificationURL}\nEnter the following Code: {userCode}");
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Returned an error in the initial request to setup the OAuth Flow!");
            }

            // Begin task to wait for login info
            var pollRequestDict = new Dictionary<string, string>
            {
                { "client_id", Global.githubClientID },
                { "device_code", _deviceCode },
                { "grant_type", "urn:ietf:params:oauth:grant-type:device_code" }
            };
            var pollURL = "https://github.com/login/oauth/access_token";
            bool verified = false;
            bool expired = false;
            while (!verified && !expired)
            {
                Thread.Sleep((interval + 1) * 1000);
                try
                {
                    var response = await client.PostAsJsonAsync(pollURL, pollRequestDict);

                    var responseDict = QueryStringToDict(await response.Content.ReadAsStringAsync());

                    if (responseDict.TryGetValue("access_token", out string? value))
                    {
                        verified = true;
                        Global.AccessToken = value;
                    }
                    else
                    {
                        var error = responseDict["error"];
                        if (error == "authorization_pending")
                        {
                            // Do nothing, we're still waiting
                        }
                        else if (error == "slow_down")
                        {
                            interval = int.Parse(responseDict["interval"]);
                        }
                        else if (error == "expired_token")
                        {
                            expired = true;
                        }
                        else
                        {
                            throw new Exception($"Couldn't handle the error message. You must've set something up wrong!\nError was: {error}");
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Returned an error in the polling request! Catch me later in the CLI");
                }
            }

            return await base.Execute();
        }

        // ---------------------------------- Helper Methods ---------------------------------------------------------------

        // https://stackoverflow.com/a/3847593
        private static string DecodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }

        // Helper method to create a dictionary with the query string returned from GitHub
        private static Dictionary<string, string> QueryStringToDict(string queryString)
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
    }
}
