using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Emgu.CV.DepthAI;
using Emgu.CV.Reg;
using Microsoft.Kinect;
using sift.common;

namespace sift.kinect
{
    public class CameraOpen
    {
        public MainForm mainForm;

        /// <summary>
        /// Map depth range to byte range
        /// </summary>
        public const int MapDepthToByte = 8000 / 256;
        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        public KinectSensor kinectSensor = null;

        private ColorFrameReader colorFrameReader = null;
        private DepthFrameReader depthFrameReader = null;
        private FrameDescription depthFrameDescription = null;

        public byte[] depthPixels = null;
        public ushort[] udepth = null;
        public ColorSpacePoint[] colorSpacePoints = null;
        private Bitmap depthBitmap = null;
        public byte[] colorPixels = null;
        private Bitmap colorBitmap = null;

        // 视频流显示
        private PictureBox pictureBoxColor = null;
        private PictureBox pictureBoxDepth = null;
        // 深度相机的内部参数
        public CameraIntrinsics depthCameraIntrinsics;

        // 修改生成8位图的索引表，从伪彩修改为灰度  
        ColorPalette palette;

        public CameraOpen() {
            
            // 获取一个Format8bppIndexed格式图像的Palette对象  
            using (Bitmap bmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
            {
                palette = bmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = System.Drawing.Color.FromArgb(i, i, i);
            }
        }

        public void Init(PictureBox picBoxColor, PictureBox picBoxDepth, MainForm mainForm) {
            this.mainForm = mainForm;
            // get the kinectSensor object
            this.kinectSensor = KinectSensor.GetDefault();

            // open the reader for the color frames
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();
            // wire handler for frame arrival
            this.colorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;
            // create the colorFrameDescription from the ColorFrameSource using Bgra format
            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
            // create the bitmap to display
            this.colorPixels = new byte[colorFrameDescription.Width * colorFrameDescription.Height * 4];
            this.colorBitmap = new Bitmap(colorFrameDescription.Width, colorFrameDescription.Height, 0, System.Drawing.Imaging.PixelFormat.Format32bppArgb, IntPtr.Zero);

            // open the reader for the depth frames
            this.depthFrameReader = this.kinectSensor.DepthFrameSource.OpenReader();
            this.depthFrameReader.FrameArrived += this.Reader_DepthFrameArrived;
            // get FrameDescription from DepthFrameSource
            this.depthFrameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;
            // allocate space to put the pixels being received and converted
            this.depthPixels = new byte[this.depthFrameDescription.Width * this.depthFrameDescription.Height];
            this.udepth = new ushort[depthPixels.Length];
            this.colorSpacePoints = new ColorSpacePoint[depthPixels.Length];
            this.depthBitmap = new Bitmap(this.depthFrameDescription.Width, this.depthFrameDescription.Height, 0, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, IntPtr.Zero);
            // 不搞调色板，8位bmp会出现伪彩色
            depthBitmap.Palette = palette;

            // set IsAvailableChanged event notifier
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // open the sensor
            this.kinectSensor.Open();

            pictureBoxColor = picBoxColor;
            pictureBoxDepth = picBoxDepth;

        }

        unsafe private void Reader_DepthFrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            bool depthFrameProcessed = false;

            using (DepthFrame depthFrame = e.FrameReference.AcquireFrame())
            {
                depthCameraIntrinsics = kinectSensor.CoordinateMapper.GetDepthCameraIntrinsics();
                if (depthFrame != null)
                {
                    // the fastest way to process the body index data is to directly access 
                    // the underlying buffer
                    using (Microsoft.Kinect.KinectBuffer depthBuffer = depthFrame.LockImageBuffer())
                    {
                       
                        // verify data and write the color data to the display bitmap
                        if (((this.depthFrameDescription.Width * this.depthFrameDescription.Height) == (depthBuffer.Size / this.depthFrameDescription.BytesPerPixel)) &&
                            (this.depthFrameDescription.Width == this.depthBitmap.Width) && (this.depthFrameDescription.Height == this.depthBitmap.Height))
                        {
                            // Note: In order to see the full range of depth (including the less reliable far field depth)
                            // we are setting maxDepth to the extreme potential depth threshold
                            ushort maxDepth = ushort.MaxValue;

                            // If you wish to filter by reliable depth distance, uncomment the following line:
                            maxDepth = depthFrame.DepthMaxReliableDistance;

                            //this.ProcessDepthFrameData(depthBuffer.UnderlyingBuffer, depthBuffer.Size, depthFrame.DepthMinReliableDistance, maxDepth);
                            // depth frame data is a 16 bit value
                            ushort* frameData = (ushort*)depthBuffer.UnderlyingBuffer;
                            
                            // convert depth to a visual representation
                            for (int i = 0; i < (int)(depthBuffer.Size / this.depthFrameDescription.BytesPerPixel); ++i)
                            {
                                // Get the depth for this pixel
                                ushort depth = frameData[i];
                                udepth[i] = depth;
                                
                                // To convert to a byte, we're mapping the depth value to the byte range.
                                // Values outside the reliable depth range are mapped to 0 (black).
                                //this.depthPixels[i] = (byte)(depth >= depthFrame.DepthMinReliableDistance && depth <= maxDepth ? (depth / MapDepthToByte) : 0);
                                this.depthPixels[i] = (byte)(depth >= depthFrame.DepthMinReliableDistance && depth <= maxDepth ? (depth / MapDepthToByte) : 0);
                            }
                            //kinectSensor.CoordinateMapper.
                            //(udepth, colorSpacePoints);

                            depthFrameProcessed = true;
                        }
                    }
                }
            }

            if (depthFrameProcessed)
            {
                Rectangle rect = new Rectangle(0, 0, depthBitmap.Width, depthBitmap.Height);
                BitmapData bitmapData = depthBitmap.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                IntPtr ptrSrc = bitmapData.Scan0;

                Marshal.Copy(depthPixels, 0, ptrSrc, depthPixels.Length);
                this.depthBitmap.UnlockBits(bitmapData);
                pictureBoxDepth.Height = (int)((float)pictureBoxColor.Width / depthBitmap.Width * depthBitmap.Height);
                pictureBoxDepth.Image = depthBitmap;
            }
        }

        /// <summary>
        /// Directly accesses the underlying image buffer of the DepthFrame to 
        /// create a displayable bitmap.
        /// This function requires the /unsafe compiler option as we make use of direct
        /// access to the native memory pointed to by the depthFrameData pointer.
        /// </summary>
        /// <param name="depthFrameData">Pointer to the DepthFrame image data</param>
        /// <param name="depthFrameDataSize">Size of the DepthFrame image data</param>
        /// <param name="minDepth">The minimum reliable depth value for the frame</param>
        /// <param name="maxDepth">The maximum reliable depth value for the frame</param>
        private unsafe void ProcessDepthFrameData(IntPtr depthFrameData, uint depthFrameDataSize, ushort minDepth, ushort maxDepth)
        {
            // depth frame data is a 16 bit value
            ushort* frameData = (ushort*)depthFrameData;

            // convert depth to a visual representation
            for (int i = 0; i < (int)(depthFrameDataSize / this.depthFrameDescription.BytesPerPixel); ++i)
            {
                // Get the depth for this pixel
                ushort depth = frameData[i];

                // To convert to a byte, we're mapping the depth value to the byte range.
                // Values outside the reliable depth range are mapped to 0 (black).
                this.depthPixels[i] = (byte)(depth >= minDepth && depth <= maxDepth ? (depth / MapDepthToByte) : 0);
            }
        }

        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            // ColorFrame is IDisposable
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;
                    
                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {

                        Rectangle rect = new Rectangle(0, 0, colorBitmap.Width, colorBitmap.Height);
                        BitmapData bitmapData = colorBitmap.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        IntPtr ptrSrc = bitmapData.Scan0;
                        
                        // verify data and write the new color frame data to the display bitmap
                        if ((colorFrameDescription.Width == this.colorBitmap.Width) && (colorFrameDescription.Height == this.colorBitmap.Height))
                        {
                            colorFrame.CopyConvertedFrameDataToIntPtr(
                                ptrSrc,
                                (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                ColorImageFormat.Bgra);
                        }

                        Marshal.Copy(ptrSrc, colorPixels, 0, colorFrameDescription.Width * colorFrameDescription.Height * 4);
                        this.colorBitmap.UnlockBits(bitmapData);
                    }
                }

                pictureBoxColor.Height = (int)((float)pictureBoxColor.Width / colorBitmap.Width * colorBitmap.Height);
                pictureBoxColor.Image = colorBitmap;
                mainForm.picScalaFactor = (float)pictureBoxColor.Width / (float)colorBitmap.Width;
            }
        }

        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
         
        }

        public void close() {
            if (this.colorFrameReader != null)
            {
                // ColorFrameReder is IDisposable
                this.colorFrameReader.Dispose();
                this.colorFrameReader = null;
            }
            if (this.depthFrameReader != null)
            {
                // ColorFrameReder is IDisposable
                this.depthFrameReader.Dispose();
                this.depthFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }

        public void convert() {
           
        }

        /**
         * 相机开启版本2模式，深度流和彩色流同时进入
         */
        private MultiSourceFrameReader reader;
        public object rawDataLock = new object();
        public void InitV2(PictureBox picBoxColor, PictureBox picBoxDepth, MainForm mainForm)
        {
            this.mainForm = mainForm;
            // get the kinectSensor object
            this.kinectSensor = KinectSensor.GetDefault();
            this.reader = this.kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Depth | FrameSourceTypes.Color);

            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.FrameDescription;
            this.colorPixels = new byte[colorFrameDescription.Width * colorFrameDescription.Height * 4];
            this.colorBitmap = new Bitmap(colorFrameDescription.Width, colorFrameDescription.Height, 0, System.Drawing.Imaging.PixelFormat.Format32bppArgb, IntPtr.Zero);

            this.depthFrameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;
            // allocate space to put the pixels being received and converted
            this.depthPixels = new byte[this.depthFrameDescription.Width * this.depthFrameDescription.Height];
            this.udepth = new ushort[depthPixels.Length];
            this.colorSpacePoints = new ColorSpacePoint[depthPixels.Length];
            this.depthBitmap = new Bitmap(this.depthFrameDescription.Width, this.depthFrameDescription.Height, 0, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, IntPtr.Zero);
            // 不搞调色板，8位bmp会出现伪彩色
            depthBitmap.Palette = palette;

            // Add an event handler to be called whenever depth and color both have new data
            this.reader.MultiSourceFrameArrived += this.Reader_MultiSourceFrameArrived;

            pictureBoxColor = picBoxColor;
            pictureBoxDepth = picBoxDepth;

            // open the sensor
            this.kinectSensor.Open();
        }

        private void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e) {
            bool validDepth = false;
            bool validColor = false;

            MultiSourceFrameReference frameReference = e.FrameReference;
            MultiSourceFrame multiSourceFrame = null;
            DepthFrame depthFrame = null;
            ColorFrame colorFrame = null;

            try
            {
                multiSourceFrame = frameReference.AcquireFrame();
                depthCameraIntrinsics = kinectSensor.CoordinateMapper.GetDepthCameraIntrinsics();

                if (multiSourceFrame != null)
                {
                    // MultiSourceFrame is IDisposable
                    lock (this.rawDataLock)
                    {
                        ColorFrameReference colorFrameReference = multiSourceFrame.ColorFrameReference;
                        DepthFrameReference depthFrameReference = multiSourceFrame.DepthFrameReference;

                        colorFrame = colorFrameReference.AcquireFrame();
                        depthFrame = depthFrameReference.AcquireFrame();

                        if ((depthFrame != null) && (colorFrame != null))
                        {
                            // 彩色流
                            FrameDescription colorFrameDescription = colorFrame.FrameDescription;
                            int colorWidth = colorFrameDescription.Width;
                            int colorHeight = colorFrameDescription.Height;
                            if ((colorFrameDescription.Width == this.colorBitmap.Width) && (colorFrameDescription.Height == this.colorBitmap.Height))
                            {
                                Rectangle rect = new Rectangle(0, 0, colorBitmap.Width, colorBitmap.Height);
                                BitmapData bitmapData = colorBitmap.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                IntPtr ptrSrc = bitmapData.Scan0;
                                colorFrame.CopyConvertedFrameDataToIntPtr(
                                    ptrSrc,
                                    (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                    ColorImageFormat.Bgra);
                                Marshal.Copy(ptrSrc, colorPixels, 0, colorFrameDescription.Width * colorFrameDescription.Height * 4);
                                this.colorBitmap.UnlockBits(bitmapData);
                                pictureBoxColor.Height = (int)((float)pictureBoxColor.Width / colorBitmap.Width * colorBitmap.Height);
                                pictureBoxColor.Image = colorBitmap;
                                mainForm.picScalaFactor = (float)pictureBoxColor.Width / (float)colorBitmap.Width;
                            }

                            // 深度流
                            FrameDescription depthFrameDescription = depthFrame.FrameDescription;
                            int depthWidth = depthFrameDescription.Width;
                            int depthHeight = depthFrameDescription.Height;
                            if ((depthWidth * depthHeight) == this.udepth.Length)
                            {
                                depthFrame.CopyFrameDataToArray(this.udepth);
                                ushort maxDepth = ushort.MaxValue;
                                maxDepth = depthFrame.DepthMaxReliableDistance;
                                for (int i = 0; i < udepth.Length; ++i)
                                {
                                    ushort depth = udepth[i];
                                    this.depthPixels[i] = (byte)(depth >= depthFrame.DepthMinReliableDistance && depth <= maxDepth ? (depth / MapDepthToByte) : 0);
                                }

                                Rectangle rect = new Rectangle(0, 0, depthBitmap.Width, depthBitmap.Height);
                                BitmapData bitmapData = depthBitmap.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                                IntPtr ptrSrc = bitmapData.Scan0;
                                Marshal.Copy(depthPixels, 0, ptrSrc, depthPixels.Length);
                                this.depthBitmap.UnlockBits(bitmapData);
                                pictureBoxDepth.Height = (int)((float)pictureBoxColor.Width / depthBitmap.Width * depthBitmap.Height);
                                pictureBoxDepth.Image = depthBitmap;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignore if the frame is no longer available
            }
            finally
            {
                // MultiSourceFrame, DepthFrame, ColorFrame, BodyIndexFrame are IDispoable
                if (depthFrame != null)
                {
                    depthFrame.Dispose();
                    depthFrame = null;
                }

                if (colorFrame != null)
                {
                    colorFrame.Dispose();
                    colorFrame = null;
                }

                if (multiSourceFrame != null)
                {
                    multiSourceFrame = null;
                }
            }
        }
    }
}
