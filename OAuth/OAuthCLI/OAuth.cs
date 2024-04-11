namespace OAuthCLI;

public class OAuth
{

    readonly string ClientID;
    readonly HttpClient client;

    public string? AccessToken { get; set; }
    string _deviceCode = "";

    public OAuth(HttpClient client)
    {
        this.client = client;
        var envClientID = Environment.GetEnvironmentVariable("GithubClientID") ?? throw new Exception("Environment Variable for Github Client ID not set!");
        //var envClientID = "Iv1.be664de67499daba";
        this.ClientID = envClientID;
    }

    public async Task<bool> Login()
    {
        // Initialize
        var loginRequestDict = new Dictionary<string, string>
        {
            { "client_id", ClientID }
        };
        var loginRequestBody = new FormUrlEncodedContent(loginRequestDict);
        var loginURL = "https://github.com/login/device/code";
        int interval = 5;

        try
        {
            var response = await client.PostAsync(loginURL, loginRequestBody);

            var responseDict = queryStringToDict(await response.Content.ReadAsStringAsync());
            _deviceCode = responseDict["device_code"];
            interval = int.Parse(responseDict["interval"]);
            string userCode = responseDict["user_code"];
            var verificationURL = DecodeUrlString(responseDict["verification_uri"]);
            System.Console.WriteLine($"Please navigate to {verificationURL}\nEnter the following Code: {userCode}");
        }
        catch (HttpRequestException e)
        {
            Console.Error.WriteLine("Returned an error in the initial request! Catch me later in the CLI");
            Console.Error.Write(e);
        }

        // Begin task to wait for login info
        var pollRequestDict = new Dictionary<string, string>
        {
            { "client_id", ClientID },
            { "device_code", _deviceCode },
            { "grant_type", "urn:ietf:params:oauth:grant-type:device_code" }
        };
        var pollRequestBody = new FormUrlEncodedContent(pollRequestDict);
        var pollURL = "https://github.com/login/oauth/access_token";
        bool verified = false;
        bool expired = false;
        while (!verified && !expired)
        {
            Thread.Sleep((interval + 1) * 1000);
            try
            {
                var response = await client.PostAsync(pollURL, pollRequestBody);

                var responseDict = queryStringToDict(await response.Content.ReadAsStringAsync());

                if (responseDict.TryGetValue("access_token", out string? value))
                {
                    verified = true;
                    AccessToken = value;
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
                    else {
                        throw new Exception($"Couldn't handle the error message. You must've set something up wrong!\nError was: {error}");
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine("Returned an error in the polling request! Catch me later in the CLI");
                Console.Error.Write(e);
            }
        }

        return AccessToken != null;
    }

    // https://stackoverflow.com/a/3847593
    private static string DecodeUrlString(string url)
    {
        string newUrl;
        while ((newUrl = Uri.UnescapeDataString(url)) != url)
            url = newUrl;
        return newUrl;
    }

    // Helper method to create a dictionary with the query string returned from GitHub
    private static Dictionary<string, string> queryStringToDict(string queryString)
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