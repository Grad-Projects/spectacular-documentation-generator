using SpectactularCLI.Utilities;
using System.Net.Http.Json;

namespace SpectactularCLI.Commands
{
    public class LoginCommand : Command
    {
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
                var response = await Global.Client.PostAsJsonAsync(loginURL, loginRequestDict);

                var responseDict = HelperMethods.QueryStringToDict(await response.Content.ReadAsStringAsync());
                _deviceCode = responseDict["device_code"];
                interval = int.Parse(responseDict["interval"]);
                string userCode = responseDict["user_code"];
                var verificationURL = HelperMethods.DecodeUrlString(responseDict["verification_uri"]);
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
                    var response = await Global.Client.PostAsJsonAsync(pollURL, pollRequestDict);

                    var responseDict = HelperMethods.QueryStringToDict(await response.Content.ReadAsStringAsync());

                    if (responseDict.TryGetValue("access_token", out string? value))
                    {
                        verified = true;
                        Global.AccessToken = value;
                        Global.Client.DefaultRequestHeaders.Add("Authorization", $"{Global.AccessToken}");
                        Global.Commands.Clear();
                        Global.Commands.Add(new GenerateDocumentsCommand());
                        Global.Commands.Add(new QuitCommand());
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
    }
}
