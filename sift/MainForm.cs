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
using sift.Lucas_Kanade;
using sift.kinect;
using Microsoft.Kinect;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using sift.PFH;
using sift.PointCloud;

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
        /** kinect*/
        public CameraOpen cameraOpen = null;

        /**
         * ImaShow窗口
         */
        public List<ImgShow> imgShows = new List<ImgShow>();

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

            dialog.FileName = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + "图片"; //设置默认文件名
            dialog.Filter = "图片(*.bmp)|*.bmp";
            dialog.DefaultExt = "bmp";                                 //设置默认格式（可以不设）
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                firstPicBox.Image.Save(dialog.FileName);
            }
        }

        private void lKFAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> imageRef = new Image<Gray, byte>(picturePath[0]);
            Rectangle rectTemp = new Rectangle(30, 60, 100, 100);
            Image<Gray, byte> imageRefTemp = new Image<Gray, byte>(rectTemp.Size);
            imageRefTemp = imageRef.Copy(rectTemp);

            Bitmap graphicBitmap = new Bitmap(graphicsPicBox.Width, graphicsPicBox.Height);
            Graphics g = Graphics.FromImage(graphicBitmap);
            Rectangle rectDraw = new Rectangle((int)(rectTemp.Left * picScalaFactor), (int)(rectTemp.Top * picScalaFactor), (int)(rectTemp.Width * picScalaFactor), (int)(rectTemp.Height * picScalaFactor));
            g.DrawRectangle(new Pen(Color.Green, 3), rectDraw);
            graphicsPicBox.Image = graphicBitmap; 

            Image<Gray, byte> imageDef = new Image<Gray, byte>(picturePath[1]);
            double[] p = new double[]{0.01, 0.01, 0.0, 0.0, 30.0,60.0};
            ForwardAdditive.method(imageRefTemp, imageDef, p, rectTemp.Width, rectTemp.Height);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*            double[,] dR = new double[,] { { Math.Sqrt(2.0) / 2, Math.Sqrt(2.0) / 2, 0 },
                            { -Math.Sqrt(2.0) / 2, Math.Sqrt(2.0) / 2, 0 },
                        { 0, 0, 1 }};
                        Matrix<double> Rz = new Matrix<double>(dR) ;



                        double[,] pt = new double[,] { { 0, 1, 0 } };
                        double[,] nt = new double[,] { { 0, 1, 0 } };
                        double[,] ps = new double[,] { {1, 0, 0 } };
                        double[,] ns = new double[,] { { 1, 0, 0 } };

                        Matrix<double> mpt = new Matrix<double>(pt);
                        Matrix<double> mps = new Matrix<double>(ps);
                        Matrix<double> mpt_r = mpt.Mul(Rz);
                        Matrix<double> mps_r = mps.Mul(Rz);
                        pt[0, 0] = mpt_r.Data[0, 0];
                        pt[0, 1] = mpt_r.Data[0, 1];
                        pt[0, 2] = mpt_r.Data[0, 2];
                        ps[0, 0] = mps_r.Data[0, 0];
                        ps[0, 1] = mps_r.Data[0, 1];
                        ps[0, 2] = mps_r.Data[0, 2];

                        double f1, f2, f3, f4;
                        PointFetures.pfh(pt, pt, ps, ps, out f1, out f2, out f3, out f4);
                        KdTree.test();
                        SvdRT.testN();*/

            //KdTree.test();
            MatchingAlgorithm.test();
            int a = 0;
        }

        private void openKinectBtn_Click(object sender, EventArgs e)
        {
            if (cameraOpen != null) {
                openKinectBtn.Text = "打开";
                cameraOpen.close();
                cameraOpen = null;
                return;
            }

            cameraOpen = new CameraOpen();
            cameraOpen.Init(firstPicBox, secondPicBox);
            openKinectBtn.Text = "关闭";
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FusionBtn_Click(object sender, EventArgs e)
        {
            if (cameraOpen == null) {
                return;
            }

            lock (cameraOpen.colorPixels) {
                ushort[] udepth = new ushort[cameraOpen.udepth.Length];
                cameraOpen.udepth.CopyTo(udepth, 0);
                ColorSpacePoint[] colorSpacePoints = new ColorSpacePoint[cameraOpen.colorSpacePoints.Length];
                cameraOpen.colorSpacePoints.CopyTo(colorSpacePoints, 0);
                byte[] colorPixels = new byte[cameraOpen.colorPixels.Length];
                cameraOpen.colorPixels.CopyTo(colorPixels, 0);

                int depthWidth = secondPicBox.Image.Width;
                int depthHeight = secondPicBox.Image.Height;
                int colorWidth = firstPicBox.Image.Width;
                int colorHeight = firstPicBox.Image.Height;
                Bitmap colorDepthBitmap = new Bitmap(depthWidth, depthHeight, 0, PixelFormat.Format32bppArgb, IntPtr.Zero);

                Rectangle ret = new Rectangle(0, 0, colorDepthBitmap.Width, colorDepthBitmap.Height);
                BitmapData bitmapData = colorDepthBitmap.LockBits(ret, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                IntPtr ptrSrc = bitmapData.Scan0;
                byte[] dataImg = new byte[colorDepthBitmap.Width * colorDepthBitmap.Height * 4];

                Bitmap bmp = ((Bitmap)firstPicBox.Image);
                for (int i = 0; i < colorSpacePoints.Length; i++)
                {
                    int x = (int)colorSpacePoints[i].X;
                    int y = (int)colorSpacePoints[i].Y;

                    // 直通滤波，限制z范围点云, 单位mm
                    if (udepth[i] < 500 || udepth[i] > 1000) {
                        udepth[i] = 0;
                        dataImg[i * 4] = 255;
                        dataImg[i * 4 + 1] = 255;
                        dataImg[i * 4 + 2] = 255;
                        dataImg[i * 4 + 3] = 255;
                        continue;
                    }

                    if (x <= 0 || x >= colorWidth ||
                        y <= 0 || y >= colorHeight)
                    {
                        dataImg[i * 4] = 255;
                        dataImg[i * 4 + 1] = 255;
                        dataImg[i * 4 + 2] = 255;
                        dataImg[i * 4 + 3] = 255;
                    }
                    else
                    {
                        dataImg[i * 4] = colorPixels[(x + y * colorWidth)*4];
                        dataImg[i * 4 + 1] = colorPixels[(x + y * colorWidth)*4 + 1];
                        dataImg[i * 4 + 2] = colorPixels[(x + y * colorWidth)*4 + 2];
                        dataImg[i * 4 + 3] = colorPixels[(x + y * colorWidth)*4 + 3];
                    }
                }
                Marshal.Copy(dataImg, 0, ptrSrc, dataImg.Length);
                colorDepthBitmap.UnlockBits(bitmapData);

                ImgShow img = new ImgShow(colorDepthBitmap, udepth, cameraOpen.depthCameraIntrinsics, dataImg, this);
                img.Show();
                imgShows.Add(img);
            }
        }

        private void calculateCloudRT_Click(object sender, EventArgs e)
        {
            if (imgShows.Count < 2) {
                return;
            }

            List<PointCloud3D> spointClouds;
            List<PointCloud3D> tpointClouds;
            MatchingAlgorithm.pointCloudMatch(imgShows[0].filterPointCloud3d, imgShows[1].filterPointCloud3d, double.MinValue, out spointClouds, out tpointClouds);

            double[,] r;
            double[] t;   
            SvdRT.RegisterPointCloud(spointClouds, tpointClouds, out r, out t);
        }
    }
}
