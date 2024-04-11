using SpectactularCLI.Commands;

namespace SpectactularCLI.Utilities
{
    public class Output
    {
        public static void PrintWelcomeBanner()
        {
            Console.WriteLine("\r\n   _____                 __                   __              ____                                        __     ______                           __            \r\n  / ___/____  ___  _____/ /_____ ________  __/ /___ ______   / __ \\____  _______  ______ ___  ___  ____  / /_   / ____/__  ____  ___  _________ _/ /_____  _____\r\n  \\__ \\/ __ \\/ _ \\/ ___/ __/ __ `/ ___/ / / / / __ `/ ___/  / / / / __ \\/ ___/ / / / __ `__ \\/ _ \\/ __ \\/ __/  / / __/ _ \\/ __ \\/ _ \\/ ___/ __ `/ __/ __ \\/ ___/\r\n ___/ / /_/ /  __/ /__/ /_/ /_/ / /__/ /_/ / / /_/ / /     / /_/ / /_/ / /__/ /_/ / / / / / /  __/ / / / /_   / /_/ /  __/ / / /  __/ /  / /_/ / /_/ /_/ / /    \r\n/____/ .___/\\___/\\___/\\__/\\__,_/\\___/\\__,_/_/\\__,_/_/     /_____/\\____/\\___/\\__,_/_/ /_/ /_/\\___/_/ /_/\\__/   \\____/\\___/_/ /_/\\___/_/   \\__,_/\\__/\\____/_/     \r\n    /_/                                                                                                                                                         \r\n");
        }

        public static void PrintCommands()
        {
            Console.WriteLine();
            foreach (Command command in Global.Commands)
            {
                if (!command.Hidden())
                {
                    Console.Write(command.GetPrefix());
                    Console.WriteLine("\u001B[34m" + command.GetIdentifier() + "\u001B[0m" + " - " + command.GetDescription());
                }
            }
            Console.WriteLine();
        }

        public static void PrintEnterCommand()
        {
            Console.Write("> ");
        }

        public static async Task PrintLoggedInUser()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", Global.AccessToken);

                    HttpResponseMessage response = await client.GetAsync(Global.API_DOMAIN + "api/private/user");

                    if (!response.IsSuccessStatusCode)
                    {
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                // Handle exception
            }
        }
    }
}
