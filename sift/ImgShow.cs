using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Microsoft.Kinect;
using sift.common;
using sift.PFH;
using sift.PointCloud;
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

namespace sift
{
    public partial class ImgShow : Form
    {
        // 创建这个ImageShow的Form
        private MainForm mainForm;

        private Bitmap bitmap = null;
        private ushort[] depths = null;
        private CameraIntrinsics cameraIntrinsics;
        // 注意4个byte代表一个像素点rgba
        private byte[] rgbaData = null;

        // 降采样后的点
        public List<PointCloud3D> filterPointCloud3d;

        private int sampleRange = 20;
        private int fpfhRange = 50;

        public ImgShow(Bitmap bitmap, ushort[] depths, CameraIntrinsics cameraIntrinsics, byte[] rgbaData, MainForm mainform)
        {
            InitializeComponent();
            this.bitmap = bitmap;
            this.depths = depths; 
            this.cameraIntrinsics = cameraIntrinsics;
            this.rgbaData = rgbaData;
            this.mainForm = mainform;
        }

        private void ImgShow_Load(object sender, EventArgs e)
        {
            Image<Gray, byte> image = BitmapExtensions.ToGrayImage(bitmap);
            picBox.Image = BitmapExtensions.GrayToBitmap(image);
            //picBox.Image = this.bitmap;

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
                    int x_mm, y_mm;
                    calculateWorldXY(cameraIntrinsics, i, j, z_mm, out x_mm, out y_mm);
                    pointCloud3Ds.Add(new PointCloud3D(x_mm, y_mm, z_mm, i, j));
                }
            }

            filterPointCloud3d = pointCloud3Ds;
            // 2.点云降采样
            /*            KdTree kdTree = new KdTree(pointCloud3Ds);
                        filterPointCloud3d = PointCloud3D.downSampling(picBox.Image.Width, picBox.Image.Height, sampleRange, pointCloud3Ds);
                        PointFetures.FPFH(filterPointCloud3d, fpfhRange);*/
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            int pic_x = (int)((double)x / (double)picBox.Width * (double)bitmap.Width);
            int pic_y = (int)((double)y / (double)picBox.Height * (double)bitmap.Height);

            int z_mm = depths[(pic_x + pic_y * bitmap.Width)];
            int x_mm, y_mm;

            calculateWorldXY(cameraIntrinsics, pic_x, pic_y, z_mm, out x_mm, out y_mm);

            infoLabel.Text = String.Format("x_mm:{0} y_mm:{1} z_mm:{2}; x:{3} y:{4}",
                x_mm, y_mm, z_mm, x, y);
        }

        private void downSamplingBtn_Click(object sender, EventArgs e)
        {
            // 1.计算点云
            List<PointCloud3D> pointCloud3Ds = new List<PointCloud3D>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++) {
                    int z_mm = depths[i + j * bitmap.Width];
                    if (z_mm <= 0) {
                        continue;
                    }
                    int x_mm, y_mm;
                    calculateWorldXY(cameraIntrinsics, i, j, z_mm, out x_mm, out y_mm);
                    pointCloud3Ds.Add(new PointCloud3D(x_mm, y_mm, z_mm, i, j));
                }
            }

            // 2.点云降采样
            List<PointCloud3D> filterPointClouds = PointCloud3D.downSampling(picBox.Image.Width, picBox.Image.Height, sampleRange, pointCloud3Ds);

            // 3.显示降采样后的点云
            showCloudPoint3D(filterPointClouds);
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

        private void calculateWorldXY(CameraIntrinsics cameraIntrinsics,
            int xp, int yp, double zw,
            out int xw, out int yw) {

            float ux = cameraIntrinsics.PrincipalPointX;
            float uy = cameraIntrinsics.PrincipalPointY;
            float fx = cameraIntrinsics.FocalLengthX;
            float fy = cameraIntrinsics.FocalLengthY;

            xw = (int)(zw * (xp - ux) / fx);
            yw = (int)(zw * (yp - uy) / fy);
        }

        private void rollBackBtn_Click(object sender, EventArgs e)
        {
            this.picBox.Image = bitmap;
        }

        private void calFpfh_Click(object sender, EventArgs e)
        {
            PointFetures.FPFH(filterPointCloud3d, fpfhRange);
            int a = 0;
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

        private void showCloudPoint3D(List<PointCloud3D> filterPointClouds) {
            // 显示离散点云
            Bitmap colorDepthBitmap = new Bitmap(bitmap.Width, bitmap.Height, 0, PixelFormat.Format32bppArgb, IntPtr.Zero);
            Rectangle ret = new Rectangle(0, 0, colorDepthBitmap.Width, colorDepthBitmap.Height);
            BitmapData bitmapData = colorDepthBitmap.LockBits(ret, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            IntPtr ptrSrc = bitmapData.Scan0;
            byte[] dataImg = new byte[colorDepthBitmap.Width * colorDepthBitmap.Height * 4];
            for (int i = 0; i < filterPointClouds.Count; i++)
            {
                int x = filterPointClouds[i].Pic_X;
                int y = filterPointClouds[i].Pic_Y;
                dataImg[(x + y * bitmap.Width) * 4] = rgbaData[(x + y * bitmap.Width) * 4];
                dataImg[(x + y * bitmap.Width) * 4 + 1] = rgbaData[(x + y * bitmap.Width) * 4 + 1];
                dataImg[(x + y * bitmap.Width) * 4 + 2] = rgbaData[(x + y * bitmap.Width) * 4 + 2];
                dataImg[(x + y * bitmap.Width) * 4 + 3] = rgbaData[(x + y * bitmap.Width) * 4 + 3];
            }
            Marshal.Copy(dataImg, 0, ptrSrc, dataImg.Length);
            colorDepthBitmap.UnlockBits(bitmapData);
            picBox.Image = colorDepthBitmap;
            filterPointCloud3d = filterPointClouds;
        }
    }
}