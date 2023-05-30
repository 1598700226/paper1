using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.Kinect;
using sift.common;
using sift.PFH;
using sift.PointCloudHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace sift
{
    public partial class ImgShow : Form
    {
        // 创建这个ImageShow的Form
        private MainForm mainForm;

        public Bitmap bitmap = null;
        public ushort[] depths = null;
        public byte[] depthsPixel;
        private CameraIntrinsics cameraIntrinsics;
        // 注意4个byte代表一个像素点rgba
        public byte[] bgraData = null;
        // 原始 未经过区域选点的
        public byte[] oriBgraData = null;

        // 降采样后的点
        public List<PointCloud3D> filterPointCloud3d;

        private int sampleRange = 5;
        private int fpfhRange = 50;

        // 选择待匹配点
        private bool isSeletingWaitMatchPoint = false;
        public List<PointCloud3D> waitMatchPoints = new List<PointCloud3D>();

        // 选择地面的点
        private bool isSeletingPlanePoint = false;
        public List<PointCloud3D> planePoint = new List<PointCloud3D>(3);

        public ImgShow(Bitmap bitmap, ushort[] depths, byte[] depthsPixel, 
            CameraIntrinsics cameraIntrinsics, byte[] bgraData, byte[] oriBgraData, MainForm mainform)
        {
            InitializeComponent();
            this.bitmap = bitmap;
            this.depths = depths; 
            this.depthsPixel = depthsPixel;
            this.cameraIntrinsics = cameraIntrinsics;
            this.bgraData = bgraData;
            this.oriBgraData = oriBgraData;
            this.mainForm = mainform;
        }

        private void ImgShow_Load(object sender, EventArgs e)
        {
            picBox.Image = this.bitmap;
            // 1.计算点云
            List<PointCloud3D> pointCloud3Ds = new List<PointCloud3D>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int z_mm = depths[i + j * bitmap.Width];
                    if (z_mm <= 0)
                    {
                        continue;
                    }
                    double x_mm, y_mm;
                    calculateWorldXY(i, j, z_mm, out x_mm, out y_mm);
                    double z = getDepthPixelByPicXY(i, j);
                    pointCloud3Ds.Add(new PointCloud3D(x_mm, y_mm, z_mm, i, j, z));
                }
            }
            filterPointCloud3d = pointCloud3Ds;

            waitMatchPoints.Add(new PointCloud3D(200, 200, 1, 200, 200, 1));
            waitMatchPoints.Add(new PointCloud3D(300, 200, 1, 300, 200, 1));
            waitMatchPoints.Add(new PointCloud3D(250, 250, 1, 250, 250, 1));
            waitMatchPoints.Add(new PointCloud3D(200, 300, 1, 200, 300, 1));
            waitMatchPoints.Add(new PointCloud3D(300, 300, 1, 300, 300, 1));
        }

        public double getDepthByPicXY(double x, double y) {
            int ix = (int)Math.Round(x, MidpointRounding.AwayFromZero);
            int iy = (int)Math.Round(y, MidpointRounding.AwayFromZero);

            return depths[(int)ix + (int)iy * bitmap.Width];
            //return Algorithm.bilinearInterpolation(depths, bitmap.Width, bitmap.Height, x, y);
        }

        public double getDepthPixelByPicXY(double x, double y)
        {
            int ix = (int)Math.Round(x, MidpointRounding.AwayFromZero);
            int iy = (int)Math.Round(y, MidpointRounding.AwayFromZero);

            return depthsPixel[(int)ix + (int)iy * bitmap.Width];
            //return Algorithm.bilinearInterpolation(depthsPixel, bitmap.Width, bitmap.Height, x, y);
        }

        public PointCloud3D GetPointCloud3DByPicXY(double x, double y) { 
            double zw = getDepthByPicXY(x, y);
            calculateWorldXY(x, y, zw, out double xw, out double yw);
            return new PointCloud3D(xw, yw, zw);
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            int pic_x = (int)((double)x / (double)picBox.Width * (double)bitmap.Width);
            int pic_y = (int)((double)y / (double)picBox.Height * (double)bitmap.Height);
            int pic_z = (int)getDepthPixelByPicXY(pic_x, pic_y);
            int z_mm = depths[(pic_x + pic_y * bitmap.Width)];
            double x_mm, y_mm;
            calculateWorldXY(pic_x, pic_y, z_mm, out x_mm, out y_mm);
            infoLabel.Text = String.Format("x_mm:{0:N} y_mm:{1:N} z_mm:{2:N}; x:{3} y:{4} z:{5} waitMatchPointNum:{6}",
                x_mm, y_mm, z_mm, pic_x, pic_y, pic_z, waitMatchPoints.Count);
        }

        private void downSamplingBtn_Click(object sender, EventArgs e)
        {
/*            // 1.计算点云
            List<PointCloud3D> pointCloud3Ds = new List<PointCloud3D>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++) {
                    int z_mm = depths[i + j * bitmap.Width];
                    if (z_mm <= 0) {
                        continue;
                    }
                    double x_mm, y_mm;
                    calculateWorldXY(i, j, z_mm, out x_mm, out y_mm);
                    double z = getDepthPixelByPicXY(i, j);
                    pointCloud3Ds.Add(new PointCloud3D(x_mm, y_mm, z_mm, i, j, z));
                }
            }*/

            // 2.点云降采样
            filterPointCloud3d = PointCloud3D.downSamplingTisu(picBox.Image.Width, picBox.Image.Height, sampleRange, filterPointCloud3d);

            // 3.显示降采样后的点云
            showCloudPoint3D(filterPointCloud3d);
        }

        private void savePicBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.FileName = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + "图片"; //设置默认文件名
            dialog.Filter = "图片(*.bmp)|*.bmp";
            dialog.DefaultExt = "bmp";                                 //设置默认格式（可以不设）
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picBox.Image.Save(dialog.FileName, ImageFormat.Bmp);
            }
        }

        public void calculateWorldXY(double xp, double yp, double zw,
            out double xw, out double yw) {

            float ux = this.cameraIntrinsics.PrincipalPointX;
            float uy = this.cameraIntrinsics.PrincipalPointY;
            float fx = this.cameraIntrinsics.FocalLengthX;
            float fy = this.cameraIntrinsics.FocalLengthY;

            xw = (zw * (xp - ux) / fx);
            yw = (zw * (yp - uy) / fy);
        }

        public Color GetColor(int pic_x, int pic_y) {
            return Color.FromArgb(bgraData[(pic_x + pic_y * bitmap.Width) * 4 + 3], 
                bgraData[(pic_x + pic_y * bitmap.Width) * 4 + 2], 
                bgraData[(pic_x + pic_y * bitmap.Width) * 4 + 1], 
                bgraData[(pic_x + pic_y * bitmap.Width) * 4]);
        }

        public void setColorToListPoint(List<PointCloud3D> filterPointClouds) {
            foreach (PointCloud3D point in filterPointClouds) {
                point.color = GetColor((int)point.Pic_X, (int)point.Pic_Y);
            }
        }

        private void rollBackBtn_Click(object sender, EventArgs e)
        {
            this.picBox.Image = getOriginBitmap();
        }

        private void rollBackBtn_Click_1(object sender, EventArgs e)
        {
            // 1.计算点云
            List<PointCloud3D> pointCloud3Ds = new List<PointCloud3D>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int z_mm = depths[i + j * bitmap.Width];
                    if (z_mm <= 0)
                    {
                        continue;
                    }
                    double x_mm, y_mm;
                    calculateWorldXY(i, j, z_mm, out x_mm, out y_mm);
                    double z = getDepthPixelByPicXY(i, j);
                    pointCloud3Ds.Add(new PointCloud3D(x_mm, y_mm, z_mm, i, j, z));
                }
            }
            filterPointCloud3d = pointCloud3Ds;
            showCloudPoint3D(filterPointCloud3d);
        }

        private void calFpfh_Click(object sender, EventArgs e)
        {
            PointFetures.FPFH(filterPointCloud3d, fpfhRange);
        }

        private void ImgShow_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.imgShows.Remove(this);
        }

        private void btnOutliers_Click(object sender, EventArgs e)
        {
            filterPointCloud3d = PointCloud3D.RemoveOutliersByStatistic(filterPointCloud3d, 3);
            showCloudPoint3D(filterPointCloud3d);
        }

        private void DirectFilteringBtn_Click(object sender, EventArgs e)
        {
            filterPointCloud3d = PointCloud3D.DirectFiltingByWorldZmm(filterPointCloud3d, 1500, 0);
            showCloudPoint3D(filterPointCloud3d);
        }

        private void showCloudPoint3D(List<PointCloud3D> filterPointClouds) {
            // 显示离散点云
            Bitmap colorDepthBitmap = new Bitmap(bitmap.Width, bitmap.Height, 0, PixelFormat.Format32bppArgb, IntPtr.Zero);
            Rectangle ret = new Rectangle(0, 0, colorDepthBitmap.Width, colorDepthBitmap.Height);
            BitmapData bitmapData = colorDepthBitmap.LockBits(ret, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            IntPtr ptrSrc = bitmapData.Scan0;
            byte[] dataImg = new byte[colorDepthBitmap.Width * colorDepthBitmap.Height * 4];
            for (int i = 0; i < filterPointClouds.Count; i++)
            {
                int x = (int)filterPointClouds[i].Pic_X;
                int y = (int)filterPointClouds[i].Pic_Y;
                dataImg[(x + y * bitmap.Width) * 4] = bgraData[(x + y * bitmap.Width) * 4];
                dataImg[(x + y * bitmap.Width) * 4 + 1] = bgraData[(x + y * bitmap.Width) * 4 + 1];
                dataImg[(x + y * bitmap.Width) * 4 + 2] = bgraData[(x + y * bitmap.Width) * 4 + 2];
                dataImg[(x + y * bitmap.Width) * 4 + 3] = bgraData[(x + y * bitmap.Width) * 4 + 3];
            }
            Marshal.Copy(dataImg, 0, ptrSrc, dataImg.Length);
            colorDepthBitmap.UnlockBits(bitmapData);
            picBox.Image = colorDepthBitmap;
            bitmap = colorDepthBitmap;
            //filterPointCloud3d = filterPointClouds;
        }

        private void btnRgbD_Click(object sender, EventArgs e)
        {
            Bitmap bitmapGray = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format8bppIndexed);
            ColorPalette palette = bitmapGray.Palette;
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bitmapGray.Palette = palette;
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmapGray.LockBits(rect, ImageLockMode.WriteOnly, bitmapGray.PixelFormat);
            try
            {
                byte[] data = new byte[bitmap.Width * bitmap.Height];
                for (int i = 0; i < bitmap.Height; i++)
                {
                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        data[i * bitmap.Width + j] = (byte)(bgraData[(i * bitmap.Width + j) * 4] * 0.25F + 
                            bgraData[(i * bitmap.Width + j) * 4 + 1] * 0.25F + 
                            bgraData[(i * bitmap.Width + j) * 4 + 2] * 0.25F +
                            depths[(i * bitmap.Width) + j] * 256 / 8000 * 0.25F);
                    }
                }
                Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            }
            finally
            {
                bitmapGray.UnlockBits(bmpData);
            }
            picBox.Image = bitmapGray;
        }

        public Bitmap getOriginBitmap() {
            Bitmap originBitmap = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
            Rectangle ret = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = originBitmap.LockBits(ret, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            IntPtr ptrSrc = bitmapData.Scan0;
            Marshal.Copy(oriBgraData, 0, ptrSrc, oriBgraData.Length);
            originBitmap.UnlockBits(bitmapData);
            return originBitmap;
        }

        private void btnPlyFile_Click(object sender, EventArgs e)
        {
            /*// 1. KD树生成, 并计算自身的法线 spfh值
            KdTree kdTree = new KdTree(filterPointCloud3d);
            PointFetures.getNormals(kdTree, filterPointCloud3d, 10);*/
            setColorToListPoint(filterPointCloud3d);
            PLY.writePlyFile_xyzrgb("kinectPly_rgb.ply", filterPointCloud3d);
        }

        private void 输出off文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setColorToListPoint(filterPointCloud3d);
            PLY.writeOFFFile_xyzrgb("kinectOff_rgb.off", filterPointCloud3d);
        }

        private void removeGroundBtn_Click(object sender, EventArgs e)
        {
            filterPointCloud3d = PointCloud3D.GroundFilting(filterPointCloud3d, int.Parse(GroundFilteringIteration.Text), double.Parse(GroundFilteringError.Text));
            showCloudPoint3D(filterPointCloud3d);
        }

        private void selectWaitMatchPoint_Click(object sender, EventArgs e)
        {
            isSeletingWaitMatchPoint = true;
            waitMatchPoints.Clear();
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (isSeletingWaitMatchPoint) {
                if (e.Button == MouseButtons.Right)
                {
                    isSeletingWaitMatchPoint = false;
                }
                if (e.Button == MouseButtons.Left)
                {
                    int x = e.X;
                    int y = e.Y;
                    int pic_x = (int)((double)x / (double)picBox.Width * (double)bitmap.Width);
                    int pic_y = (int)((double)y / (double)picBox.Height * (double)bitmap.Height);
                    int pic_z = (int)getDepthPixelByPicXY(pic_x, pic_y);
                    int z_mm = depths[(pic_x + pic_y * bitmap.Width)];
                    calculateWorldXY(pic_x, pic_y, z_mm, out double x_mm, out double y_mm);
                    waitMatchPoints.Add(new PointCloud3D(x_mm, y_mm, z_mm, pic_x, pic_y, pic_z));
                }
            }

            if (isSeletingPlanePoint) {
                if (e.Button == MouseButtons.Right)
                {
                    isSeletingPlanePoint = false;
                }
                if (e.Button == MouseButtons.Left)
                {
                    int x = e.X;
                    int y = e.Y;
                    int pic_x = (int)((double)x / (double)picBox.Width * (double)bitmap.Width);
                    int pic_y = (int)((double)y / (double)picBox.Height * (double)bitmap.Height);
                    planePoint.Add(GetPointCloud3DByPicXY(pic_x, pic_y));
                }
            }
        }

        private void DBSCAN_begin_btn_Click(object sender, EventArgs e)
        {
            double eps = double.Parse(DBSCAN_epsValue.Text);
            double minNum = int.Parse(DBSCAN_minNum.Text);
            List<List<PointCloud3D>> clusterPoints = ClusterPointCloud.DBSCAN(filterPointCloud3d, eps, minNum);

            List<List<PointCloud3D>> cluster = clusterPoints.OrderByDescending(p => p.Count).ToList();
            filterPointCloud3d = cluster[0];
            showCloudPoint3D(cluster[0]);
        }

        private void selectConvertPoints_Click(object sender, EventArgs e)
        {
            isSeletingPlanePoint = true;
            planePoint.Clear();

        }

        private void convertAndOutputPly_Click(object sender, EventArgs e)
        {
            if (planePoint.Count != 3) {
                return;
            }
/*            QRCode.getPosition((Bitmap)picBox.Image,
                out System.Drawing.PointF leftDown,
                out System.Drawing.PointF leftUp,
                out System.Drawing.PointF rightUp);

            List<PointCloud3D> pointCloud3Ds = new List<PointCloud3D>();
            pointCloud3Ds.Add(GetPointCloud3DByPicXY(leftDown.X, leftDown.Y));
            pointCloud3Ds.Add(GetPointCloud3DByPicXY(leftUp.X, leftUp.Y));
            pointCloud3Ds.Add(GetPointCloud3DByPicXY(rightUp.X, rightUp.Y));*/

            // 单位毫米
            List<PointCloud3D> planePointCloud3Ds = new List<PointCloud3D>();
            planePointCloud3Ds.Add(new PointCloud3D(0, 0, 0));
            planePointCloud3Ds.Add(new PointCloud3D(0, 50, 0));
            planePointCloud3Ds.Add(new PointCloud3D(50, 50, 0));

            SvdRT.RegisterPointCloud(planePoint, planePointCloud3Ds, out MathNet.Numerics.LinearAlgebra.Matrix<double> rotation, out Vector<double> translation);
            List<PointCloud3D> handlePC3Ds = ICP.transformListPointClouds(filterPointCloud3d, rotation, translation);
            setColorToListPoint(filterPointCloud3d);
            for (int i = 0; i < filterPointCloud3d.Count; i++)
            {
                PointCloud3D item = filterPointCloud3d[i];
                handlePC3Ds[i].color = item.color;
            }

            PLY.writePlyFile_xyzrgb("kinectPly_rgb_convert_plane.ply", handlePC3Ds);
        }
    }
}