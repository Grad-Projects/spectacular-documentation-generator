namespace OAuthCLI;

public class Program
{

    // Use only one HttpClient in the project
    readonly static HttpClient client = new();

    async static Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        OAuth user = new OAuth(client);
        await user.Login();
        System.Console.WriteLine("Goodbye World!");

    }
}