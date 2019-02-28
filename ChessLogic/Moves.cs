using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic {
    class Moves {
        private FigureMoving fm;
        private Board board;

        public Moves(Board board) {
            this.board = board;
        }

        public bool CanMove(FigureMoving fm) {
            this.fm = fm;
            return
                CanMoveFrom()
                && CanMoveTo()
                && CanFigureMove()
                && !board.IsCheckAfterMove(fm);
        }

        bool CanMoveFrom() {
            return fm.From.OnBoard() &&
                   fm.Figure.GetColor() == board.moveColor;
        }

        bool CanMoveTo() {
            return fm.To.OnBoard() &&
                   fm.From != fm.To &&
                   board.GetFigureAt(fm.To).GetColor() != board.moveColor;
        }

        private bool CanFigureMove() {
            switch (fm.Figure) {

                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();

                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();

                case Figure.whiteRook:
                case Figure.blackRook:
                    return (fm.SignX == 0 || fm.SignY == 0) &&
                        CanStraightMove();

                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return (fm.SignX != 0 && fm.SignY != 0) &&
                           CanStraightMove();

                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanKnightMove();

                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPownMove();

                default: return false;
            }
        }

        private bool CanPownMove() {
            if (fm.From.y < 1 || fm.From.y > 6)
                return false;
            var stepY = fm.Figure.GetColor() == Color.white ? 1 : -1;
            return
                CanPownGo(stepY) ||
                CanPownJump(stepY) ||
                CanPownEat(stepY);
        }

        private bool CanPownEat(int stepY) {
            if (board.GetFigureAt(fm.To) != Figure.none)
                if (fm.AbsDeltaX == 1)
                    if (fm.DeltaY == stepY)
                        return true;
            return false;
        }

        private bool CanPownJump(int stepY) {

            if (board.GetFigureAt(fm.To) == Figure.none)
                if(fm.DeltaX == 0)
                    if (fm.DeltaY == 2 * stepY)
                        if (fm.From.y == 1 || fm.From.y == 6)
                            if (board.GetFigureAt(new Square(fm.From.x, fm.From.y + stepY)) == Figure.none)
                        return true;
            return false;
        }

        private bool CanPownGo(int stepY) {
            if (board.GetFigureAt(fm.To) == Figure.none)
                if(fm.DeltaX == 0)
                    if (fm.DeltaY == stepY)
                        return true;
            return false;
        }

        private bool CanStraightMove() {
            Square at = fm.From;
            do {
                at = new Square(at.x + fm.SignX, at.y + fm.SignY);
                if (at == fm.To)
                    return true;
            } while (at.OnBoard() &&
                     board.GetFigureAt(at) == Figure.none);
            return false;
        }

        private bool CanKingMove() {
            return fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1;
        }

        private bool CanKnightMove() {
            if (fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2) return true;
            if (fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1) return true;
            return false;
        }
    }
}
