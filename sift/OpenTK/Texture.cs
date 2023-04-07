using Emgu.CV;
using Emgu.CV.Structure;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace sift.OpenTK
{
    public class Texture
    {
        public readonly int Handle;

        public Texture(int handle)
        {
            Handle = handle;
        }

        public static Texture LoadFromImage(String path) {
            int handle = GL.GenTexture();

            Bitmap bitmap = new Bitmap(path);
            Rectangle ret = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(ret, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            IntPtr ptrSrc = bitmapData.Scan0;
            byte[] dataImg = new byte[bitmap.Width * bitmap.Height * 4];
            Marshal.Copy(ptrSrc, dataImg, 0, dataImg.Length);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                bitmap.Width, bitmap.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, dataImg);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(handle);
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}
