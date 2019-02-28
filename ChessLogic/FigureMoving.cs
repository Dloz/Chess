using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic {
    internal class FigureMoving {
        public Figure Figure { get; private set; }
        public Square From { get; private set; }
        public Square To { get; private set; }
        public Figure Promotion { get; private set; }

        public FigureMoving(FigureOnSquare figureOnSquare, Square to, Figure promotion = Figure.none) {
            Figure = figureOnSquare.Figure;
            From = figureOnSquare.Square;
            To = to;
            Promotion = promotion;
        }

        public FigureMoving(string move)
        {
            Figure = (Figure)move[0];
            From = new Square(move.Substring(1, 2));
            To = new Square(move.Substring(3, 2));
            Promotion = (move.Length == 6) ? (Figure) move[5] : Figure.none;
        }

        public int DeltaX => To.x - From.x;
        public int DeltaY => To.y - From.y;

        public int AbsDeltaX => Math.Abs(DeltaX);
        public int AbsDeltaY => Math.Abs(DeltaY);

        public int SignX => Math.Sign(DeltaX);
        public int SignY => Math.Sign(DeltaY);

        public override string ToString() {
            var text = (char)Figure + From.Name + To.Name;
            if (Promotion != Figure.none)
                text += (char) Promotion;
            return text;
        }
    }
}
