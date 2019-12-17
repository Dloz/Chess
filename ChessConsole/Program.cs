using ChessClient;
using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("http://localhost:55565/api/", "Games");
            var ret = client.SendMove("Pe4e5");
        }
    }
}
