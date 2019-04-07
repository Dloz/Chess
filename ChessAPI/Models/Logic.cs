using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess2API.Models
{
    public class Logic
    {
        private ModelChessDB db;
        public Logic()
        {
            db = new ModelChessDB();
        }
        public Game GetCurrentGame()
        {
            Game game = db.Games
                .Where(g => g.Status == "play")
                .OrderBy(g => g.ID)
                .FirstOrDefault();
            if (game == null)
                game = CreateNewGame();
            return game;
        }

        private Game CreateNewGame()
        {
            Game game = new Game();

            Chess chess = new Chess();

            game.FEN = chess.Fen;
            game.Status = "play";

            db.Games.Add(game);
            db.SaveChanges();

            return game;
        }

        internal Game GetGame(int id)
        {
            return db.Games.Find(id);
        }

        public Game MakeMove(int id, string move)
        {
            Game game = GetGame(id);
            if (game == null) return game;

            if (game.Status != "play")
                return game;    

            Chess chess = new Chess(game.FEN);
            Chess chessNext = chess.Move(move);

            if (chessNext.Fen == game.FEN)
                return game;

            game.FEN = chessNext.Fen;

            //if (chessNext.IsCheckMate || chess.IsStalemate)
            //    game.Status = "done"; // TODO

            db.Entry(game).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return game;
        }
    }
}