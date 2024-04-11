using SpectactularCLI.Commands;
using SpectactularCLI.Utilities;
using System;

public class App
{
    private readonly CommandHandler commandHandler = new CommandHandler();

    private void Run()
    {
        Output.PrintWelcomeBanner();

        bool shouldContinue = true;

        while (shouldContinue)
        {
            Output.PrintLoggedInUser();

            Output.PrintCommands();

            commandHandler.SetCommands(Global.Commands);

            Output.PrintEnterCommand();

            string userInput = Console.ReadLine();
            try
            {
                shouldContinue = commandHandler.GetCommand(userInput).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred...");
                Global.Commands = Global.DefaultCommands;
            }
        }
    }

    public static void Main(string[] args)
    {
        new App().Run();
    }
}
