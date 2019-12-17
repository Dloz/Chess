using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessClient
{
    public class Client
    {
        public string host { get; private set; }
        public string user { get; private set; }

        int CurrentGameId;

        public Client(string host, string user)
        {
            this.host = host;
            this.user = user;
            GetCurrentGame();
        }

        public GameInfo GetCurrentGame()
        {
            GameInfo game = new GameInfo(ParseJson(CallServer()));
            CurrentGameId = game.GameId;
            return game;
        }

        public GameInfo SendMove(string move)
        {
            string json = CallServer(CurrentGameId + "/" + move);
            var list = ParseJson(json);
            GameInfo game = new GameInfo(list);
            return game;
        }

        private string CallServer(string param = "")
        {
            WebRequest request = WebRequest.Create(host + user + "/" + param);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        private NameValueCollection ParseJson(string json)
        {
            var dict = new Dictionary<string, string>();
            NameValueCollection list = new NameValueCollection();
            string pattern = @"""(\w+)\"":""?([^,""}]*)""?";
            foreach (Match match in Regex.Matches(json, pattern))
            {
                if (match.Groups.Count == 3)
                {
                    dict[match.Groups[1].Value] = match.Groups[2].Value;

                    list[match.Groups[1].Value] = match.Groups[2].Value;
                }
            }
            return list;
        }
    }
}
