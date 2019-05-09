using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using ChessClient;

namespace ConsoleChess
{
    class Program
    {
        Client client;
        public const string HOST = ""; // TODO
        public const string USER = "Black";
        static void Main(string[] args)
        {
            Program program = new Program();
                program.Start();
        }

        private void Start()
        {
            void Start()
            {
                client = new Client(HOST, USER);
                client.GetCurrentGame();
                
            }
        }
    }
}
