using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sift.common
{
    internal class sift
    {
        public static void siftEmgu(String firstPicPath, String secondPicPath, List<PointF> polygonPoints)
        {
            Image<Gray, Byte> originPic = new Image<Gray, byte>(firstPicPath);

            SIFT sift = new SIFT(1000);
            //计算特征点
            Mat srcImg1 = CvInvoke.Imread(firstPicPath);
            Mat srcImg2 = CvInvoke.Imread(secondPicPath);
            MKeyPoint[] keyPoints1 = sift.Detect(srcImg1);
            MKeyPoint[] keyPoints2 = sift.Detect(srcImg2);
            //绘制特征点
            Mat sift_feature1 = new Mat();
            Mat sift_feature2 = new Mat();
            VectorOfKeyPoint vkeyPoint1 = new VectorOfKeyPoint(keyPoints1);
            VectorOfKeyPoint vkeyPoint2 = new VectorOfKeyPoint(keyPoints2);
            Features2DToolbox.DrawKeypoints(srcImg1, vkeyPoint1, sift_feature1, new Bgr(0, 255, 0),
                Features2DToolbox.KeypointDrawType.Default);
            Features2DToolbox.DrawKeypoints(srcImg2, vkeyPoint2, sift_feature2, new Bgr(0, 255, 0));
            //显示绘制结果
            CvInvoke.NamedWindow("sift_feature1", Emgu.CV.CvEnum.WindowFlags.Normal);
            CvInvoke.NamedWindow("sift_feature2", Emgu.CV.CvEnum.WindowFlags.Normal);
            CvInvoke.Imshow("sift_feature1", sift_feature1);
            CvInvoke.Imshow("sift_feature2", sift_feature2);

            Mat scharrImg1_x = new Mat();
            CvInvoke.Scharr(srcImg1, scharrImg1_x, Emgu.CV.CvEnum.DepthType.Cv8U, 1, 0);
            CvInvoke.Imshow("sift_feature1", scharrImg1_x);
            int a = 0;
        }
    }
}
