using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using Vishivator;

namespace Vishivator2._0 {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);

        Board board;
        List<Letter> letters;
        GeneratedWindow gw = null;
        List<OrnamentFounding> foundings;
        List<Shirt> shirts;
        public MainWindow() {
            InitializeComponent();
            letters = new List<Letter>();
            foundings = new List<OrnamentFounding>();
            shirts = new List<Shirt>();
            var lettersUnformatted = File.ReadAllLines("letters.txt", Encoding.UTF8);
            for (int i = 0; i < lettersUnformatted.Length; i++) {
                var lunf = lettersUnformatted[i].Split('|');
                letters.Add(new Letter(lunf[0][0], lunf[1].Split('-'), lunf[2].Split('-')));
            }
            board = new Board(letters);
            var ornaments = Directory.GetFiles("ornaments/", "*.json");
            string tempstr;
            for (int i = 0; i < ornaments.Length; i++) {
                tempstr = File.ReadAllText(ornaments[i]); ;
                foundings.Add(JsonConvert.DeserializeObject<OrnamentFounding>(tempstr));
                typeBox.Items.Add(foundings[i].name);
            }
            var shirtFiles = Directory.GetFiles("shirts/", "*.json");
            for (int i = 0; i < shirtFiles.Length; i++) {
                tempstr = File.ReadAllText(shirtFiles[i]);
                shirts.Add(JsonConvert.DeserializeObject<Shirt>(tempstr));
                shirtBox.Items.Add(shirts[i].name);
            }
            //gw.gen
            //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            //panel1.BackgroundImageLayout = ImageLayout.Zoom;
            typeBox.SelectedIndex = 0;
            shirtBox.SelectedIndex = 0;
            Console.WriteLine("Found " + foundings.Count + " items.");
        }


        public static Bitmap RotateImg(Bitmap bmp, float angle, Color bkColor) {
            int w = bmp.Width;
            int h = bmp.Height;
            PixelFormat pf = default(PixelFormat);
            if (bkColor == Color.Transparent) {
                pf = PixelFormat.Format32bppArgb;
            } else {
                pf = bmp.PixelFormat;
            }

            Bitmap tempImg = new Bitmap(w, h, pf);
            Graphics g = Graphics.FromImage(tempImg);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 1, 1);
            g.Dispose();

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            Matrix mtrx = new Matrix();
            //Using System.Drawing.Drawing2D.Matrix class 
            mtrx.Rotate(angle);
            RectangleF rct = path.GetBounds(mtrx);
            Bitmap newImg = new Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height), pf);
            g = Graphics.FromImage(newImg);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tempImg, 0, 0);
            g.Dispose();
            tempImg.Dispose();
            return newImg;
        }

        public static System.Windows.Media.Imaging.BitmapSource GetImageStream(System.Drawing.Image myImage) {
            var bitmap = new Bitmap(myImage);
            IntPtr bmpPt = bitmap.GetHbitmap();
            System.Windows.Media.Imaging.BitmapSource bitmapSource =
             System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                   bmpPt,
                   IntPtr.Zero,
                   Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            //freeze bitmapSource and clear memory to avoid memory leaks
            bitmapSource.Freeze();
            DeleteObject(bmpPt);

            return bitmapSource;
        }

        private void CreateOrnament_Click(object sender, EventArgs e) {
            if(gw != null) {
                gw.Close();
            }
            gw = new GeneratedWindow();
            var fix = typeBox.SelectedIndex;
            var six = shirtBox.SelectedIndex;
            string text = nameBox.Text.ToLower().Trim();
            if (text.IndexOf(' ') >= 0) {
                MessageBox.Show("Please, remove spaces!");
                return;
            }
            if (text.Length == 0) {
                MessageBox.Show("Please, enter a word!");
                return;
            }
            board.Clear();
            board.setWord(text);
            var imageSize = 609;
            var bsize = imageSize / (board.getMaxValue() * 2);
            var spoint = imageSize / 2;
            SolidBrush redBrush = new SolidBrush(Color.FromArgb(255, foundings[fix].redColor.R, foundings[fix].redColor.G, foundings[fix].redColor.B));
            SolidBrush blackBrush = new SolidBrush(Color.FromArgb(255, foundings[fix].blackColor.R, foundings[fix].blackColor.G, foundings[fix].blackColor.B));
            SolidBrush grayBrush = new SolidBrush(Color.Gray);

            var bmp = new Bitmap(imageSize,imageSize);
            Graphics graphics = Graphics.FromImage(bmp);
            gw.bgnd.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(foundings[fix].backgroundColor.R, foundings[fix].backgroundColor.G, foundings[fix].backgroundColor.B));
            graphics.Clear(Color.Transparent);
            foreach (var p in board.points) {
                graphics.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint + p.X * bsize, spoint - (p.Y + 1) * bsize, bsize, bsize)); // 1
                graphics.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint - (p.Y + 1) * bsize, spoint - (p.X + 1) * bsize, bsize, bsize)); // 2
                graphics.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint - (p.X + 1) * bsize, spoint + (p.Y) * bsize, bsize, bsize)); // 3
                graphics.FillRectangle(p.Color == 1 ? redBrush : blackBrush, new Rectangle(spoint + (p.Y) * bsize, spoint + p.X * bsize, bsize, bsize)); // 4
            }
            var res = bmp.Clone();
            for (int i = 0; i < 32; i++) {
                graphics.FillRectangle(grayBrush, new Rectangle(spoint + bsize * i, 0, 1, imageSize));
                graphics.FillRectangle(grayBrush, new Rectangle(0, spoint + bsize * i, imageSize, 1));
                graphics.FillRectangle(grayBrush, new Rectangle(spoint - bsize * i, 0, 1, imageSize));
                graphics.FillRectangle(grayBrush, new Rectangle(0, spoint - bsize * i, imageSize, 1));
                graphics.FillRectangle(grayBrush, new Rectangle(spoint - bsize * i, 0, 1, imageSize));
                graphics.FillRectangle(grayBrush, new Rectangle(0, spoint + bsize * i, imageSize, 1));
                graphics.FillRectangle(grayBrush, new Rectangle(spoint + bsize * i, 0, 1, imageSize));
                graphics.FillRectangle(grayBrush, new Rectangle(0, spoint - bsize * i, imageSize, 1));
            }
            //panel1.BackgroundImage = bmp;
            System.Drawing.Image orig = Bitmap.FromFile("ornaments/" + foundings[fix].imageFile);
            System.Drawing.Image shirt = Bitmap.FromFile("shirts/" + shirts[six].imageFile);
            Graphics gp = Graphics.FromImage(orig);
            Graphics gs = Graphics.FromImage(shirt);
            for (int i = 0; i < foundings[fix].ornamentPlaces.Count; i++) {
                Bitmap resized = new Bitmap(((Bitmap)res), new System.Drawing.Size((int)(foundings[fix].ornamentPlaces[i].width / Math.Sqrt(2)), (int)(foundings[fix].ornamentPlaces[i].width / Math.Sqrt(2))));
                resized = RotateImg(resized, foundings[fix].ornamentPlaces[i].rotate, Color.Transparent);
                gp.DrawImage(resized, new System.Drawing.Point((int)foundings[fix].ornamentPlaces[0].centerPos.X - resized.Width / 2, (int)foundings[fix].ornamentPlaces[0].centerPos.Y - resized.Height / 2));
            }
            for (int i = 0; i < shirts[six].regions.Count; i++) {
                float scale = shirts[six].regions[i].width * 1.0f / orig.Width;
                Bitmap resized = new Bitmap(orig, new System.Drawing.Size((int)(orig.Width * scale), (int)(orig.Height * scale)));
                TextureBrush tBrush = new TextureBrush(resized);
                tBrush.RotateTransform(shirts[six].regions[i].rotate);
                var r = new GraphicsPath();
                gs.FillPolygon(tBrush, shirts[six].regions[i].dots.ToArray(), FillMode.Winding);
            }
            gw.gen.Source = GetImageStream(bmp);
            gw.orn.Source = GetImageStream(orig);
            gw.shirt.Source = GetImageStream(shirt);
            nameBox.Text = text.ToUpper();
            bmp.Save(text + ".bmp");
            orig.Save(text + fix + ".bmp");
            gw.Show();
            //pictureBox1.Image = orig;
            //pictureBox2.Image = shirt;
        }
    }
}
