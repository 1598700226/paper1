using Emgu.CV.Flann;
using Emgu.CV.OCR;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.Optimization;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace sift.PointCloudHandler
{
    public class PointCloud3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Pic_X { get; set; }
        public double Pic_Y { get; set; }
        public double Pic_Z { get; set; }

        public Color color { get; set; }

        private int H { get; set; }

        // 点云特征
        public bool hasNormal;
        public double[,] n; // 1行3列 x[0,0] y[0,1] z[0,2]
        public bool hasSpfh;
        public int[] spfh;
        public double[] fpfh;

        // 点云分类
        public bool isVisited = false;
        public int clusterId = 0;

        public PointCloud3D(double xw, double yw, double zw)
        {
            X = xw;
            Y = yw;
            Z = zw;
            hasSpfh = false;
        }

        public PointCloud3D(double xw, double yw, double zw, double pic_x, double pic_y, double pic_z)
        {
            X = xw;
            Y = yw;
            Z = zw;
            Pic_X = pic_x;
            Pic_Y = pic_y;
            Pic_Z = pic_z;
            hasSpfh = false;
        }

        public PointCloud3D(double xw, double yw, double zw, double pic_x, double pic_y, double pic_z, Color pointColor)
        {
            X = xw;
            Y = yw;
            Z = zw;
            Pic_X = pic_x;
            Pic_Y = pic_y;
            Pic_Z = pic_z;
            color = pointColor;
            hasSpfh = false;
        }

        public PointCloud3D(PointCloud3D pt)
        {
            X = pt.X;
            Y = pt.Y;
            Z = pt.Z;
            Pic_X = pt.Pic_X;
            Pic_Y = pt.Pic_Y;
            Pic_Z = pt.Pic_Z;
            hasSpfh = pt.hasSpfh;
            hasNormal = pt.hasNormal;
            if (hasNormal) {
                n = new double[1, 3] { { pt.n[0, 0], pt.n[0, 1], pt.n[0, 2] } };
            }
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }

        public double Distance(PointCloud3D other)
        {
            double dx = Math.Pow(X - other.X, 2);
            double dy = Math.Pow(Y - other.Y, 2);
            double dz = Math.Pow(Z - other.Z, 2);
            return Math.Sqrt(dx + dy + dz);
        }

        // 点云降采样-体素
        public static List<PointCloud3D> downSamplingTisu(int picWidth, int picHeight, int r, List<PointCloud3D> pointCloud3Ds) {
            double xmax, ymax, zmax;
            xmax = ymax = zmax = int.MinValue;
            double xmin, ymin, zmin;
            xmin = ymin = zmin = int.MaxValue;

            // 1.计算区域
            foreach(PointCloud3D item in pointCloud3Ds)
            {
                double z_mm = item.Z;
                double x_mm = item.X;
                double y_mm = item.Y;

                if (z_mm < 0)
                {
                    continue;
                }
                zmin = zmin < z_mm ? zmin : z_mm;
                zmax = zmax > z_mm ? zmax : z_mm;
                xmin = xmin < x_mm ? xmin : x_mm;
                xmax = xmax > x_mm ? xmax : x_mm;
                ymin = ymin < y_mm ? ymin : y_mm;
                ymax = ymax > y_mm ? ymax : y_mm;
            }

            // 2.计算维度, 根据维度排序
            int Dx = (int)((xmax - xmin) / r);
            int Dy = (int)((ymax - ymin) / r);
            int Dz = (int)((zmax - zmin) / r);
            foreach (PointCloud3D item in pointCloud3Ds)
            {
                int hx = (int)((item.X - xmin) / r);
                int hy = (int)((item.Y - ymin) / r);
                int hz = (int)((zmax - zmin) / r);
                item.H = hx + hy * Dx + hz * Dy * Dz;
            }
            pointCloud3Ds.Sort((a, b) => a.H.CompareTo(b.H));

            // 3.找到相同维度，取均值实现降采样
            List<PointCloud3D> filterPointClouds = new List<PointCloud3D>();
            for (int i = 0, j = 0; i < pointCloud3Ds.Count - 1; i++)
            {
                if (pointCloud3Ds[i].H == pointCloud3Ds[i + 1].H)
                {
                    continue;
                }
                else
                {
                    filterPointClouds.Add(pointCloud3Ds[i]);
/*                    double x, y, z;
                    double pic_x, pic_y, pic_z;
                    x = pointCloud3Ds[i].X;
                    y = pointCloud3Ds[i].Y;
                    z = pointCloud3Ds[i].Z;
                    pic_x = pointCloud3Ds[i].Pic_X;
                    pic_y = pointCloud3Ds[i].Pic_Y;
                    pic_z = pointCloud3Ds[i].Pic_Z;
                    filterPointClouds.Add(new PointCloud3D(x, y, z, pic_x, pic_y, pic_z, pointCloud3Ds[i].color));*/
                }
            }

            return filterPointClouds;
        }
        public static List<PointCloud3D> downSamplingTisu(int r, List<PointCloud3D> pointCloud3Ds)
        {
            double xmax, ymax, zmax;
            xmax = ymax = zmax = int.MinValue;
            double xmin, ymin, zmin;
            xmin = ymin = zmin = int.MaxValue;

            // 1.计算区域
            foreach (PointCloud3D item in pointCloud3Ds)
            {
                double z_mm = item.Z;
                double x_mm = item.X;
                double y_mm = item.Y;

                if (z_mm < 0)
                {
                    continue;
                }
                zmin = zmin < z_mm ? zmin : z_mm;
                zmax = zmax > z_mm ? zmax : z_mm;
                xmin = xmin < x_mm ? xmin : x_mm;
                xmax = xmax > x_mm ? xmax : x_mm;
                ymin = ymin < y_mm ? ymin : y_mm;
                ymax = ymax > y_mm ? ymax : y_mm;
            }

            // 2.计算维度, 根据维度排序
            int Dx = (int)((xmax - xmin) / r);
            int Dy = (int)((ymax - ymin) / r);
            int Dz = (int)((zmax - zmin) / r);
            foreach (PointCloud3D item in pointCloud3Ds)
            {
                int hx = (int)((item.X - xmin) / r);
                int hy = (int)((item.Y - ymin) / r);
                int hz = (int)((zmax - zmin) / r);
                item.H = hx + hy * Dx + hz * Dy * Dz;
            }
            pointCloud3Ds.Sort((a, b) => a.H.CompareTo(b.H));

            // 3.找到相同维度，取均值实现降采样
            List<PointCloud3D> filterPointClouds = new List<PointCloud3D>();
            for (int i = 0, j = 0; i < pointCloud3Ds.Count - 1; i++)
            {
                if (pointCloud3Ds[i].H == pointCloud3Ds[i + 1].H)
                {
                    continue;
                }
                else
                {
                    filterPointClouds.Add(pointCloud3Ds[i]);
                 }
            }

            return filterPointClouds;
        }

        // 点云降采样-平均
        public static List<PointCloud3D> downSamplingAvg(int picWidth, int picHeight, int r, List<PointCloud3D> pointCloud3Ds)
        {
            List<PointCloud3D> filterPointClouds = new List<PointCloud3D>();
            for (int i = 0; i < pointCloud3Ds.Count; i++)
            {
                if (pointCloud3Ds[i].Pic_X % r == 0 || pointCloud3Ds[i].Pic_Y % r == 0) {
                    filterPointClouds.Add(pointCloud3Ds[i]);
                }
            }
            return filterPointClouds;
        }

        // 点云类型转double[,]二维数组
        public double[,] pointCloud2double()
        {
            double[,] point = new double[1, 3];
            point[0, 0] = this.X;
            point[0, 1] = this.Y;
            point[0, 2] = this.Z;
            return point;
        }

        // 相关性计算
        public double CalculateCorrelation(PointCloud3D other) {
            if (other.hasSpfh == false || this.hasSpfh == false) { 
                return double.MinValue;
            }

            // 剔除都为0的特征区间，防止干扰
            List<double> sfpfh = new List<double>();
            List<double> tfpfh = new List<double>();
            for (int i = 0; i < this.fpfh.Length; i++) {
                if (this.fpfh[i] != 0 || other.fpfh[i] != 0) {
                    sfpfh.Add(this.fpfh[i]);
                    tfpfh.Add(other.fpfh[i]);
                }
            }

            double R = MatchingAlgorithm.DiffValue(sfpfh.ToArray(), tfpfh.ToArray());
            return R;
        }

        // 基于统计方法剔除离群点
        // theta: 距离标准差的倍数，默认3
        public static List<PointCloud3D> RemoveOutliersByStatistic(List<PointCloud3D> pointCloud3Ds, double theta = 3) {
            List<PointCloud3D> afterRemove = new List<PointCloud3D>();
            List<PointCloud3D> Remove = new List<PointCloud3D>();

            KdTree kdTree = new KdTree(pointCloud3Ds);
            // 1 计算均值 存储近邻点 与 距离
            List<PointCloud3D> nearPoints = new List<PointCloud3D>();
            List<double> nearDisTance = new List<double>();
            double avgDistance = 0;
            for (int i = 0; i < pointCloud3Ds.Count; i++) {
                PointCloud3D itemPoint = pointCloud3Ds[i];
                PointCloud3D nearPoint = kdTree.KnumNearestNeighbors(itemPoint, 2)[1];
                double distance = itemPoint.Distance(nearPoint);
                nearPoints.Add(nearPoint);
                nearDisTance.Add(distance);
                avgDistance += distance;
            }
            avgDistance /= pointCloud3Ds.Count;

            // 2. 计算标准差
            double standardDeviation = 0;
            for (int i = 0; i < pointCloud3Ds.Count; i++)
            {
                PointCloud3D itemPoint = pointCloud3Ds[i];
                PointCloud3D nearPoint = nearPoints[i];
                double distance = nearDisTance[i];
                standardDeviation += Math.Pow(avgDistance - distance, 2);
            }
            standardDeviation = Math.Sqrt(standardDeviation);
            standardDeviation /= pointCloud3Ds.Count;
            standardDeviation = theta * standardDeviation;

            // 3. 剔除离群点
            for (int i = 0; i < pointCloud3Ds.Count; i++)
            {
                double diffDistance = Math.Abs(nearDisTance[i] - avgDistance);
                if (diffDistance > standardDeviation) {
                    Remove.Add(pointCloud3Ds[i]);
                }
            }
            afterRemove = pointCloud3Ds.Except(Remove).ToList();
            return afterRemove;
        }

        // 根据Z的深度直通滤波
        public static List<PointCloud3D> DirectFiltingByWorldZmm(List<PointCloud3D> pointCloud3Ds, int max, int min) { 
            List<PointCloud3D> filterPointClouds = new List<PointCloud3D>();
            foreach (PointCloud3D item in pointCloud3Ds)
            {
                if (item.Z > min && item.Z < max) { 
                    filterPointClouds.Add(item);
                }
            }
            return filterPointClouds;
        }

        // 去除地面点云
        public static List<PointCloud3D> GroundFilting(List<PointCloud3D> pointCloud3Ds, int iterationMax, double error) {
            List<PointCloud3D> filterPointClouds = pointCloud3Ds.ToList();
            // 1.Ransac算法 随机取3点 并找到最优的平面
            int iteration = 0;
            Random randomGenerator = new Random();
            List<PointCloud3D> bestInlines = new List<PointCloud3D>();
            while (iteration < iterationMax) { 
                iteration++;
                Vector<double>[] randomPcs = new Vector<double>[3];
                for (int i = 0; i < randomPcs.Length; i++) {
                    int index = randomGenerator.Next(0, pointCloud3Ds.Count);
                    PointCloud3D item = pointCloud3Ds[index];
                    randomPcs[i] = Vector<double>.Build.Dense(new double[] { item.X, item.Y, item.Z });
                }
                Vector<double> v01 = Vector<double>.Build.Dense(new double[] { 
                    randomPcs[1][0] - randomPcs[0][0],
                    randomPcs[1][1] - randomPcs[0][1],
                    randomPcs[1][2] - randomPcs[0][2]});
                Vector<double> v02 = Vector<double>.Build.Dense(new double[] { 
                    randomPcs[2][0] - randomPcs[0][0],
                    randomPcs[2][1] - randomPcs[0][1],
                    randomPcs[2][2] - randomPcs[0][2]});
                Vector<double> normal = Vector<double>.Build.Dense(new double[] {
                    v01[1] * v02[2] - v01[2] * v02[1],
                    v01[2] * v02[0] - v01[0] * v02[2],
                    v01[0] * v02[1] - v01[1] * v02[0]});
                // Ax + By + Cz + D = 0 得到参数
                normal = normal.Normalize(1);
                double d = -(normal[0] * randomPcs[0][0] + normal[1] * randomPcs[0][1] + normal[2] * randomPcs[0][2]);
                double[] model = new double[4] { normal[0], normal[1], normal[2], d };

                // 统计inliers
                List<PointCloud3D> inliers = new List<PointCloud3D>();
                for (int i = 0; i < pointCloud3Ds.Count; i++)
                {
                    PointCloud3D item = pointCloud3Ds[i];
                    double dis = Math.Abs(item.X * model[0] + item.Y * model[1] + item.Z * model[2] + model[3]);
                    if (dis <= error)
                    {
                        inliers.Add(item);
                    }
                }
                if (inliers.Count > bestInlines.Count) {
                    bestInlines = inliers.ToList();
                }
            }

            return filterPointClouds.Except(bestInlines).ToList();
        }
    }
}
