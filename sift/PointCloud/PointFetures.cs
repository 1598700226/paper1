using Emgu.CV;
using sift.PointCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sift.PFH
{
    public static class PointFetures
    {
        private static double[,] oPoint = new double[1, 3] { { 0, 0, 0 } };

        /// <summary>
        /// 计算PFH
        /// https://www.freesion.com/article/16421482146/
        /// </summary>
        /// <param name="ps">计算源点</param>
        /// <param name="ns">源点向量</param>
        /// <param name="pt">目标点</param>
        /// <param name="nt">目标点向量</param>
        /// <param name="f1">v和nt的夹角</param>
        /// <param name="f2">源点向量和两点向量的夹角</param>
        /// <param name="f3">目标点向量在源点坐标w和u(u是ns)的投影夹角</param>
        /// <param name="f4">距离</param>
        /// <returns></returns>
        public static bool pfh(
            double[,] ps, double[,] ns, double[,] pt, double[,] nt,
            out double f1, out double f2, out double f3, out double f4)
        {

            f4 = Math.Sqrt((ps[0, 0] - pt[0, 0]) * (ps[0, 0] - pt[0, 0]) +
                (ps[0, 1] - pt[0, 1]) * (ps[0, 1] - pt[0, 1]) +
                (ps[0, 2] - pt[0, 2]) * (ps[0, 2] - pt[0, 2]));
            if (f4 == 0.0)
            {
                f1 = f2 = f3 = f4 = 0.0;
                return false;
            }

            Matrix<double> matrix_ns = new Matrix<double>(ns);
            Matrix<double> matrix_nt = new Matrix<double>(nt);
            double[,] ptps = new double[1, 3];
            for (int i = 0; i < 3; i++)
            {
                ptps[0, i] = (pt[0, i] - ps[0, i]) / f4;
            }
            Matrix<double> matrix_ptps = new Matrix<double>(ptps);

            double angle1 = Math.Round(matrix_ns.DotProduct(matrix_ptps), 6);
            double angle2 = Math.Round(matrix_nt.DotProduct(matrix_ptps), 6);
            if (Math.Acos(Math.Abs(angle1)) > Math.Acos(Math.Abs(angle2)))
            {
                matrix_ns = new Matrix<double>(nt);
                matrix_nt = new Matrix<double>(ns);
                matrix_ptps = -1 * matrix_ptps;
                f2 = -angle2;
            }
            else
            {
                f2 = angle1;
            }

            Mat v = matrix_ptps.Mat.Cross(matrix_ns);
            Matrix<double> matrix_v = new Matrix<double>(1, 3, 1);
            v.CopyTo(matrix_v);
            if (matrix_v.Norm == 0)
            {
                f1 = f2 = f3 = f4 = 0.0;
                return false;
            }

            Mat w = matrix_ns.Mat.Cross(matrix_v);
            f1 = v.Dot(matrix_nt);

            double fw = w.Dot(matrix_nt);
            double fv = v.Dot(matrix_nt);
            f3 = Math.Atan2(fw, fv);

            return true;
        }

        // 根据近邻点计算spfh的值
/*        public static bool SPFH(KdTree pointKdTree, PointCloud3D pointCloud3D, int range)
        {
            bool ret = false;
            if (pointCloud3D.hasNormal == false) {
                return ret;
            }

            List<PointCloud3D> pointCloud3Ds = pointKdTree.RNearestNeighbors(pointCloud3D, range);
            if (pointCloud3Ds.Count == 0)
            {
                return ret;
            }


            int length = 11;
            int[] alpha = new int[length];
            int[] fai = new int[length];
            int[] theta = new int[length];
            SPFH spfh = new SPFH(alpha, fai, theta, length);


            for (int i = 0; i < pointCloud3Ds.Count; i++)
            {
                if (pointCloud3Ds[i].hasNormal == false) {
                    continue;
                }

                double[,] pt = pointCloud3Ds[i].pointCloud2double();
                double[,] nt = pointCloud3Ds[i].n;
                double[,] ps = pointCloud3D.pointCloud2double();
                double[,] ns = pointCloud3D.n;
                double f1, f2, f3, f4;
                bool pfhResult = pfh(ps, ns, pt, nt, out f1, out f2, out f3, out f4);
                if (pfhResult == false)
                {
                    continue;
                }
                spfh.AddFeatureStatistic(ref spfh.alpha, f1);
                spfh.AddFeatureStatistic(ref spfh.fai, f2);
                spfh.AddFeatureStatistic(ref spfh.theta, f3);
                ret = true;
            }

            pointCloud3D.hasSpfh = ret;
            pointCloud3D.spfh = spfh.ToDoubleArray();
            return ret;
        }*/

        // 根据近邻点计算spfh的值
        public static bool SPFH(KdTree pointKdTree, PointCloud3D pointCloud3D, int range)
        {
            bool ret = false;
            if (pointCloud3D.hasNormal == false)
            {
                return ret;
            }

            List<PointCloud3D> pointCloud3Ds = pointKdTree.RNearestNeighbors(pointCloud3D, range);
            if (pointCloud3Ds.Count <= 1)
            {
                return ret;
            }

            int length = 11;
            int[] alpha = new int[length];
            int[] fai = new int[length];
            int[] theta = new int[length];
            SPFH spfh = new SPFH(alpha, fai, theta, length);

            for (int i = 0; i < pointCloud3Ds.Count; i++)
            {
                for (int j = i + 1; j < pointCloud3Ds.Count; j++) {
                    if (!pointCloud3Ds[i].hasNormal || !pointCloud3Ds[j].hasNormal)
                    {
                        continue;
                    }
                    double[,] pt = pointCloud3Ds[j].pointCloud2double();
                    double[,] nt = pointCloud3Ds[j].n;
                    double[,] ps = pointCloud3Ds[i].pointCloud2double();
                    double[,] ns = pointCloud3Ds[i].n;
                    double f1, f2, f3, f4;
                    bool pfhResult = pfh(ps, ns, pt, nt, out f1, out f2, out f3, out f4);
                    if (pfhResult == false)
                    {
                        continue;
                    }
                    spfh.AddFeatureStatistic(ref spfh.alpha, f1);
                    spfh.AddFeatureStatistic(ref spfh.fai, f2);
                    spfh.AddFeatureStatistic(ref spfh.theta, f3);
                    ret = true;

                }
            }

            pointCloud3D.hasSpfh = ret;
            pointCloud3D.spfh = spfh.ToDoubleArray();
            return ret;
        }

        public static List<PointCloud3D> FPFH(List<PointCloud3D> pointCloud3Ds, int range)
        {

            List<PointCloud3D> pointCloud3DsFPFH = new List<PointCloud3D>();

            // 1. KD树生成, 并计算自身的法线 spfh值
            KdTree kdTree = new KdTree(pointCloud3Ds);
            getNormals(kdTree, pointCloud3Ds, range);
            for (int i = 0; i < pointCloud3Ds.Count; i++)
            {
                PointCloud3D pointCloud3D = pointCloud3Ds[i];
                SPFH(kdTree, pointCloud3D, range);
            }

            // 2. 根据周围点计算fpfh值
            for (int i = 0; i < pointCloud3Ds.Count; i++)
            {
                PointCloud3D pointCloud3D = pointCloud3Ds[i];
                // 如果自己没有Spfh则过滤
                if (pointCloud3D.hasSpfh == false)
                {
                    continue;
                }

                List<PointCloud3D> nearClouds = kdTree.RNearestNeighbors(pointCloud3D, range);
                double[] fpfh = new double[pointCloud3D.spfh.Length];
                foreach (PointCloud3D item in nearClouds)
                {
                    if (item.hasSpfh == false)
                    {
                        continue;
                    }

                    double w = item.Distance(pointCloud3D);
                    if (w == 0)
                    {
                        AddToFpfh(ref fpfh, ref item.spfh, 1, 1);
                        continue;
                    }

                    AddToFpfh(ref fpfh, ref item.spfh, nearClouds.Count, w);
                }
                pointCloud3D.fpfh = fpfh;
                pointCloud3DsFPFH.Add(pointCloud3D);
            }

            return pointCloud3DsFPFH;
        }

        private static void AddToFpfh(ref double[] fpfh, ref int[] spfh, int k, double w)
        {
            for (int i = 0; i < spfh.Length; i++)
            {
                fpfh[i] += ((double)spfh[i] / w / k);
            }
        }

        public static double[,] getCenterPoint(List<PointCloud3D> pointCloud3Ds)
        {
            double x, y, z;
            x = y = z = 0;
            for (int i = 0; i < pointCloud3Ds.Count; i++)
            {
                x += pointCloud3Ds[i].X;
                y += pointCloud3Ds[i].Y;
                z += pointCloud3Ds[i].Z;
            }

            x /= pointCloud3Ds.Count;
            y /= pointCloud3Ds.Count;
            z /= pointCloud3Ds.Count;
            double[,] centerPoint = new double[1, 3] { { x, y, z } };
            return centerPoint;
        }

        public static bool getNormals(KdTree kdTree, List<PointCloud3D> pointCloud3Ds, int range) {
            for (int i = 0; i < pointCloud3Ds.Count; i++) {
                PointCloud3D itemP = pointCloud3Ds[i];

                List<PointCloud3D> nearPoints = kdTree.RNearestNeighbors(itemP, range);
                if (nearPoints.Count < 3) {
                    itemP.hasNormal = false;
                    itemP.hasSpfh = false;
                    continue;
                }

                itemP.n = SvdRT.calNormalVector(nearPoints, itemP);
                itemP.hasNormal = true;
            }

            return true;
        }


    }
}
