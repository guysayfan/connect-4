using connect_4_core;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace connect_4
{
    public partial class Connect4 : Form, IGameEngineEvents, IPlayer
    {
        const int INVALID_COL = 999;

        const int penWidth = 4;
        const int offset = 30;
        const int cellOffset = 8;
        const int cellSize = 80;

        uint clickedCol = INVALID_COL;
        Pen penRed = new Pen(Color.Red, penWidth);
        Pen penDarkBlue = new Pen(Color.DarkBlue, penWidth);
        Brush brushBlue = new SolidBrush(Color.Blue);
        Brush brushWhite = new SolidBrush(Color.White);
        Brush brushRed = new SolidBrush(Color.Red);
        Brush brushYellow = new SolidBrush(Color.Yellow);

        IGameEngine engine = new GameEngine();

        Board board;


        public Connect4()
        {
            board = engine.GetBoard();
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            ResizeRedraw = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;

            var width = 2 * (penWidth + 2 * offset) + 7 * cellSize;
            var height = 2 * (penWidth + 2 * offset) + 6 * cellSize;
            ClientSize = new Size(width, height);

            Task.Run(() => engine.Run(this, new NormalAIPlayer(1, 0), this));
        }

        private void connect4_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);

            g.DrawRectangle(penRed, offset, offset, ClientSize.Width - (2 * offset + penWidth), ClientSize.Height - (2 * offset + penWidth));
            drawBoard(g);

        }

        private void drawCell(Graphics g, Pen p, uint x, uint y, Brush color)
        {
            g.FillRectangle(brushBlue, offset + x, offset + y, cellSize, cellSize);
            g.FillEllipse(color, offset + x + cellOffset, offset + y + cellOffset, cellSize - cellOffset * 2, cellSize - (cellOffset * 2));
            g.DrawEllipse(p, offset + x + cellOffset, offset + y + cellOffset, cellSize - cellOffset * 2, cellSize - (cellOffset * 2));
        }

        private void drawBoard(Graphics g)
        {
            
            for (uint col = 0; col < 7; col++)
            {
                for (uint row = 0; row < 6; row++)
                {
                    var location = new Location(col, row);
                    var player = board.GetPlayer(location);
                    Brush color = brushWhite;
                    if (player == 0)
                    {
                        color = brushRed;
                    } else if (player == 1)
                    {
                        color = brushYellow;
                    }

                    drawCell(g, penDarkBlue, col * cellSize + offset, row * cellSize + offset, color);
                }
            }
        }

        private void connect4_MouseClick(object sender, MouseEventArgs e)
        {
            uint col = getClickedCol(e);

            if (col != INVALID_COL && !board.IsColFull(col))
            {
                clickedCol = col;
            }
        }

        private uint getClickedCol(MouseEventArgs e)
        {
            var clickX = e.Location.X - (2 * offset + penWidth);

            for (uint col = 0; col < 7; col++)
            {
                if (clickX > cellSize * col && clickX < cellSize * (col + 1)) {
                    return col;
                }
            }
            return INVALID_COL;
        }

        private Rectangle calcCellRect(Location location)
        {
            var x = location.Col * cellSize + penWidth + 2 * offset;
            var y = location.Row * cellSize + penWidth + 2 * offset;
            return new Rectangle((int)x, (int)y, cellSize - cellOffset, cellSize - cellOffset);
        }

        public void OnDropPiece(Location location)
        {
            var cell = calcCellRect(location);
            Invalidate(cell);
        }


        public uint Play(Board board)
        {
            while (clickedCol == INVALID_COL) {
                Thread.Sleep(100);
            }

            var result = clickedCol;
            clickedCol = INVALID_COL;
            return result;
        }

        public void OnGameOver(uint winner)
        {
            string message;
            if (winner == 0)
            {
                message = "You win!";
            } else if (winner == 1)
            {
                message = "You lose. :(";
            } else
            {
                message = "It's a tie!";
            }
            MessageBox.Show(message, "GAME OVER");
        }
    }
}