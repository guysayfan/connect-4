using System.Drawing;

namespace connect_4
{
    public partial class connect4 : Form
    {
        const int penWidth = 4;
        const int offset = 30;
        const int cellOffset = 8;
        const int cellSize = 58;
        const int frameX = 3;
        const int frameY = 15;

        Pen penRed = new Pen(Color.Red, penWidth);
        Pen penDarkBlue = new Pen(Color.DarkBlue, penWidth);
        Brush brushBlue = new SolidBrush(Color.Blue);
        Brush brushWhite = new SolidBrush(Color.White);

        public connect4()
        {
            InitializeComponent();
            ResizeRedraw = true;

        }

        private void connect4_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);
            
            g.DrawRectangle(penRed, offset, offset, Width - (offset + penWidth + frameX) * 2, Height - (offset + penWidth + frameY) * 2);
            drawBoard(g);
        }

        private void drawCell(Graphics g, Pen p, int x, int y)
        {
            g.FillRectangle(brushBlue, offset + x, offset + y, cellSize, cellSize);
            g.FillEllipse(brushWhite, offset + x + cellOffset, offset + y + cellOffset, cellSize - cellOffset * 2, cellSize - (cellOffset * 2));
            g.DrawEllipse(p, offset + x + cellOffset, offset + y + cellOffset, cellSize - cellOffset * 2, cellSize - (cellOffset * 2));
        }

        private void drawBoard(Graphics g)
        {

            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 6; row++)
                {
                    drawCell(g, penDarkBlue, col * cellSize + offset, row * cellSize + offset);
                }
            }
            //g.FillPolygon(penDarkBlue, standPoints);
        }

        private void connect4_Load(object sender, EventArgs e)
        {

        }
    }
}