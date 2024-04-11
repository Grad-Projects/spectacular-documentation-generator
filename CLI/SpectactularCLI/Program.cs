using SpectactularCLI.Commands;
using SpectactularCLI.Utilities;
using System;

public class App
{

    private static async Task Run()
    {

        CommandHandler commandHandler = new CommandHandler();

        Output.PrintWelcomeBanner();

        bool shouldContinue = true;

        while (shouldContinue)
        {
            //await Output.PrintLoggedInUser();

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
                Global.Commands = Global.DefaultCommands;
            }
        }
    }

    public static async Task Main(string[] args)
    {
        await Run();
    }
}
