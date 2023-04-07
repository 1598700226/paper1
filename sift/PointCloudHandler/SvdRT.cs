using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;


/**
 * 需要Nuget下载MathNet包
*/

namespace sift.PointCloudHandler
{
    public class SvdRT
    {
        public static void RegisterPointCloud(List<PointCloud3D> sourceClouds, List<PointCloud3D> targetClouds,
            out double[,] rotation, out double[] translation)
        {
            // 生成用于运算的点云
            Matrix<double> sourceCloud = GeneratePointCloudByPicPosition(sourceClouds);
            Matrix<double> targetCloud = GeneratePointCloudByPicPosition(targetClouds);

            // 计算中心点
            Vector<double> sourceCentroid = CalculateCentroid(sourceCloud);
            Vector<double> targetCentroid = CalculateCentroid(targetCloud);

            // 每个点云点与中心点做差
            Matrix<double> centeredSourceCloud = CenterPointCloud(sourceCloud, sourceCentroid);
            Matrix<double> centeredTargetCloud = CenterPointCloud(targetCloud, targetCentroid);

            // 计算协方差矩阵
            Matrix<double> crossCovariance = CalculateCrossCovariance(centeredSourceCloud, centeredTargetCloud);

            // SVD
            Svd<double> svd = crossCovariance.Svd();

            // 旋转矩阵
            Matrix<double> rotationMatrix = svd.VT.Transpose() * svd.U.Transpose();

            // 平移矩阵
            Vector<double> translationVector = targetCentroid - rotationMatrix * sourceCentroid;
            /*            Matrix<double> test = Matrix<double>.Build.Dense(sourceCloud.RowCount, 3);
                        for (int i = 0; i < sourceCloud.RowCount; i++) {
                            Matrix<double> ret = rotationMatrix* sourceCloud.Row(i).ToColumnMatrix();
                            test[i, 0] = ret[0, 0];
                            test[i, 1] = ret[1, 0];
                            test[i, 2] = ret[2, 0];
                        }*/

            // Print out results
            Console.WriteLine("Rotation Matrix:");
            Console.WriteLine(rotationMatrix);
            Console.WriteLine("Translation Vector:");
            Console.WriteLine(translationVector);

            rotation = new double[3,3];
            for (int i = 0; i < rotationMatrix.RowCount; i++) {
                rotation[i, 0] = rotationMatrix.Row(i)[0];
                rotation[i, 1] = rotationMatrix.Row(i)[1];
                rotation[i, 2] = rotationMatrix.Row(i)[2];
            }
            translation = new double[3];
            translation[0] = translationVector[0];
            translation[1] = translationVector[1];
            translation[2] = translationVector[2];
        }

        public static void RegisterPointCloud(Matrix<double> sourceClouds, Matrix<double> targetClouds,
            out Matrix<double> rotationMatrix, out Vector<double> translationVector)
        {
            // 计算中心点
            Vector<double> sourceCentroid = CalculateCentroid(sourceClouds);
            Vector<double> targetCentroid = CalculateCentroid(targetClouds);

            // 每个点云点与中心点做差
            Matrix<double> centeredSourceCloud = CenterPointCloud(sourceClouds, sourceCentroid);
            Matrix<double> centeredTargetCloud = CenterPointCloud(targetClouds, targetCentroid);

            // 计算协方差矩阵
            Matrix<double> crossCovariance = CalculateCrossCovariance(centeredSourceCloud, centeredTargetCloud);

            // SVD
            Svd<double> svd = crossCovariance.Svd();

            // 旋转矩阵
            rotationMatrix = svd.VT.Transpose() * svd.U.Transpose();

            // 平移矩阵
            translationVector = targetCentroid - rotationMatrix * sourceCentroid;

            // Print out results
            Console.WriteLine("Rotation Matrix:");
            Console.WriteLine(rotationMatrix);
            Console.WriteLine("Translation Vector:");
            Console.WriteLine(translationVector);
        }

        public static Matrix<double> GeneratePointCloudByPicPosition(List<PointCloud3D> pointClouds)
        {
            Matrix<double> pointCloud = Matrix<double>.Build.Dense(pointClouds.Count, 3);
            for (int i = 0; i < pointClouds.Count; i++)
            {
                pointCloud[i, 0] = pointClouds[i].Pic_X;
                pointCloud[i, 1] = pointClouds[i].Pic_Y;
                pointCloud[i, 2] = pointClouds[i].Pic_Z;
            }

            return pointCloud;
        }

        public static Matrix<double> GeneratePointCloudByWorldPosition(List<PointCloud3D> pointClouds)
        {
            Matrix<double> pointCloud = Matrix<double>.Build.Dense(pointClouds.Count, 3);
            for (int i = 0; i < pointClouds.Count; i++)
            {
                pointCloud[i, 0] = pointClouds[i].X;
                pointCloud[i, 1] = pointClouds[i].Y;
                pointCloud[i, 2] = pointClouds[i].Z;
            }

            return pointCloud;
        }

        static Vector<double> CalculateCentroid(Matrix<double> pointCloud)
        {
            int numPoints = pointCloud.RowCount;
            Vector<double> centroid = Vector<double>.Build.Dense(3);

            for (int i = 0; i < numPoints; i++)
            {
                centroid += pointCloud.Row(i);
            }

            centroid /= numPoints;

            return centroid;
        }

        static Matrix<double> CenterPointCloud(Matrix<double> pointCloud, Vector<double> centroid)
        {
            int numPoints = pointCloud.RowCount;
            Matrix<double> centeredPointCloud = Matrix<double>.Build.Dense(numPoints, 3);

            for (int i = 0; i < numPoints; i++)
            {
                centeredPointCloud.SetRow(i, pointCloud.Row(i) - centroid);
            }

            return centeredPointCloud;
        }

        static Matrix<double> CalculateCrossCovariance(Matrix<double> sourcePointCloud, Matrix<double> targetPointCloud)
        {
            int numPoints = sourcePointCloud.RowCount;
            Matrix<double> crossCovariance = Matrix<double>.Build.Dense(3, 3);

            for (int i = 0; i < numPoints; i++)
            {
                crossCovariance += sourcePointCloud.Row(i).ToColumnMatrix() * targetPointCloud.Row(i).ToRowMatrix();
            }

            return crossCovariance;
        }

        // 计算点云中某一点的法向量
        public static double[,] calNormalVector(List<PointCloud3D> nearPointCloud3Ds, PointCloud3D pointCloud)
        {
            double[,] n = new double[1, 3];

            // 生成用于运算的点云
            Matrix<double> nearPoints = GeneratePointCloudByPicPosition(nearPointCloud3Ds);

            // 计算中心点
            Vector<double> centroid = CalculateCentroid(nearPoints);

            // 每个点云点与中心点做差
            Matrix<double> centeredSourceCloud = CenterPointCloud(nearPoints, centroid);

            // 计算协方差矩阵
            Matrix<double> crossCovariance = CalculateCrossCovariance(centeredSourceCloud, centeredSourceCloud);

            // SVD
            Svd<Double> svd = crossCovariance.Svd();

            // 判断法线方向
            Vector<double> ns = svd.U.Column(2);
            Vector<double> nsc = Vector<double>.Build.Dense(3);
            nsc[0] = pointCloud.X - centroid[0];
            nsc[1] = pointCloud.Y - centroid[1];
            nsc[2] = pointCloud.Z - centroid[2];
            double angle = Math.Round(ns.DotProduct(nsc), 6);
            if (angle < 0) {
                ns = -ns;
            }
            n[0, 0] = Math.Round(ns[0], 6);
            n[0, 1] = Math.Round(ns[1], 6);
            n[0, 2] = Math.Round(ns[2], 6);
            return n;
        }

        public static void testRT() {
            List<PointCloud3D> spointCloud3Ds = new List<PointCloud3D>();
            List<PointCloud3D> tpointCloud3Ds = new List<PointCloud3D>();

            spointCloud3Ds.Add(new PointCloud3D(1, 1, 1));
            spointCloud3Ds.Add(new PointCloud3D(2, 2, 1));
            spointCloud3Ds.Add(new PointCloud3D(3, 3, 1));
            spointCloud3Ds.Add(new PointCloud3D(4, 3, 1));
            spointCloud3Ds.Add(new PointCloud3D(5, 3, 1));
            spointCloud3Ds.Add(new PointCloud3D(6, 3, 1));

            tpointCloud3Ds.Add(new PointCloud3D(1, 1, 1));
            tpointCloud3Ds.Add(new PointCloud3D(2, 2, 1));
            tpointCloud3Ds.Add(new PointCloud3D(3, 3, 1));
            tpointCloud3Ds.Add(new PointCloud3D(4, 3, 1));
            tpointCloud3Ds.Add(new PointCloud3D(5, 3, 1));
            tpointCloud3Ds.Add(new PointCloud3D(66, 3, 1));

            double[,] r;
            double[] t;

            RegisterPointCloud(spointCloud3Ds, tpointCloud3Ds, out r, out t);
            int a = 0;
        }

        public static void testN()
        {
            List<PointCloud3D> spointCloud3Ds = new List<PointCloud3D>();

            // 构造点云
            spointCloud3Ds.Add(new PointCloud3D(1, 1, 1));
            spointCloud3Ds.Add(new PointCloud3D(2, 2, 1));
            spointCloud3Ds.Add(new PointCloud3D(1, 2, 1));
            spointCloud3Ds.Add(new PointCloud3D(2, 1, 1));

            double[,] n1;
            double[,] n2;

            // 不同方向法线验证
            n1 = calNormalVector(spointCloud3Ds, new PointCloud3D(1.5, 1.5, 0));
            n2 = calNormalVector(spointCloud3Ds, new PointCloud3D(1.5, 1.5, 0));
            int a = 0;
        }
    }
}
