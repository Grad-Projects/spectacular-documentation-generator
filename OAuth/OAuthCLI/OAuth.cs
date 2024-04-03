namespace OAuthCLI;

public class OAuth
{

    readonly string ClientID;
    readonly HttpClient client;

    string? AccessToken { get; set; }
    string _deviceCode = "";

    public OAuth(HttpClient client)
    {
        this.client = client;
        var envClientID = Environment.GetEnvironmentVariable("GithubClientID") ?? throw new Exception("Environment Variable for Github Client ID not set!");
        this.ClientID = envClientID;
    }

    public async Task Login()
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

            var responseString = await response.Content.ReadAsStringAsync();
            // TODO: Write a helper function to turn this into a dictionary
            string[] responseSplit = responseString.Split("&");
            _deviceCode = responseSplit[0].Split("=")[1];
            interval = int.Parse(responseSplit[2].Split("=")[1]);
            string loginCode = responseSplit[3].Split("=")[1];
            var verificationURL = DecodeUrlString(responseSplit[4].Split("=")[1]);
            System.Console.WriteLine($"Please navigate to {verificationURL}\nEnter the following Code: {loginCode}");
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
        int maxCount = 0;
        while (!verified && maxCount < 10)
        {
            System.Console.WriteLine($"Polling #{maxCount}");
            Thread.Sleep((interval + 1) * 1000);
            try
            {
                var response = await client.PostAsync(pollURL, pollRequestBody);

                var responseString = await response.Content.ReadAsStringAsync();
                string[] responseSplit = responseString.Split("&");
                if (responseSplit[0].Split("=")[0] != "error")
                {
                    verified = true;
                    AccessToken = responseSplit[0].Split("=")[1];
                }
                else
                {
                    // TODO: Handle Errors
                }
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine("Returned an error in the polling request! Catch me later in the CLI");
                Console.Error.Write(e);
            }
            maxCount++;
        }

        System.Console.WriteLine($"Got Access Token: {AccessToken}");

    }

    // https://stackoverflow.com/a/3847593
    private static string DecodeUrlString(string url)
    {
        string newUrl;
        while ((newUrl = Uri.UnescapeDataString(url)) != url)
            url = newUrl;
        return newUrl;
    }
}