using SpectactularCLI.Commands;

namespace SpectactularCLI.Utilities
{
    public class Output
    {
        public static void PrintWelcomeBanner()
        {
            // Implement welcome banner printing
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
            Console.Write("");
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
