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
    }
}
