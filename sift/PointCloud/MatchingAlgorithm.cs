using Emgu.CV.Structure;
using Emgu.CV;
using sift.PFH;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System;
using sift.common;

namespace sift.PointCloud
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

        // 两个无序点云中寻找到对应的点，根据相关系数R进行限制
        public static void pointCloudMatch(List<PointCloud3D> sourcePC, List<PointCloud3D> targetPC, double limitR,
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

        public static void Correlation_coefficient(Bitmap tempImg, Bitmap curImg) {
/*            Image<Bgr, Byte> templateImage = BitmapExtensions.ToImage(tempImg);
            Image<Bgr, Byte> currentImage = BitmapExtensions.ToImage(curImg);
            templateImage.sub*/
        }

        public static void test()
        {
            List<PointCloud3D> spoint = new List<PointCloud3D>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    spoint.Add(new PointCloud3D(i + 0.11, j + 0.11, 100.11));
                }
            }
            List<PointCloud3D> tpoint = new List<PointCloud3D>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tpoint.Add(new PointCloud3D(i + 0.1, j + 0.1, 100.1));
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
            MatchingAlgorithm.pointCloudMatch(fpfhs, tpfhs, 0.9, out spointClouds, out tpointClouds);

            double[,] r;
            double[] t;
            SvdRT.RegisterPointCloud(tpointClouds, spointClouds, out r, out t);
            int a = 0;
        }
    }
}
