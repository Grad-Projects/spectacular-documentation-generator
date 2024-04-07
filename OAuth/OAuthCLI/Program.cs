namespace OAuthCLI;

public class Program
{

    // Use only one HttpClient in the project
    readonly static HttpClient client = new();

    async static Task Main(string[] args)
    {
        Console.WriteLine("Simulating Login");

        OAuth user = new OAuth(client);
        bool success = await user.Login();
        if (success)
        {
            System.Console.WriteLine(user.AccessToken);
        }
        else 
        {
            System.Console.WriteLine("Failed to get token!");
        }

    }
}