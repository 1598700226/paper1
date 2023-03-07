using Emgu.CV.Structure;
using Emgu.CV;
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
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using sift.common;

namespace sift
{
    public partial class MainForm : Form
    {
        /** roi **/
        public PictureBox graphicsPicBox;
        public Boolean isSelectRoi = false;
        public List<Point> roiVertex = new List<Point>();
        /** pic**/
        string[] picturePath;
        public float picScalaFactor = 1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            graphicsPicBox = new PictureBox
            {
                Size = firstPicBox.Size,
                Location = new Point(0,0),
                BackColor = Color.Transparent,
                Visible = true,
                Name = "graphicsPicBox",
                Parent = firstPicBox
            };
            graphicsPicBox.BringToFront();
            graphicsPicBox.MouseClick += new MouseEventHandler(graphicsPicBoxMouseClick);
            graphicsPicBox.MouseMove += new MouseEventHandler(graphicsPicBoxMouseMove);
        }

        private void graphicsPicBoxMouseClick(object sender, MouseEventArgs e)
        {
            if (!isSelectRoi) {
                return;
            }

            if (e.Button == MouseButtons.Left) {
                roiVertex.Add(e.Location);
            }

            if (e.Button == MouseButtons.Right) {
                roiVertex.Add(e.Location);
                isSelectRoi = false;
                if (roiVertex.Count < 3) {
                    return;
                }
                Bitmap graphicBitmap = new Bitmap(graphicsPicBox.Width, graphicsPicBox.Height);
                using (Graphics g = Graphics.FromImage(graphicBitmap))
                {
                    Pen pen = new Pen(Color.Red, 3);
                    g.DrawLines(pen, roiVertex.ToArray());
                    g.DrawLine(pen, roiVertex[0], roiVertex[roiVertex.Count - 1]);
                    graphicsPicBox.Image = graphicBitmap;
                }
            }
        }

        private void graphicsPicBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (!isSelectRoi)
            {
                return;
            }
            if (roiVertex.Count == 0)
            {
                return;
            }

            Bitmap graphicBitmap = new Bitmap(graphicsPicBox.Width, graphicsPicBox.Height);
            using (Graphics g = Graphics.FromImage(graphicBitmap)) {
                Pen pen = new Pen(Color.Red, 3);
                g.DrawLine(pen, roiVertex[0], e.Location);
                g.DrawLine(pen, roiVertex[roiVertex.Count - 1], e.Location);
                if (roiVertex.Count > 1)
                {
                    g.DrawLines(pen, roiVertex.ToArray());
                }
                graphicsPicBox.Image = graphicBitmap;
            }
        }

        private void firstPicOpenBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();  //ofd类
            ofd.Title = "获取图片";  //窗口名
            ofd.InitialDirectory = @"D:\"; //打开的路径
            ofd.Multiselect = true;  //是否允许多选
            ofd.Filter = "所有文件|*.*"; //支持的文件格式
            ofd.ShowDialog();  //打开选择窗口
            picturePath = ofd.FileNames;  //将选择的图片的路径存储到picturePath
            if (picturePath.Length != 2) {
                MessageBox.Show("必须选择2副图片！");
                return;
            }

            Bitmap picture1 = new Bitmap(picturePath[0]);  //显示第一张图片
            firstPicBox.Height = (int)((float)firstPicBox.Width / picture1.Width * picture1.Height);
            firstPicBox.Image = picture1;
            picScalaFactor = (float)firstPicBox.Width / picture1.Width;
            graphicsPicBox.Size = firstPicBox.Size;
            Bitmap picture2 = new Bitmap(picturePath[1]);
            secondPicBox.Height = (int)((float)secondPicBox.Width / picture2.Width * picture2.Height);
            secondPicBox.Image = picture2;
        }

        private void roiSelect_Click(object sender, EventArgs e)
        {
            isSelectRoi = true;
            roiVertex.Clear();
            //
        }

        private void siftBtn_Click(object sender, EventArgs e)
        {
            Sift.siftEmgu(picturePath[0], picturePath[1], null);
        }

        private void sparkBtn_Click(object sender, EventArgs e)
        {
            SparkUtils sparkUtils = new SparkUtils(firstPicBox);
            sparkUtils.createSimulateSparks();
        }

        private void saveLeftPicBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.FileName = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "图片"; //设置默认文件名
            dialog.Filter = "图片(*.bmp)|*.bmp";
            dialog.DefaultExt = "bmp";                                 //设置默认格式（可以不设）
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                firstPicBox.Image.Save(dialog.FileName);
            }
        }
    }
}
