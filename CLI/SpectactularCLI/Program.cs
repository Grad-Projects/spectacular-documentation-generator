using SpectactularCLI.Commands;
using SpectactularCLI.Utilities;
using System;

public class App
{
    private readonly CommandHandler commandHandler = new CommandHandler();

    private async void Run()
    {
        Output.PrintWelcomeBanner();

        bool shouldContinue = true;

        while (shouldContinue)
        {
            await Output.PrintLoggedInUser();

            Output.PrintCommands();

            commandHandler.SetCommands(Global.Commands);

            Output.PrintEnterCommand();

            string userInput = Console.ReadLine();
            try
            {
                shouldContinue = await commandHandler.GetCommand(userInput).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred...");
                Console.WriteLine($"Error: {e}");
                Global.Commands = Global.DefaultCommands;
            }
        }
    }

    public static void Main(string[] args)
    {
        new App().Run();
    }
}
