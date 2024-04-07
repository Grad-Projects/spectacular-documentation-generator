using System.IO.Compression;
using System.Net.Http.Json;

namespace OAuthCLI;

public class Program
{

    // Use only one HttpClient in the project
    readonly static HttpClient client = new();

    async static Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        OAuth user = new OAuth(client);
        // await user.Login();
        System.Console.WriteLine("Goodbye World!");

        Stream stream = new MemoryStream();

        ZipFile.CreateFromDirectory(@"C:\Users\bbdnet3310\Documents\Payslips", stream);

        ZipArchive zipArchive = new ZipArchive(stream);

        await client.PostAsJsonAsync("https://localhost:7118/api/generate/folder_documentation", zipArchive);
    }
}