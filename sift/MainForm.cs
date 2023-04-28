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
using sift.PointCloudHandler;
using MathNet.Numerics.LinearAlgebra;
using sift.OpenTK;
using System.Runtime.InteropServices.ComTypes;
using sift.SocketApp;

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

        /** dic*/
        public int subsetSize = 21;
        public int dicSearchRange = 50;
        public double limitR = 0.50;

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
        }


        private void closeROIbtn_Click(object sender, EventArgs e)
        {
            roiVertex.Clear();
            graphicsPicBox.Image = null;
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
        private void saveRightPicBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + "图片"; //设置默认文件名
            dialog.Filter = "图片(*.bmp)|*.bmp";
            dialog.DefaultExt = "bmp";                                 //设置默认格式（可以不设）
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                secondPicBox.Image.Save(dialog.FileName);
            }
        }

        private void lKFAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LKMethod.invoke(picturePath[0], picturePath[1], 31, 50, 50, 0, 0, LKMethodName.ICGN);
            LKMethod.invoke(picturePath[0], picturePath[1], 31, 50, 50, 0, 0, LKMethodName.FAGN);
            LKMethod.invoke(picturePath[0], picturePath[1], 31, 50, 50, 0, 0, LKMethodName.FCGN);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SvdRT.testRT();

            MatchingAlgorithm.test();
            PLY.test();
            int a = 0;
            //_3DShow ss = new _3DShow();
            //ss.Show();
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
            cameraOpen.Init(firstPicBox, secondPicBox, this);
            openKinectBtn.Text = "关闭";
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + "图片"; //设置默认文件名
            dialog.Filter = "图片(*.bmp)|*.bmp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                firstPicBox.Image.Save(dialog.FileName + "_RGB.bmp");
                secondPicBox.Image.Save(dialog.FileName + "_Depth.bmp");

                using (StreamWriter writer = new StreamWriter(dialog.FileName + "_config.txt"))
                {
                    
                    writer.WriteLine("fx:" + cameraOpen.depthCameraIntrinsics.FocalLengthX);
                    writer.WriteLine("fy:" + cameraOpen.depthCameraIntrinsics.FocalLengthY);
                    writer.WriteLine("px" + cameraOpen.depthCameraIntrinsics.PrincipalPointX);
                    writer.WriteLine("py" + cameraOpen.depthCameraIntrinsics.PrincipalPointY);
                    writer.WriteLine("k1" + cameraOpen.depthCameraIntrinsics.RadialDistortionSecondOrder);
                    writer.WriteLine("k2" + cameraOpen.depthCameraIntrinsics.RadialDistortionFourthOrder);
                    writer.WriteLine("k2" + cameraOpen.depthCameraIntrinsics.RadialDistortionSixthOrder);
                }
            }
        }

        private void FusionBtn_Click(object sender, EventArgs e)
        {
            if (cameraOpen == null) {
                return;
            }
            
            List<System.Drawing.PointF> roi = new List<System.Drawing.PointF>();
            if (roiVertex.Count > 0) {
                foreach (Point item in roiVertex) {
                    roi.Add(new System.Drawing.PointF(item.X / picScalaFactor, item.Y / picScalaFactor));
                }
            }

            lock (cameraOpen.colorPixels) {
                ushort[] udepth = new ushort[cameraOpen.udepth.Length];
                byte[] bytedepth = new byte[cameraOpen.depthPixels.Length];
                cameraOpen.udepth.CopyTo(udepth, 0);
                cameraOpen.depthPixels.CopyTo(bytedepth, 0);

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
                byte[] oriDataImg = new byte[colorDepthBitmap.Width * colorDepthBitmap.Height * 4];

                Bitmap bmp = ((Bitmap)firstPicBox.Image);

/*                udepth = BitmapExtensions.RadialDistortionCorrection(ref udepth, depthWidth, depthHeight,
                    cameraOpen.depthCameraIntrinsics.RadialDistortionSecondOrder,
                    cameraOpen.depthCameraIntrinsics.RadialDistortionFourthOrder,
                    cameraOpen.depthCameraIntrinsics.RadialDistortionSixthOrder,
                    cameraOpen.depthCameraIntrinsics.PrincipalPointX,
                    cameraOpen.depthCameraIntrinsics.PrincipalPointY,
                    cameraOpen.depthCameraIntrinsics.FocalLengthX,
                    cameraOpen.depthCameraIntrinsics.FocalLengthY);
                bytedepth = BitmapExtensions.RadialDistortionCorrection8byte(ref bytedepth, depthWidth, depthHeight,
                    cameraOpen.depthCameraIntrinsics.RadialDistortionSecondOrder,
                    cameraOpen.depthCameraIntrinsics.RadialDistortionFourthOrder,
                    cameraOpen.depthCameraIntrinsics.RadialDistortionSixthOrder,
                    cameraOpen.depthCameraIntrinsics.PrincipalPointX,
                    cameraOpen.depthCameraIntrinsics.PrincipalPointY,
                    cameraOpen.depthCameraIntrinsics.FocalLengthX,
                    cameraOpen.depthCameraIntrinsics.FocalLengthY);*/

                cameraOpen.kinectSensor.CoordinateMapper.MapDepthFrameToColorSpace(udepth, colorSpacePoints);

                bool[] colorRecords = new bool[colorPixels.Length];
                for (int i = 0; i < colorSpacePoints.Length; i++)
                {
                    int x = (int)colorSpacePoints[i].X;
                    int y = (int)colorSpacePoints[i].Y;

                    if (x >= 0 && x < colorWidth &&
                        y >= 0 && y < colorHeight)
                    {
                        if (colorRecords[(x + y * colorWidth) * 4]) {
                            continue;
                        }

                        oriDataImg[i * 4] = colorPixels[(x + y * colorWidth) * 4];
                        oriDataImg[i * 4 + 1] = colorPixels[(x + y * colorWidth) * 4 + 1];
                        oriDataImg[i * 4 + 2] = colorPixels[(x + y * colorWidth) * 4 + 2];
                        oriDataImg[i * 4 + 3] = colorPixels[(x + y * colorWidth) * 4 + 3];


                        // 直通滤波，限制z范围点云, 单位mm
                        if (udepth[i] < 500 || udepth[i] > 1500)
                        {
                            udepth[i] = 0;
                            dataImg[i * 4] = 255;
                            dataImg[i * 4 + 1] = 255;
                            dataImg[i * 4 + 2] = 255;
                            dataImg[i * 4 + 3] = 255;
                            continue;
                        }


                        // 区域选择
                        if (roiVertex.Count > 0)
                        {
                            if (Algorithm.IsInPolygonF(new System.Drawing.PointF(x, y), roi))
                            {
                                dataImg[i * 4] = colorPixels[(x + y * colorWidth) * 4];
                                dataImg[i * 4 + 1] = colorPixels[(x + y * colorWidth) * 4 + 1];
                                dataImg[i * 4 + 2] = colorPixels[(x + y * colorWidth) * 4 + 2];
                                dataImg[i * 4 + 3] = colorPixels[(x + y * colorWidth) * 4 + 3];
                            }
                            else
                            {
                                udepth[i] = 0;
                                dataImg[i * 4] = 255;
                                dataImg[i * 4 + 1] = 255;
                                dataImg[i * 4 + 2] = 255;
                                dataImg[i * 4 + 3] = 255;
                            }
                        }
                        else
                        {
                                dataImg[i * 4] = colorPixels[(x + y * colorWidth) * 4];
                                dataImg[i * 4 + 1] = colorPixels[(x + y * colorWidth) * 4 + 1];
                                dataImg[i * 4 + 2] = colorPixels[(x + y * colorWidth) * 4 + 2];
                                dataImg[i * 4 + 3] = colorPixels[(x + y * colorWidth) * 4 + 3];
                        }
                        colorRecords[(x + y * colorWidth) * 4] = true;
                    }
                    else {
                        udepth[i] = 0;
                        dataImg[i * 4] = 255;
                        dataImg[i * 4 + 1] = 255;
                        dataImg[i * 4 + 2] = 255;
                        dataImg[i * 4 + 3] = 255;
                    }
                }
                Marshal.Copy(dataImg, 0, ptrSrc, dataImg.Length);
                colorDepthBitmap.UnlockBits(bitmapData);

                ImgShow img = new ImgShow(colorDepthBitmap, udepth, bytedepth, cameraOpen.depthCameraIntrinsics, dataImg, oriDataImg, this);
                img.Show();
                imgShows.Add(img);
            }
        }

        List<MathNet.Numerics.LinearAlgebra.Vector<double>> angle = new List<Vector<double>>();
        private void calculateCloudRT_Click(object sender, EventArgs e)
        {
            angle.Clear();
            if (imgShows.Count < 2)
            {
                return;
            }

            // 配准点
            List<PointCloud3D> matchPointCloud3Ds = imgShows[0].waitMatchPoints;
            // 中间过程,匹配点和对应的RT
            List<List<MatchPointResult>> matchPointResultss = new List<List<MatchPointResult>>();
            List<MathNet.Numerics.LinearAlgebra.Matrix<double>> matchRs = new List<MathNet.Numerics.LinearAlgebra.Matrix<double>>();
            List<MathNet.Numerics.LinearAlgebra.Vector<double>> matchTs = new List<MathNet.Numerics.LinearAlgebra.Vector<double>>();

            for (int i = 0; i < imgShows.Count - 1; i++)
            {
                Image<Gray, Byte> sourecImage = BitmapExtensions.ToGrayImage(imgShows[i].getOriginBitmap());
                Image<Gray, Byte> targetImage = BitmapExtensions.ToGrayImage(imgShows[i + 1].getOriginBitmap());
                List<MatchPointResult> matchPointResults;
                MatchingAlgorithm.PointCloudMatch(matchPointCloud3Ds, sourecImage, targetImage, out matchPointResults, subsetSize, dicSearchRange, limitR);

                List<PointCloud3D> pointCloud3Ds1 = new List<PointCloud3D>();
                List<PointCloud3D> pointCloud3Ds2 = new List<PointCloud3D>();
                for (int pi = 0; pi < matchPointResults.Count; pi++)
                {
                    double pic1_x = matchPointResults[pi].X;
                    double pic1_y = matchPointResults[pi].Y;
                    double pic1_z = imgShows[0].getDepthPixelByPicXY(pic1_x, pic1_y);
                    double z1_mm = imgShows[0].getDepthByPicXY(pic1_x, pic1_y);
                    double x1_mm, y1_mm;
                    imgShows[0].calculateWorldXY(pic1_x, pic1_y, z1_mm, out x1_mm, out y1_mm);

                    double pic2_x = matchPointResults[pi].match_X;
                    double pic2_y = matchPointResults[pi].match_Y;
                    double pic2_z = imgShows[1].getDepthPixelByPicXY(pic2_x, pic2_y);
                    double z2_mm = imgShows[1].getDepthByPicXY(pic2_x, pic2_y);
                    double x2_mm, y2_mm;
                    imgShows[1].calculateWorldXY(pic2_x, pic2_y, z2_mm, out x2_mm, out y2_mm);

                    if (z1_mm == 0 || z2_mm == 0)
                        continue;
                    pointCloud3Ds1.Add(new PointCloud3D(x1_mm, y1_mm, z1_mm, pic1_x, pic1_y, pic1_z));
                    pointCloud3Ds2.Add(new PointCloud3D(x2_mm, y2_mm, z2_mm, pic2_x, pic2_y, pic2_z));
                }
                SvdRT.RegisterPointCloud(pointCloud3Ds2, pointCloud3Ds1,
                    out MathNet.Numerics.LinearAlgebra.Matrix<double> mr, out Vector<double> vt);
                matchRs.Add(mr);
                matchTs.Add(vt);
                matchPointCloud3Ds = pointCloud3Ds2;
            }

            // 计算对应第一幅图的R和T
            for (int i = 0; i < imgShows.Count - 1; i++)
            {
                List<MathNet.Numerics.LinearAlgebra.Matrix<double>> R = matchRs.GetRange(0, i + 1);
                List<MathNet.Numerics.LinearAlgebra.Vector<double>> T = matchTs.GetRange(0, i + 1);
                ICP.getRotationsAndTranslation(R, T, 
                    out MathNet.Numerics.LinearAlgebra.Matrix<double> accumulateR,
                    out MathNet.Numerics.LinearAlgebra.Vector<double> accumulateT);
                angle.Add(Algorithm.MatrixToEuler(accumulateR));
            }
        }

        private void ShowMatchResultBtn_Click(object sender, EventArgs e)
        {
            if (imgShows.Count < 2)
            {
                return;
            }

            List<Bitmap> bitmaps = new List<Bitmap>();
            foreach (ImgShow imshow in imgShows)
            {
                bitmaps.Add(imshow.bitmap);
            }

            List<List<MatchPointResult>> matchPointResultss = new List<List<MatchPointResult>>();
            List<PointCloud3D> matchPointCloud3Ds = imgShows[0].waitMatchPoints;
            for (int i = 0; i < imgShows.Count - 1; i++)
            {
                Image<Gray, Byte> sourecImage = BitmapExtensions.ToGrayImage(imgShows[i].getOriginBitmap());
                Image<Gray, Byte> targetImage = BitmapExtensions.ToGrayImage(imgShows[i + 1].getOriginBitmap());
                List<MatchPointResult> matchPointResults;
                MatchingAlgorithm.PointCloudMatch(matchPointCloud3Ds, sourecImage, targetImage, out matchPointResults, subsetSize, dicSearchRange, limitR);
                List<PointCloud3D> pointCloud3Ds1 = new List<PointCloud3D>();
                List<PointCloud3D> pointCloud3Ds2 = new List<PointCloud3D>();
                for (int pi = 0; pi < matchPointResults.Count; pi++)
                {
                    double pic1_x = matchPointResults[pi].X;
                    double pic1_y = matchPointResults[pi].Y;
                    double pic1_z = imgShows[i].getDepthPixelByPicXY(pic1_x, pic1_y);
                    double z1_mm = imgShows[i].getDepthByPicXY(pic1_x, pic1_y);
                    double x1_mm, y1_mm;
                    imgShows[i].calculateWorldXY(pic1_x, pic1_y, z1_mm, out x1_mm, out y1_mm);
                    double pic2_x = matchPointResults[pi].match_X;
                    double pic2_y = matchPointResults[pi].match_Y;
                    double pic2_z = imgShows[i + 1].getDepthPixelByPicXY(pic1_x, pic1_y);
                    double z2_mm = imgShows[i + 1].getDepthByPicXY(pic2_x, pic2_y);
                    double x2_mm, y2_mm;
                    imgShows[i + 1].calculateWorldXY(pic2_x, pic2_y, z2_mm, out x2_mm, out y2_mm);
                    if (z1_mm == 0 || z2_mm == 0)
                        continue;
                    pointCloud3Ds1.Add(new PointCloud3D(x1_mm, y1_mm, z1_mm, pic1_x, pic1_y, pic1_z));
                    pointCloud3Ds2.Add(new PointCloud3D(x2_mm, y2_mm, z2_mm, pic2_x, pic2_y, pic2_z));
                }
                matchPointResultss.Add(matchPointResults);
                matchPointCloud3Ds = pointCloud3Ds2;
            }
            MatchPointShow matchPointShow = new MatchPointShow(bitmaps.ToArray(), matchPointResultss);
            matchPointShow.Show();
        }

        private void Show3DBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < imgShows.Count; i++)
            {
                List<PointCloud3D> pointCloud3Ds = imgShows[i].filterPointCloud3d;
                List<Color> colors = new List<Color>();
                foreach (PointCloud3D item in pointCloud3Ds)
                {
                    colors.Add(imgShows[i].GetColor((int)item.Pic_X, (int)item.Pic_Y));
                }

                _3DShow dShow = new _3DShow(pointCloud3Ds, colors);
                dShow.Show();
            }
        }

        private void openTkTest_Click(object sender, EventArgs e)
        {
            using (Learn game = new Learn(800, 600, "LearnOpenTK"))
            {
                //Run takes a double, which is how many frames per second it should strive to reach.
                //You can leave that out and it'll just update as fast as the hardware will allow it.
                game.Run(60.0);
            }

        }

        // 计算RT 计算重投影误差 进行融合
        private void plyFileOutputBtn_Click(object sender, EventArgs e)
        {
            List<PointCloud3D> matchPoint3Ds = imgShows[0].waitMatchPoints;
            List<List<PointCloud3D>> result3DLists = new List<List<PointCloud3D>>();
            List<PointCloud3D> result3Ds = new List<PointCloud3D>();
            result3DLists.Add(imgShows[0].filterPointCloud3d);
            result3Ds.AddRange(imgShows[0].filterPointCloud3d);

            //1.计算RT
            List<List<MatchPointResult>> matchPointResultss = new List<List<MatchPointResult>>();
            List<MathNet.Numerics.LinearAlgebra.Matrix<double>> matchRs = new List<MathNet.Numerics.LinearAlgebra.Matrix<double>>();
            List<MathNet.Numerics.LinearAlgebra.Vector<double>> matchTs = new List<MathNet.Numerics.LinearAlgebra.Vector<double>>();
            for (int i = 0; i < imgShows.Count - 1; i++)
            {
                Image<Gray, Byte> sourecImage = BitmapExtensions.ToGrayImage(imgShows[i].getOriginBitmap());
                Image<Gray, Byte> targetImage = BitmapExtensions.ToGrayImage(imgShows[i + 1].getOriginBitmap());
                List<MatchPointResult> matchPointResults;
                MatchingAlgorithm.PointCloudMatch(matchPoint3Ds, sourecImage, targetImage, out matchPointResults, subsetSize, dicSearchRange, limitR);
                matchPointResultss.Add(matchPointResults);

                List<PointCloud3D> pointCloud3Ds1 = new List<PointCloud3D>();
                List<PointCloud3D> pointCloud3Ds2 = new List<PointCloud3D>();
                for (int pi = 0; pi < matchPointResults.Count; pi++)
                {
                    double pic1_x = matchPointResults[pi].X;
                    double pic1_y = matchPointResults[pi].Y;
                    double pic1_z = imgShows[i].getDepthPixelByPicXY(pic1_x, pic1_y);
                    double z1_mm = imgShows[i].getDepthByPicXY(pic1_x, pic1_y);
                    double x1_mm, y1_mm;
                    imgShows[i].calculateWorldXY(pic1_x, pic1_y, z1_mm, out x1_mm, out y1_mm);

                    double pic2_x = matchPointResults[pi].match_X;
                    double pic2_y = matchPointResults[pi].match_Y;
                    double pic2_z = imgShows[i + 1].getDepthPixelByPicXY(pic1_x, pic1_y);
                    double z2_mm = imgShows[i + 1].getDepthByPicXY(pic2_x, pic2_y);
                    double x2_mm, y2_mm;
                    imgShows[i + 1].calculateWorldXY(pic2_x, pic2_y, z2_mm, out x2_mm, out y2_mm);

                    if (z1_mm == 0 || z2_mm == 0)
                        continue;
                    pointCloud3Ds1.Add(new PointCloud3D(x1_mm, y1_mm, z1_mm, pic1_x, pic1_y, pic1_z));
                    pointCloud3Ds2.Add(new PointCloud3D(x2_mm, y2_mm, z2_mm, pic2_x, pic2_y, pic2_z));
                }
                MathNet.Numerics.LinearAlgebra.Matrix<double> r;
                MathNet.Numerics.LinearAlgebra.Vector<double> t;
                SvdRT.RegisterPointCloud(pointCloud3Ds2, pointCloud3Ds1, out r, out t);
                //ICP.iteration(imgShows[i].filterPointCloud3d, imgShows[i+1].filterPointCloud3d, r, t, out MathNet.Numerics.LinearAlgebra.Matrix<double> mr, out MathNet.Numerics.LinearAlgebra.Vector<double> vt);
                matchRs.Add(r);
                matchTs.Add(t);
                // 把前一张的变形图当后一张参考图
                matchPoint3Ds = pointCloud3Ds2;
            }

            //2.重投影到第一幅图片
            for (int i = 0; i < matchRs.Count; i++) {
                List<MathNet.Numerics.LinearAlgebra.Matrix<double>> R = matchRs.GetRange(0, i + 1);
                List<MathNet.Numerics.LinearAlgebra.Vector<double>> T = matchTs.GetRange(0, i + 1);
                List<PointCloud3D> pointCloud3Dsto1 = ICP.transformListPointClouds(imgShows[i + 1].filterPointCloud3d, R, T);

                result3DLists.Add(pointCloud3Dsto1);
                result3Ds.AddRange(pointCloud3Dsto1);
            }

            /*// 通过可变圆模板重新匹配
            Bitmap Img_0 = imgShows[0].getOriginBitmap();
            List<List<MatchPointResult>> vms = new List<List<MatchPointResult>>();
            for (int i = 0; i < imgShows.Count - 1; i++)
            {
                Bitmap targetImage = imgShows[i + 1].getOriginBitmap();
                List<MathNet.Numerics.LinearAlgebra.Matrix<double>> R = matchRs.GetRange(0, i + 1);
                List<MatchPointResult> vm = MatchingAlgorithm.VariableCircleTemplateMatching(Img_0, targetImage, R, imgShows[0].filterPointCloud3d,
                    10, dicSearchRange, limitR, matchPointResultss[0]);
                vms.Add(vm);
            }*/

            //输出点云
            KdTree kdTree = new KdTree(result3Ds);
            PointFetures.getNormals(kdTree, result3Ds, 10);
            PLY.writePlyFile_xyzn("kinectFusionPly.ply", result3Ds);
        }

        private void VariableCircleMatchBegin_Click(object sender, EventArgs e)
        {
            double angle_x = double.Parse(variableCircle_angle_x.Text);
            double angle_y = double.Parse(variableCircle_angle_y.Text);
            double angle_z = double.Parse(variableCircle_angle_z.Text);
            int R = int.Parse(variableCircle_sample_r.Text);
            int searchSize = int.Parse(variableCircle_search_size.Text);
            int ref_index = int.Parse(variableCircle_ref_index.Text);
            double limit_R = double.Parse(variableCircle_limit_r.Text);
            // 判断是否会超过边界
            ref_index = ref_index < 0 ? imgShows.Count - 1 : 
                ref_index >= imgShows.Count - 1 ? imgShows.Count - 1 : ref_index;

            // 通过可变圆模板重新匹配
            Bitmap Img_0 = imgShows[0].getOriginBitmap();
            Bitmap targetImage = imgShows[ref_index].getOriginBitmap();
            List<MatchPointResult> vm = MatchingAlgorithm.VariableCircleTemplateMatching(Img_0, targetImage, angle[ref_index - 1], imgShows[0].waitMatchPoints,
                    R, searchSize, limit_R);

            List<Bitmap> bitmaps = new List<Bitmap>() { imgShows[0].bitmap, imgShows[ref_index].bitmap};
            MatchPointShow matchPointShow = new MatchPointShow(bitmaps.ToArray(), new List<List<MatchPointResult>>() {vm});
            matchPointShow.Show();

        }

        private void 服务端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerService server = new ServerService();
            server.Show();
        }

        private void 客户端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientService client = new ClientService();
            client.Show();
            
        }
    }
}
