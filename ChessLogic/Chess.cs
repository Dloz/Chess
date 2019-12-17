using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Chess {
        private Board _board;
        public string Fen { get; private set; }
        private Moves moves;
        List<FigureMoving> allMoves;

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1") {
            Fen = fen;
            _board = new Board(fen);
            moves = new Moves(_board);
        }

        private Chess(Board board) {
            _board = board;
            Fen = board.Fen;
            moves = new Moves(_board); 
        }

        public Chess Move(string move) {

            var fm = new FigureMoving(move);
            //if (!moves.CanMove(fm))
            //    return this; // Cannot move exceptions TODO
            var nextBoard = _board.Move(fm);
            var nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public char GetFigureAt(int x, int y) {
            var square = new Square(x,y);
            var figure = _board.GetFigureAt(square);
            return figure == Figure.none ? '.' : (char)figure;
        }

        public char GetFigureAt(string xy) {
            var square = new Square(xy);
            var figure = _board.GetFigureAt(square);
            return figure == Figure.none ? '.' : (char)figure;
        }

        public IEnumerable<string> YieldValidMoves()
        {
            foreach (FigureOnSquare fs in _board.YieldFigures())
                foreach (Square to in Square.YieldSquares())
                {
                    FigureMoving fm = new FigureMoving(fs, to);
                    if (moves.CanMove(fm))
                        yield return fm.ToString();
                }
        }

        private void FindAllMoves() {
            allMoves = new List<FigureMoving>();
            foreach (var fs in _board.YieldFigures()) {
                foreach (var to in Square.YieldSquares()) {
                    var fm = new FigureMoving(fs, to);
                    if (!moves.CanMove(fm)) continue;
                    if(!_board.IsCheckAfterMove(fm))
                        allMoves.Add(fm);
                }
            }
        }

        public List<string> GetAllMoves() {
            FindAllMoves();
            var list = new List<string>();
            foreach (var fm in allMoves) {
                list.Add(fm.ToString());
            }

            return list;
        }

        public bool IsCheck() {
            return _board.IsCheck();
        }
    }
}
