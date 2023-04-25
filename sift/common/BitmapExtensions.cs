using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace sift.common
{
    public static class BitmapExtensions
    {
        public static Image<Bgr, byte> ToBgrImage(Bitmap bitmap)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>(bitmap.Width, bitmap.Height);

            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            try
            {
                int stride = bmpData.Stride;
                byte[] data = new byte[Math.Abs(stride) * bitmap.Height];
                Marshal.Copy(bmpData.Scan0, data, 0, data.Length);

                int channels = bitmap.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;

                int k = 0;
                for (int i = 0; i < bitmap.Height; i++)
                {
                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        image[i, j] = new Bgr(data[k], data[k + 1], data[k + 2]);
                        k += channels;
                    }
                    k += stride - bitmap.Width * channels;
                }
            }
            finally
            {
                bitmap.UnlockBits(bmpData);
            }

            return image;
        }

        public static Image<Gray, byte> ToGrayImage(this Bitmap bitmap)
        {
            Image<Gray, byte> image = new Image<Gray, byte>(bitmap.Width, bitmap.Height);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            byte[] pixelData = new byte[bmpData.Stride * bitmap.Height];
            Marshal.Copy(bmpData.Scan0, pixelData, 0, pixelData.Length);
            bitmap.UnlockBits(bmpData);
            int index = 0;
            for (int row = 0; row < image.Height; row++)
            {
                for (int col = 0; col < image.Width; col++)
                {
                    byte grayValue = pixelData[index];
                    image.Data[row, col, 0] = grayValue;
                    index += bytesPerPixel;
                }
            }
            return image;
        }

        public static Bitmap BgrToBitmap(Image<Bgr, byte> image)
        {
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);

            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
            try
            {
                int stride = bmpData.Stride;
                byte[] data = new byte[Math.Abs(stride) * bitmap.Height];

                int channels = 3; // BGR

                int k = 0;
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        data[k] = (byte)image[i, j].Blue;
                        data[k + 1] = (byte)image[i, j].Green;
                        data[k + 2] = (byte)image[i, j].Red;
                        k += channels;
                    }
                    k += stride - image.Width * channels;
                }

                Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            }
            finally
            {
                bitmap.UnlockBits(bmpData);
            }

            return bitmap;
        }

        public static Bitmap GrayToBitmap(this Image<Gray, byte> image)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format8bppIndexed);
            ColorPalette palette = bitmap.Palette;
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bitmap.Palette = palette;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
            try
            {
                byte[] data = new byte[image.Width * image.Height];
                int k = 0;
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        data[k] = (byte) image[i, j].Intensity;
                        k++;
                    }
                }
                Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            }
            finally
            {
                bitmap.UnlockBits(bmpData);
            }

            return bitmap;
        }

        public static unsafe byte[] ConvertTo8Byte(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            byte[] result = new byte[width * height];
            int n = 0;
            if (img.PixelFormat.ToString() == "Format8bppIndexed")
            {
                BitmapData data = img.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite,
                                         PixelFormat.Format8bppIndexed);
                byte* ptr = (byte*)(data.Scan0);
                for (int i = 0; i != data.Height; i++)
                {
                    for (int j = 0; j != data.Width; j++)
                    {
                        result[i * data.Width + j] = *ptr;
                        ptr++;
                    }
                }
                img.UnlockBits(data);
            }
            else
            {
                BitmapData data = img.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite,
                                         PixelFormat.Format24bppRgb);
                var bp = (byte*)data.Scan0.ToPointer();
                for (int i = 0; i != data.Height; i++)
                {
                    for (int j = 0; j != data.Width; j++)
                    {
                        float value = 0.11F * bp[i * data.Stride + j * 3] + 0.59F * bp[i * data.Stride + j * 3 + 1] +
                                      0.3F * bp[i * data.Stride + j * 3 + 2];
                        result[n] = (byte)value;
                        n++;
                    }
                }
                img.UnlockBits(data);
            }
            return result;
        }

        // 径向畸变矫正
        public static byte[] RadialDistortionCorrection8byte(ref byte[] img, int w, int h, double k1, double k2, double k3, double cx, double cy, double fx, double fy)
        {
            byte[] img_temp = new byte[w * h];
            //Array.Copy(img, img_temp, img.Length);
            //遍历所有像素点进行校正
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    // 先将像素坐标转为相机坐标
                    double u = (x - cx) / fx;
                    double v = (y - cy) / fy;

                    //计算出每个点（相机坐标系）与中心点的距离
                    double r = u * u + v * v;

                    //计算出径向畸变校正后的新像素点坐标
                    double newX = u + u * (k1 * r + k2 * r * r + k3 * r * r * r);
                    double newY = v + v * (k1 * r + k2 * r * r + k3 * r * r * r);

                    //转回像素坐标系
                    double picX = newX * fx + cx;
                    double picY = newY * fy + cy;

                    //防止越界
                    if (picX < 0 || picX >= w || picY < 0 || picY >= h)
                    {
                        continue;
                    }

                    //计算校正后的像素点颜色值
                    //byte value = (byte)Algorithm.bilinearInterpolation(img, w, h, picX, picY);
                    byte value = (byte)img[(int)picX + w * (int)picY];

                    //将校正后的像素点颜色值赋给新的图像
                    img_temp[x + y * w] = value;
                }
            }

            return img_temp;
        }

        public static byte[] RadialDistortionCorrection32byte(ref byte[] img, int w, int h, double k1, double k2, double k3, double cx, double cy, double fx, double fy)
        {
            byte[] img_temp = new byte[w * h * 4];
            //Array.Copy(img, img_temp, img.Length);
            //遍历所有像素点进行校正
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    // 先将像素坐标转为相机坐标
                    double u = (x - cx) / fx;
                    double v = (y - cy) / fy;

                    //计算出每个点（相机坐标系）与中心点的距离
                    double r = u * u + v * v;

                    //计算出径向畸变校正后的新像素点坐标
                    double newX = u + u * (k1 * r + k2 * r * r + k3 * r * r * r);
                    double newY = v + v * (k1 * r + k2 * r * r + k3 * r * r * r);

                    //转回像素坐标系
                    double picX = newX * fx + cx;
                    double picY = newY * fy + cy;

                    //防止越界
                    if (picX < 0 || picX >= w || picY < 0 || picY >= h)
                    {
                        continue;
                    }

                    //计算校正后的像素点颜色值
                    byte value1 = img[((int)picX + (int)picY * w) * 4];
                    byte value2 = img[((int)picX + (int)picY * w) * 4 + 1];
                    byte value3 = img[((int)picX + (int)picY * w) * 4 + 2];
                    byte value4 = img[((int)picX + (int)picY * w) * 4 + 3];

                    //将校正后的像素点颜色值赋给新的图像
                    img_temp[(x + y * w) * 4] = value1;
                    img_temp[(x + y * w) * 4 + 1] = value2;
                    img_temp[(x + y * w) * 4 + 2] = value3;
                    img_temp[(x + y * w) * 4 + 3] = value4;
                }
            }

            return img_temp;
        }

        // 径向畸变矫正
        public static ushort[] RadialDistortionCorrection(ref ushort[] img, int w, int h, double k1, double k2, double k3, double cx, double cy, double fx, double fy)
        {
            ushort[] img_temp = new ushort[w * h];
            //Array.Copy(img, img_temp, img.Length);
            //遍历所有像素点进行校正
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    // 先将像素坐标转为相机坐标
                    double u = (x - cx) / fx;
                    double v = (y - cy) / fy;

                    //计算出每个点（相机坐标系）与中心点的距离
                    double r = u * u + v * v;

                    //计算出径向畸变校正后的新像素点坐标
                    double newX = u + u * (k1 * r + k2 * r * r + k3 * r * r * r);
                    double newY = v + v * (k1 * r + k2 * r * r + k3 * r * r * r);

                    //转回像素坐标系
                    double picX = newX * fx + cx;
                    double picY = newY * fy + cy;

                    //防止越界
                    if (picX < 0 || picX >= w || picY < 0 || picY >= h)
                    {
                        continue;
                    }

                    //计算校正后的像素点颜色值
                    //ushort value = (ushort)Algorithm.bilinearInterpolation(img, w, h, picX, picY);
                    ushort value = (ushort)img[(int)picX + w * (int)picY];

                    //将校正后的像素点颜色值赋给新的图像
                    img_temp[x + y * w] = value;
                }
            }

            return img_temp;
        }
    }
}
