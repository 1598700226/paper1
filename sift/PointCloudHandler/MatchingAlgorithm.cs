using Emgu.CV.Structure;
using Emgu.CV;
using sift.PFH;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System;
using sift.common;
using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;

namespace sift.PointCloudHandler
{
    public static class MatchingAlgorithm
    {
        public static double Correlation_coefficient(double[] ImgRef, double[] ImgTest)
        {
            //循环变量
            int num = ImgRef.Count();
            int i;
            double TmpSum = 0, TstSum = 0;
            double TmpAve, TstAve;
            double TmpSig = 0, TstSig = 0, PSSig = 0;
            //求和
            for (i = 0; i < num; i++)
            {
                TmpSum += ImgRef[i];
                TstSum += ImgTest[i];
            }
            //求平均数
            TmpAve = TmpSum / num;
            TstAve = TstSum / num;

            for (i = 0; i < num; i++)
            {
                TmpSig += (ImgRef[i] - TmpAve) * (ImgRef[i] - TmpAve);
                TstSig += (ImgTest[i] - TstAve) * (ImgTest[i] - TstAve);
                PSSig += (ImgRef[i] - TmpAve) * (ImgTest[i] - TstAve);
            }
            //求相关系数
            double R = (PSSig * PSSig / (TmpSig * TstSig));
            return R;
        }
        public static double Correlation_coefficient(byte[] ImgRef, byte[] ImgTest)
        {
            //循环变量
            int num = ImgRef.Count();
            int i;
            int TmpSum = 0, TstSum = 0;
            double TmpAve, TstAve;
            double TmpSig = 0, TstSig = 0, PSSig = 0;
            //求和
            for (i = 0; i < num; i++)
            {
                TmpSum += (int)ImgRef[i];
                TstSum += (int)ImgTest[i];
            }
            //求平均数
            TmpAve = (double)TmpSum / (double)(num);
            TstAve = (double)TstSum / (double)(num);

            for (i = 0; i < num; i++)
            {
                TmpSig += (ImgRef[i] - TmpAve) * (ImgRef[i] - TmpAve);
                TstSig += (ImgTest[i] - TstAve) * (ImgTest[i] - TstAve);
                PSSig += (ImgRef[i] - TmpAve) * (ImgTest[i] - TstAve);
            }
            //求相关系数
            double R = (double)(PSSig * PSSig / (TmpSig * TstSig));
            return R;
        }

        public static double DiffValue(double[] ImgRef, double[] ImgTest)
        {
            //循环变量
            int num = ImgRef.Length;

            double err = 0;
            for (int i = 0; i < num; i++)
            {
                err += (ImgRef[i] - ImgTest[i]) * (ImgRef[i] - ImgTest[i]);
            }
            //求相关系数
            double R = 1 - err;
            return R;
        }

        /**
         *  无效
         */
        // 两个无序点云中寻找到对应的点，根据相关系数R进行限制
        public static void PointCloudMatch(List<PointCloud3D> sourcePC, List<PointCloud3D> targetPC, double limitR,
            out List<PointCloud3D> matchSourcePC, out List<PointCloud3D> matchTargetPC) {
            matchSourcePC = new List<PointCloud3D>(); 
            matchTargetPC = new List<PointCloud3D>();

            for (int i = 0; i < sourcePC.Count; i++) {
                PointCloud3D bestMatchPoint = null;
                double maxR = limitR;
                for (int j = 0; j < targetPC.Count; j++) {
                    double tempR = sourcePC[i].CalculateCorrelation(targetPC[j]);
                    if (tempR > maxR) {
                        maxR = tempR;
                        bestMatchPoint = targetPC[j];
                    }
                }
                if (bestMatchPoint != null) {
                    matchSourcePC.Add(sourcePC[i]);
                    matchTargetPC.Add(bestMatchPoint);
                }
            }
        }

        public static void PointCloudMatch(List<PointCloud3D> sourcePC, Image<Gray, Byte> sourceImage, Image<Gray, Byte> targetImage,
           out List<MatchPointResult> matchPointResults, int subSetSize, int range, double limitR = 0.5)
        {
            matchPointResults = new List<MatchPointResult>();
            int limitBoundary = subSetSize / 2 + 1 + range;

            for (int i = 0; i < sourcePC.Count; i++)
            {
                PointCloud3D itemPc = sourcePC[i];
                // 限制边界部分
                if (itemPc.Pic_X + limitBoundary > sourceImage.Width || itemPc.Pic_X - limitBoundary < 0 ||
                    itemPc.Pic_Y + limitBoundary > sourceImage.Height || itemPc.Pic_Y - limitBoundary < 0)
                    continue;
               
                MatchPointResult matchPointResult = Correlation_coefficient(sourceImage, targetImage, itemPc.Pic_X, itemPc.Pic_Y, subSetSize, subSetSize, range, range);
                if (matchPointResult.R > limitR) {
                    matchPointResults.Add(matchPointResult);
                }
            
            }
        }
            
        public static MatchPointResult Correlation_coefficient(Image<Gray, Byte> tempImg, Image<Gray, Byte> curImg, double xt, double yt,
            int subsetSizeWidth, int subsetSizeHeight, int rangeWidth, int rangeHeight, int offset_x = 0, int offset_y = 0) {

            // 通过插值，获取参考图案
            int integerCoordinate_x = (int)xt;
            int integerCoordinate_y = (int)yt;
            double dif_x = xt - integerCoordinate_x;
            double dif_y = yt - integerCoordinate_y;
            Rectangle rect_t = new Rectangle(integerCoordinate_x - subsetSizeWidth / 2, integerCoordinate_y - subsetSizeHeight / 2, subsetSizeWidth + 1, subsetSizeHeight + 1);
            Image<Gray, float> image_t = tempImg.GetSubRect(rect_t).Convert<Gray, float>(); ;
            Image<Gray, float> img_t = new Image<Gray, float>(new Size(subsetSizeWidth, subsetSizeHeight));
            for (int iy = 0; iy < subsetSizeHeight; iy++)
                for (int ix = 0; ix < subsetSizeWidth; ix++)
                {
                    img_t.Data[iy, ix, 0] = (float)Algorithm.bilinearInterpolation(image_t, ix + dif_x, iy + dif_y);
                }

            // 在变形图案中确认搜索范围
            int rect_c_left = (integerCoordinate_x - subsetSizeWidth / 2 - rangeWidth + offset_x < 0 ? 0 : integerCoordinate_x - subsetSizeWidth / 2 - rangeWidth + offset_x);
            int rect_c_top = (integerCoordinate_y - subsetSizeHeight / 2 - rangeHeight + offset_y < 0 ? 0 : integerCoordinate_y - subsetSizeHeight / 2 - rangeHeight + offset_y);
            int rect_c_width = (subsetSizeWidth + 2 * rangeWidth > curImg.Width - rect_c_left ? curImg.Width - rect_c_left : subsetSizeWidth + 2 * rangeWidth);
            int rect_c_height = (subsetSizeHeight + 2 * rangeHeight > curImg.Height - rect_c_top ? curImg.Height - rect_c_top : subsetSizeHeight + 2 * rangeHeight);
            Rectangle rect_c = new Rectangle(rect_c_left, rect_c_top,
                rect_c_width, rect_c_height);
            Image<Gray, float> image_c = curImg.GetSubRect(rect_c).Convert<Gray, float>();

            // 获取匹配结果
            Mat result = new Mat();
            CvInvoke.MatchTemplate(img_t, image_c, result, TemplateMatchingType.CcoeffNormed);
            Point max_loc = new Point();
            Point min_loc = new Point();
            double max = 0, min = 0;
            CvInvoke.MinMaxLoc(result, ref min, ref max, ref min_loc, ref max_loc);//获得极值信息 
            int xpeak = max_loc.X;
            int ypeak = max_loc.Y;
            float[] result_float = new float[result.Height * result.Width];
            Marshal.Copy(result.DataPointer, result_float, 0, result.Height * result.Width);//根据我目前的研究看，访问Emgu数据的方法，若数据元素是基础数据，也就是double, float, int这种类型，那么可以用Marshal的Copy方法，把底层数据拷贝到一个一维数组中，然后你对数组进行操作

            // 多项式拟合亚像素结果
            double epsi_x, epsi_y;
            if (xpeak == 0)
            {
                epsi_x = (-result_float[xpeak + 1 + ypeak * result.Width]) / (2 * (result_float[xpeak + 1 + ypeak * result.Width] - result_float[xpeak + ypeak * result.Width]));
                epsi_x = epsi_x > 1 ? 0.9999 : epsi_x;
                epsi_x = epsi_x < -1 ? -0.9999 : epsi_x;
            }
            else if (xpeak == result.Width - 1)
            {
                epsi_x = (result_float[xpeak - 1 + ypeak * result.Width]) / (2 * (result_float[xpeak - 1 + ypeak * result.Width] - result_float[xpeak + ypeak * result.Width]));
                epsi_x = epsi_x > 1 ? 0.9999 : epsi_x;
                epsi_x = epsi_x < -1 ? -0.9999 : epsi_x;
            }
            else
            {
                epsi_x = (result_float[xpeak - 1 + ypeak * result.Width] - result_float[xpeak + 1 + ypeak * result.Width]) / (2 * (result_float[xpeak - 1 + ypeak * result.Width] + result_float[xpeak + 1 + ypeak * result.Width] - 2 * result_float[xpeak + ypeak * result.Width]));
                epsi_x = epsi_x > 1 ? 0.9999 : epsi_x;
                epsi_x = epsi_x < -1 ? -0.9999 : epsi_x;
            }

            if (ypeak == 0)
            {
                epsi_y = (-result_float[xpeak + (ypeak + 1) * result.Width]) / (2 * (result_float[xpeak + (ypeak + 1) * result.Width] - result_float[xpeak + ypeak * result.Width]));
                epsi_y = epsi_y > 1 ? 0.9999 : epsi_y;
                epsi_y = epsi_y < -1 ? -0.9999 : epsi_y;
            }
            else if (ypeak == result.Width - 1)
            {
                epsi_y = (result_float[xpeak + (ypeak - 1) * result.Width]) / (2 * (result_float[xpeak + (ypeak - 1) * result.Width] - result_float[xpeak + ypeak * result.Width]));
                epsi_y = epsi_y > 1 ? 0.9999 : epsi_y;
                epsi_y = epsi_y < -1 ? -0.9999 : epsi_y;
            }
            else
            {
                epsi_y = (result_float[xpeak + (ypeak - 1) * result.Width] - result_float[xpeak + (ypeak + 1) * result.Width]) / (2 * (result_float[xpeak + (ypeak - 1) * result.Width] + result_float[xpeak + (ypeak + 1) * result.Width] - 2 * result_float[xpeak + ypeak * result.Width]));
                epsi_y = epsi_y > 1 ? 0.9999 : epsi_y;
                epsi_y = epsi_y < -1 ? -0.9999 : epsi_y;
            }
            double sx = rect_c_left + xpeak + epsi_x + subsetSizeWidth / 2;
            double sy = rect_c_top + ypeak + epsi_y + subsetSizeHeight / 2;

            MatchPointResult matchPointResult = new MatchPointResult(xt, yt, sx, sy, max);
            return matchPointResult;
        }

        /**
         测试用
         */
        public static void test()
        {
            List<PointCloud3D> spoint = new List<PointCloud3D>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    spoint.Add(new PointCloud3D(i + 0.11, 100.11, j + 0.11, 
                        i + 0.11, 100.11, j + 0.11));
                }
            }
            List<PointCloud3D> tpoint = new List<PointCloud3D>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tpoint.Add(new PointCloud3D(i + 0.1, j + 0.1, 100.1,
                        i + 0.1, j + 0.1, 100.1));
                }
            }

            // 计算PFH
            KdTree skdTree = new KdTree(spoint);
            PointFetures.getNormals(skdTree, spoint, 3);
            PointFetures.SPFH(skdTree, spoint[1], 3);
            KdTree tkdTree = new KdTree(tpoint);
            PointFetures.getNormals(tkdTree, tpoint, 3);
            PointFetures.SPFH(tkdTree, tpoint[1], 3);

            // 计算FPFH
            List<PointCloud3D> fpfhs = PointFetures.FPFH(spoint, 3);
            List<PointCloud3D> tpfhs = PointFetures.FPFH(tpoint, 3);
            List<PointCloud3D> spointClouds;
            List<PointCloud3D> tpointClouds;

            // 进行匹配
            MatchingAlgorithm.PointCloudMatch(fpfhs, tpfhs, 0.9, out spointClouds, out tpointClouds);

            double[,] r;
            double[] t;
            SvdRT.RegisterPointCloud(tpointClouds, spointClouds, out r, out t);
            int a = 0;
        }
    }
}
