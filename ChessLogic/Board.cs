using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic {
    class Board {
        Figure[,] figures;

        public string Fen { get; private set; }
        public Color moveColor { get; private set; }
        public int MoveNumber { get; private set; }

        public Board(string fen) {
            Fen = fen;
            this.figures = new Figure[8,8];
            Init();
        }

        private void Init() {
            var parts = Fen.Split();
            if (parts.Length != 6) return;
            InitFigures(parts[0]);
            moveColor = (parts[1] == "b") ? Color.black : Color.white;
            MoveNumber = int.Parse(parts[5]);
        }

        private void InitFigures(string data) {
            for (int j = 8; j >= 2; j--) 
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            data = data.Replace("1", ".");
            var lines = data.Split('/');
            for (var y = 7; y >= 0; y--)
            for (var x = 0; x < 8; x++)
                figures[x, y] = lines[7 - y][x] == '.' ? Figure.none :
                    (Figure)lines[7-y][x];
        }

        void GenerateFen() {
            Fen = FenFigures() + " "
                                + (moveColor == Color.white ? "w" : "b") + " - - 0 "
                                + MoveNumber.ToString();
        }

        string FenFigures() {
            var sb = new StringBuilder();
            for (var y = 7; y >= 0; y--) {
                for (var x = 0; x < 8; x++)
                    sb.Append(figures[x, y] == Figure.none ? '1' : (char) figures[x, y]);
                if (y > 0)
                    sb.Append('/');
            }

            const string eight = "11111111";

            for (var j = 8; j >= 2; j--) {
                sb.Replace(eight.Substring(0, j), j.ToString());
            }
            return sb.ToString();
        }

        public Figure GetFigureAt(Square square) {
            return square.OnBoard() ? figures[square.x, square.y] : Figure.none;
        }

        private void SetFigureAt(Square square, Figure figure) {
            if (square.OnBoard()) {
                figures[square.x, square.y] = figure;
            }
        }

        public Board Move(FigureMoving figure) {

            var newBoard = new Board(Fen);
            newBoard.SetFigureAt(figure.From, Figure.none);
            newBoard.SetFigureAt(figure.To, figure.Promotion == Figure.none ? figure.Figure : figure.Promotion);
            if (moveColor == Color.black)
                newBoard.MoveNumber++;
            newBoard.moveColor = moveColor.FlipColor();
            newBoard.GenerateFen(); 
            return newBoard;

        }

        public IEnumerable<FigureOnSquare> YieldFigures() {
            return from Square square in Square.YieldSquares() 
                   where GetFigureAt(square).GetColor() == moveColor 
                   select new FigureOnSquare(GetFigureAt(square), square);
        }

        public bool IsCheck() {
            var after = new Board(Fen);
            after.moveColor = moveColor.FlipColor();
            return after.CanEatKing();
        }

        public bool IsCheckAfterMove(FigureMoving fm) {
            var after = Move(fm);
            return after.CanEatKing();

        }
        private bool CanEatKing() {
            var badKing = FindBadKing();
            var moves = new Moves(this);
            return YieldFigures().Select(fs => new FigureMoving(fs, badKing)).Any(fm => moves.CanMove(fm));
        }

        private Square FindBadKing() {
            var badKing = moveColor == Color.black ? Figure.whiteKing : Figure.blackKing;
            foreach (var square in Square.YieldSquares()) {
                if (GetFigureAt(square) == badKing)
                    return square;
            }

            return Square.none;
        }
    }
}
