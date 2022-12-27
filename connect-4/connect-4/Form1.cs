using connect_4_core;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace connect_4
{
    public partial class connect4 : Form
    {
        const int penWidth = 4;
        const int offset = 30;
        const int cellOffset = 8;
        const int cellSize = 80;

        Pen penRed = new Pen(Color.Red, penWidth);
        Pen penDarkBlue = new Pen(Color.DarkBlue, penWidth);
        Brush brushBlue = new SolidBrush(Color.Blue);
        Brush brushWhite = new SolidBrush(Color.White);
        Brush brushRed = new SolidBrush(Color.Red);
        Brush brushYellow = new SolidBrush(Color.Yellow);

        IGameEngine engine = new GameEngine();

        IBoard board = null;


        public connect4()
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
        }

        private void connect4_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);

            g.DrawRectangle(penRed, offset, offset, ClientSize.Width - (2 * offset + penWidth), ClientSize.Height - (2 * offset + penWidth));
            drawBoard(g);

        }

        private void drawCell(Graphics g, Pen p, int x, int y, Brush color)
        {
            g.FillRectangle(brushBlue, offset + x, offset + y, cellSize, cellSize);
            g.FillEllipse(color, offset + x + cellOffset, offset + y + cellOffset, cellSize - cellOffset * 2, cellSize - (cellOffset * 2));
            g.DrawEllipse(p, offset + x + cellOffset, offset + y + cellOffset, cellSize - cellOffset * 2, cellSize - (cellOffset * 2));
        }

        private void drawBoard(Graphics g)
        {
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 6; row++)
                {
                    var player = board.GetPlayer(col, row);
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
            var col = getClickedCol(e);

            if (col != -1 && !board.IsColFull(col))
            {
                var row = board.DropPiece(col, engine.GetActivePlayer());
                var cell = calcCellRect(row, col);
                Invalidate(cell);
            }
        }

        private int getClickedCol(MouseEventArgs e)
        {
            var clickX = e.Location.X - (2 * offset + penWidth);

            for (int col = 0; col < 7; col++)
            {
                if (clickX > cellSize * col && clickX < cellSize * (col + 1)) {
                    return col;
                }
            }
            return -1;
        }

        private Rectangle calcCellRect(int row, int col)
        {
            var x = col * cellSize + penWidth + 2 * offset;
            var y = row * cellSize + penWidth + 2 * offset;
            return new Rectangle(x, y, cellSize - cellOffset, cellSize - cellOffset);
        }
    }
}