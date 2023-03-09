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
    internal class Sift
    {
        public static void siftEmgu(String firstPicPath, String secondPicPath, List<PointF> polygonPoints)
        {
            Image<Gray, Byte> originPic = new Image<Gray, byte>(firstPicPath);
            Image<Gray, Byte> deformPic = new Image<Gray, byte>(secondPicPath); 
            SIFT sift = new SIFT(1000);
            //计算特征点
            Mat srcImg1 = CvInvoke.Imread(firstPicPath);
            Mat srcImg2 = CvInvoke.Imread(secondPicPath);
            MKeyPoint[] keyPoints1 = sift.Detect(originPic);
            MKeyPoint[] keyPoints2 = sift.Detect(deformPic);
            
            //绘制特征点
            Mat sift_feature1 = new Mat();
            Mat sift_feature2 = new Mat();
            VectorOfKeyPoint vkeyPoint1 = new VectorOfKeyPoint(keyPoints1);
            VectorOfKeyPoint vkeyPoint2 = new VectorOfKeyPoint(keyPoints2);
            Features2DToolbox.DrawKeypoints(srcImg1, vkeyPoint1, sift_feature1, new Bgr(0, 255, 0),
                Features2DToolbox.KeypointDrawType.Default);
            Features2DToolbox.DrawKeypoints(srcImg2, vkeyPoint2, sift_feature2, new Bgr(0, 255, 0),
                Features2DToolbox.KeypointDrawType.Default);
            //显示绘制结果
            CvInvoke.NamedWindow("sift_feature1", Emgu.CV.CvEnum.WindowFlags.Normal);
            CvInvoke.NamedWindow("sift_feature2", Emgu.CV.CvEnum.WindowFlags.Normal);
            CvInvoke.Imshow("sift_feature1", sift_feature1);
            CvInvoke.Imshow("sift_feature2", sift_feature2);
            //计算特征描述符
            Mat descriptors1 = new Mat();
            Mat descriptors2 = new Mat();
            sift.Compute(srcImg1, vkeyPoint1, descriptors1);
            sift.Compute(srcImg2, vkeyPoint2, descriptors2);
/*            //使用BF匹配器进行暴力匹配
            BFMatcher bFMatcher = new BFMatcher(DistanceType.L2);
            VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();
            //添加特征描述符
            bFMatcher.Add(descriptors1);
            //k最邻近匹配
            bFMatcher.KnnMatch(descriptors2, matches, 2, null);
            //寻找匹配结果中距离的最值
            double min_dist = 100, max_dist = 0;
            for (int i = 0; i < descriptors1.Rows; i++)
            {

                if (matches[i][0].Distance > max_dist)
                {

                    max_dist = matches[i][0].Distance;
                }
                if (matches[i][0].Distance < min_dist)
                {

                    min_dist = matches[i][0].Distance;
                }
            }
            //对BF匹配结果进行筛选
            VectorOfVectorOfDMatch good_matches = new VectorOfVectorOfDMatch();
            for (int i = 0; i < matches.Size; i++)
            {

                //符合条件的匹配点进行存储
                if (matches[i][0].Distance < 1.5 * min_dist)
                {

                    good_matches.Push(matches[i]);
                }
            }
            //绘制匹配点
            Mat result = new Mat();
            Features2DToolbox.DrawMatches(srcImg1, vkeyPoint1, srcImg2, vkeyPoint2, good_matches,
                result, new MCvScalar(0, 255, 0), new MCvScalar(0, 0, 255), null);
            //显示匹配结果
            CvInvoke.NamedWindow("match-result", Emgu.CV.CvEnum.WindowFlags.Normal);
            CvInvoke.Imshow("match-result", result);*/
        }
    }
}
