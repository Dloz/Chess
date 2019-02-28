using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess {  
    public class Chess {
        string fen;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1") {
            this.fen = fen;
        }

        public Chess Move(string Move) {
            return new Chess(fen);
        }

        public char GetFigureAt(int x, int y) {
            return '.';
        }
    }
}
