using DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegrammClient;

namespace teamProjectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repo = new Repository();
            Client client = new Client(repo);
            Console.WriteLine();
        }
    }
}
