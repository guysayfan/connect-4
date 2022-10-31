using System.Drawing;

namespace connect_4
{
    public partial class connect4 : Form
    {
        const int offset = 200;
        public connect4()
        {
            InitializeComponent();
        }

        private void connect4_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            var w = this.Width;
            var h = this.Height;
            var p = new Pen(Color.Black, 5);
            g.DrawRectangle(p, offset, offset, w - offset*2, h - offset*2);
            var f = new Font(FontFamily.GenericSansSerif, 20);
            var b = new SolidBrush(Color.Tan);
            g.DrawString(this.Width.ToString(), f, b, 50, 50);
        }
    }
}