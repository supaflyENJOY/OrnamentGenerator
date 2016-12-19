using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vishivator {
    public partial class MainForm : Form {
        Board board;
        List<Letter> letters;

        public MainForm() {
            InitializeComponent();
            letters = new List<Letter>();
            var lettersUnformatted = File.ReadAllLines("letters.txt",Encoding.UTF8);
            for(int i=0; i < lettersUnformatted.Length; i++) {
                var lunf = lettersUnformatted[i].Split('|');
                letters.Add(new Letter(lunf[0][0], lunf[1].Split('-'), lunf[2].Split('-')));
            }
            board = new Board(letters);
        }

        private void CreateOrnament_Click(object sender, EventArgs e) {
            string text = inputWord.Text.ToLower().Trim();
            this.Height = 734;
            if(text.IndexOf(' ') >= 0) {
                MessageBox.Show("Please, remove spaces!");
                return;
            }
            if (text.Length == 0) {
                MessageBox.Show("Please, enter a word!");
                return;
            }
            board.Clear();
            board.setWord(text);
            var bsize = panel1.Width / (board.getMaxValue() * 2);
            var spoint = panel1.Width / 2;
            SolidBrush redBrush = new SolidBrush(Color.Red);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.LightGray);
            Graphics graphics = panel1.CreateGraphics();
            graphics.Clear(Color.LightSlateGray);

            foreach (var p in board.points) {
                graphics.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint + p.X * bsize, spoint - (p.Y + 1) * bsize, bsize, bsize)); // 1
                graphics.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint - (p.Y + 1) * bsize, spoint - (p.X + 1) * bsize, bsize, bsize)); // 2
                graphics.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint - (p.X + 1) * bsize, spoint + (p.Y) * bsize, bsize, bsize)); // 3
                graphics.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint + (p.Y) * bsize, spoint + p.X * bsize, bsize, bsize)); // 4
            }
            for (int i = 0; i < 32; i++) {
                graphics.FillRectangle(whiteBrush, new Rectangle(spoint + bsize  * i, 0, 1, panel1.Width));
                graphics.FillRectangle(whiteBrush, new Rectangle(0, spoint + bsize  * i, panel1.Width, 1));
                graphics.FillRectangle(whiteBrush, new Rectangle(spoint - bsize  * i, 0, 1, panel1.Width));
                graphics.FillRectangle(whiteBrush, new Rectangle(0, spoint - bsize  * i, panel1.Width, 1));
                graphics.FillRectangle(whiteBrush, new Rectangle(spoint - bsize  * i, 0, 1, panel1.Width));
                graphics.FillRectangle(whiteBrush, new Rectangle(0, spoint + bsize  * i, panel1.Width, 1));
                graphics.FillRectangle(whiteBrush, new Rectangle(spoint + bsize  * i, 0, 1, panel1.Width));
                graphics.FillRectangle(whiteBrush, new Rectangle(0, spoint - bsize * i, panel1.Width, 1));
            }
            
            Image bmp = new Bitmap(panel1.Width, panel1.Width);
            using (Graphics g = Graphics.FromImage(bmp)) {
                g.Clear(Color.LightSlateGray);
                foreach (var p in board.points) {
                    g.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint + p.X * bsize, spoint - (p.Y + 1) * bsize, bsize, bsize)); // 1
                    g.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint - (p.Y + 1) * bsize, spoint - (p.X + 1) * bsize, bsize, bsize)); // 2
                    g.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint - (p.X + 1) * bsize, spoint + (p.Y) * bsize, bsize, bsize)); // 3
                    g.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint + (p.Y) * bsize, spoint + p.X * bsize, bsize, bsize)); // 4
                }
                for (int i = 0; i < 32; i++) {
                    g.FillRectangle(whiteBrush, new Rectangle(spoint + bsize * i, 0, 1, panel1.Width));
                    g.FillRectangle(whiteBrush, new Rectangle(0, spoint + bsize * i, panel1.Width, 1));
                    g.FillRectangle(whiteBrush, new Rectangle(spoint - bsize * i, 0, 1, panel1.Width));
                    g.FillRectangle(whiteBrush, new Rectangle(0, spoint - bsize * i, panel1.Width, 1));
                    g.FillRectangle(whiteBrush, new Rectangle(spoint - bsize * i, 0, 1, panel1.Width));
                    g.FillRectangle(whiteBrush, new Rectangle(0, spoint + bsize * i, panel1.Width, 1));
                    g.FillRectangle(whiteBrush, new Rectangle(spoint + bsize * i, 0, 1, panel1.Width));
                    g.FillRectangle(whiteBrush, new Rectangle(0, spoint - bsize * i, panel1.Width, 1));
                }
            }
            text = board.getWord().ToUpper();
            inputWord.Text = text;
            bmp.Save(text+".bmp");
        }
    }
}
