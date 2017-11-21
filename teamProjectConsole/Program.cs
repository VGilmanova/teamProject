using DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegrammClient;
using System.Threading;

namespace teamProjectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repo = new Repository();
            Client client = new Client(repo);
            Console.WriteLine("Bot is active!");
            Console.WriteLine("Press 1 to get number of active players and 2 to stop working");
            string users = Console.ReadLine();
            int choice = 0;
            bool isWorking = true;
            while (isWorking)
            {
                if (int.TryParse(users, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine(String.Format("Number of active players: {0}", repo.GetActivePlayers()));
                            break;
                        case 2:
                            Console.WriteLine("Goodbye");
                            Thread.Sleep(1500);
                            isWorking = false;
                            break;
                        default:
                            Console.WriteLine("Can't understand this command");
                            break;
                    }
                }
                else
                    Console.WriteLine("Can't understand this command");
                if (isWorking)
                {
                    Console.WriteLine("Press 1 to get number of active players and 2 to stop working");
                    users = Console.ReadLine();
                }
            }
        }
    }
}
