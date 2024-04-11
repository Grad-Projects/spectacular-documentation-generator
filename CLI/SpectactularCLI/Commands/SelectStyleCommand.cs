using SpectactularCLI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectactularCLI.Commands
{
    public class SelectStyleCommand : Command
    {
        public SelectStyleCommand() : base("S", "Select Style")
        {
        }

        public override async Task<bool> Execute()
        {
            int index = 1;
            int choice = -1;
            do
            {
                foreach (var StyleOption in Global.StyleList)
                {
                    Console.WriteLine($"{index}\t{StyleOption}");
                };

                string input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                {
                    Global.SelectedStyle = Global.StyleList[choice - 1];
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer index.");
                }
            }
            while (choice >= 0 && choice < Global.StyleList.Count);

            Console.WriteLine(GetDescription());
            return await base.Execute();
        }
    }
}
