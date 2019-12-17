using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient
{
    public struct GameInfo
    {
        public int GameId;
        public string FEN;
        public string Status;
        //public string White;
        //public string Black;
        //public string LastMove;
        //public string YourColor;
        //public bool IsYourMove;
        //public string OfferDraw;
        //public string Winner;

        public GameInfo(Dictionary<string, string> dict)
        {
            GameId = int.Parse(dict["GameID"]);
            FEN = dict["FEN"];
            Status = dict["Status"];
            //White = dict["White"];
            //Black = dict["Black"];
            //LastMove = dict["LastMove"];
            //YourColor = dict["YourColor"];
            //IsYourMove = bool.Parse(dict["IsYourMove"]);
            //OfferDraw = dict["OfferDraw"];
            //Winner = dict["Winner"];
        }

        public GameInfo(NameValueCollection list)
        {
            GameId = int.Parse(list["ID"]);
            FEN = list["FEN"];
            Status = list["Status"];
            //White = list["White"];
            //Black = list["Black"];
            //LastMove = list["LastMove"];
            //YourColor = list["YourColor"];
            //IsYourMove = bool.Parse(list["IsYourMove"]);
            //OfferDraw = list["OfferDraw"];
            //Winner = list["Winner"];
        }

        public override string ToString() =>
            "GameId = " + GameId +
            "\nFEN = " + FEN +
            "\nStatus = " + Status; //+
            //"\nWhite = " + White +
            //"\nBlack = " + Black +
            //"\nLastMove = " + LastMove +
            //"\nIsYourMove = " + IsYourMove +
            //"\nOfferDraw = " + OfferDraw +
            //"\nWinner = " + Winner;
    }
}
