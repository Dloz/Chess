using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLogic;

namespace ChessDemo {
    class Program {
        static void Main(string[] args) {
            Random random = new Random();
            var chess = new Chess();
            chess.GetFigureAt("");
            List<string> moves;
            while (true) {
                moves = chess.GetAllMoves();
                Console.WriteLine(chess.Fen);
                Console.WriteLine(chess.IsCheck() ? "Check" : "");
                Console.WriteLine(ChessToAscii(chess));
                foreach (var item in moves) {
                    Console.Write(item + "\t");
                }
                Console.WriteLine();
                Console.Write("> ");
                var move = Console.ReadLine();
                if (move == "q") break;
                if (move == "") move = moves[random.Next(moves.Count)];

                
                chess = chess.Move(move);
            }
        }

        static string ChessToAscii(Chess chess) {
            var text = new StringBuilder("  +----------------+\n");
            for (int y = 7; y >= 0; y--) {
                text.Append(y + 1);
                text.Append(" | ");
                for (int x = 0; x < 8; x++) 
                    text.Append(chess.GetFigureAt(x, y) + " ");
                text.Append("\n");

            }
            text.Append("  +----------------+\n");
            text.Append("    a b c d e f g h");
            return text.ToString();
        }
    }
}
