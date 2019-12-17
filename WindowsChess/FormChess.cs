using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessLogic;

namespace WindowsChess
{
    public partial class FormChess : Form
    {
        public const string HOST = "http://localhost:55565/api/"; // TODO
        public const string USER = "Games";
        const int SIZE = 50;
        Panel[,] map;
        Chess Chess;
        ChessClient.Client Client;

        bool wait; // true = no move
        int xFrom, yFrom;

        public FormChess()
        {
            InitializeComponent();
            Client = new ChessClient.Client(HOST, USER);
            InitPanels();
            wait = true;
            Chess = new Chess(Client.GetCurrentGame().FEN);
            ShowPosition();
        }

        private void InitPanels()
        {
            map = new Panel[8, 8];
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    map[x, y] = AddPanel(x, y);
        }

        private void ShowPosition()
        {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    ShowFigure(x, y, Chess.GetFigureAt(x, y));
            MarkSquares();
        }

        private void ShowFigure(int x, int y, char figure)
        {
            map[x, y].BackgroundImage = GetFigureImage(figure);
        }

        private Image GetFigureImage(char figure)
        {
            switch (figure)
            {
                case 'R': return Properties.Resources.WhiteRook;
                case 'N': return Properties.Resources.WhiteKnight;
                case 'B': return Properties.Resources.WhiteBishop;
                case 'Q': return Properties.Resources.WhiteQueen;
                case 'K': return Properties.Resources.WhiteKing;
                case 'P': return Properties.Resources.WhitePawn;

                case 'r': return Properties.Resources.BlackRook;
                case 'n': return Properties.Resources.BlackKnight;
                case 'b': return Properties.Resources.BlackBishop;
                case 'q': return Properties.Resources.BlackQueen;
                case 'k': return Properties.Resources.BlackKing;
                case 'p': return Properties.Resources.BlackPawn;

                default: return null;
            }
        }

        private Panel AddPanel(int x, int y)
        {
            Panel panel = new System.Windows.Forms.Panel();
            panel.BackColor = GetColor(x, y);
            panel.Location = GetLocation(x, y);
            panel.Name = "p" + x + y;
            panel.Size = new System.Drawing.Size(SIZE, SIZE);
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.MouseClick += new System.Windows.Forms.MouseEventHandler(panel_MouseClick);
            Board.Controls.Add(panel);

            return panel;
            
        }

        private Point GetLocation(int x, int y)
        {
            return new Point(SIZE / 2 + x * SIZE, 
                             SIZE / 2 + (7 - y) * SIZE);
        }

        private Color GetColor(int x, int y)
        {
            return (x + y) % 2 == 0 ? Color.DarkGray : Color.White;
        }

        private Color GetMarkedColor(int x, int y)
        {
            return (x + y) % 2 == 0 ? Color.Green : Color.LightGreen;
        }

        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            string xy = ((Panel)sender).Name.Substring(1);
            int x = xy[0] - '0';
            int y = xy[1] - '0';

            if(wait)
            {
                wait = false;
                xFrom = x;
                yFrom = y;
            }
            else
            {
                wait = true;
                // Pe2e4
                string figure = Chess.GetFigureAt(xFrom, yFrom).ToString();
                string move = figure + ToCoordinate(xFrom, yFrom) + ToCoordinate(x, y);

                Chess = new Chess(Client.SendMove(move).FEN);
            }
            ShowPosition();
        } 

        private void MarkSquares()
        {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    map[x, y].BackColor = GetColor(x, y);

            if (wait)
                MarkSquaresFrom();
            else
                MarkSquaresTo();
        }
        private void MarkSquaresFrom()
        {
            foreach (var move in Chess.YieldValidMoves()) // Pe2e4  xy = e2
            {
                int x = move[1] - 'a';
                int y = move[2] - '1';
                map[x, y].BackColor = GetMarkedColor(x, y);
            }
        }

        private void MarkSquaresTo()
        {
            string suffix = Chess.GetFigureAt(xFrom, yFrom) + ToCoordinate(xFrom, yFrom);
            foreach (var move in Chess.YieldValidMoves()) // Pe2e4  xy = e4
                if(move.StartsWith(suffix))
                {
                    int x = move[3] - 'a';
                    int y = move[4] - '1';
                    map[x, y].BackColor = GetMarkedColor(x, y);
                }
        }

        private string ToCoordinate(int x, int y)
        {
            return ((char)('a' + x)).ToString() + 
                   ((char)('1' + y)).ToString();
        }

        private void FormChess_Load(object sender, EventArgs e)
        {

        }

        private void Board_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
