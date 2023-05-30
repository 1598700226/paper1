using Emgu.CV.OCR;
using sift.PointCloudHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sift
{
    public partial class MatchPointShow : Form
    {
        // matchPointResultss 是成对的数量比 bitmaps少1
        public Bitmap[] bitmaps;
        public List<List<MatchPointResult>> MatchPointResultss;

        public int picBoxW;
        public int picBoxH;
        private int picColNum = 3;
        private double scalaWidth = 1.0;
        private double scalaHeight = 1.0;

        public PictureBox graphicsPicBox;

        public MatchPointShow()
        {
            InitializeComponent();
        }

        public MatchPointShow(Bitmap[] bitmaps, List<List<MatchPointResult>> MatchPointResultss, int picBoxW = 200, int picBoxH = 200)
        {
            InitializeComponent();
            this.bitmaps = bitmaps;
            this.MatchPointResultss = MatchPointResultss;
            this.picBoxW = picBoxW;
            this.picBoxH = picBoxH;
            this.scalaWidth = (double)bitmaps[0].Width / (double)picBoxW;
            this.scalaHeight = (double)bitmaps[0].Height / (double)picBoxH;
        }

        private void MatchPointShow_Load(object sender, EventArgs e)
        {
            // 计算拼接的picBox长和宽
            int width = bitmaps.Length <= picColNum ? bitmaps.Length * picBoxW : picColNum * picBoxW;
            int height = bitmaps.Length <= picColNum ? picBoxH : ((bitmaps.Length - 1) / picColNum  + 1)* picBoxH;
            //创建新的拼接后的Bitmap
            Bitmap resultBitmap = new Bitmap(width, height);
            //把原来的多个Bitmap复制到新Bitmap中
            using (Graphics graphics = Graphics.FromImage(resultBitmap))
            {

                for(int i = 0; i < bitmaps.Length; i++) 
                {
                    int x = i % picColNum;
                    int y = i / picColNum;
                    graphics.DrawImage(bitmaps[i], new Rectangle(x * picBoxW, y * picBoxH, picBoxW, picBoxH));
                }
            }
            //把拼接后的Bitmap显示在PictureBox中
            picBox.Image = resultBitmap;

            graphicsPicBox = new PictureBox
            {
                Size = new Size(picBox.Width, picBox.Height),
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Visible = true,
                Name = "PicBoxGraphic",
                Parent = picBox
            };

            showMatchPoint(MatchPointResultss);
        }

        private Point convertGraphicsPixBoxLoc(int index, int x, int y) { 
            int col = index % picColNum;
            int row = index / picColNum;
            double sx = (double)x / scalaWidth;
            double sy = (double)y / scalaHeight;

            Point convertPoint = new Point(x, y);
            convertPoint.X = (int)(sx + col * picBoxW);
            convertPoint.Y = (int)(sy + row * picBoxH);
            return convertPoint;
        }

        private void showMatchPoint(List<List<MatchPointResult>> MatchPointResultss) {
            Bitmap graphicBitmap = new Bitmap(graphicsPicBox.Width, graphicsPicBox.Height);

            Random random = new Random();
            using (Graphics g = Graphics.FromImage(graphicBitmap))
            {
                for (int i = 0; i < MatchPointResultss.Count; i++) {
                    List<MatchPointResult> MatchPointResults = MatchPointResultss[i];
                    foreach (MatchPointResult item in MatchPointResults)
                    {
                        Point startP = convertGraphicsPixBoxLoc(i, (int)item.X, (int)item.Y);

                        int ex = (int)Math.Round(item.match_X, 2);
                        int ey = (int)Math.Round(item.match_Y, 2);
                        Point endP = convertGraphicsPixBoxLoc(i + 1, ex, ey);
                        Pen pen = new Pen(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)), 2);
                        g.DrawLine(pen, startP, endP);
                    }
                }
            }
            graphicsPicBox.Image = graphicBitmap;
        }
    }
}
