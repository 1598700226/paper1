using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sift.PointCloudHandler
{
    public static class ICP
    {
        /**
         *   sPointCloud3Ds与tPointCloud3Ds是已经对齐过的点云
         */
        public static void iteration(List<PointCloud3D> sPointCloud3Ds, List<PointCloud3D> tPointCloud3Ds, out Matrix<double> rotation, out Vector<double> translation) {
            int iterations = 50;
            double tolerance = 0.0001;
            double error = double.MaxValue;

            rotation = Matrix<double>.Build.DenseIdentity(3);
            translation = Vector<double>.Build.Dense(3);
            Matrix<double> spcs = SvdRT.GeneratePointCloudByPicPosition(sPointCloud3Ds);
            Matrix<double> tpcs = SvdRT.GeneratePointCloudByPicPosition(tPointCloud3Ds);

            while (iterations > 0 && error > tolerance) {
                Matrix<double> tempR;
                Vector<double> tempT;
                SvdRT.RegisterPointCloud(spcs, tpcs, out tempR, out tempT);

                spcs = transformMatrixPointClouds(spcs, tempR, tempT);
                rotation = rotation * tempR;
                translation = translation + tempT;

                error = calError(spcs, tpcs);
                iterations--;
            }

            int a = 0;
        }

        public static Matrix<double> transformMatrixPointClouds(Matrix<double> sourcePoints, Matrix<double> rotation, Vector<double> translation) {
            for (int i = 0; i < sourcePoints.RowCount; i++) {
                Matrix<double> newLoction = rotation * sourcePoints.Row(i).ToColumnMatrix() + translation.ToColumnMatrix();
                sourcePoints.SetRow(i, newLoction.Transpose().Row(0));
            }
            return sourcePoints;
        }

        public static List<PointCloud3D> transformListPointClouds(List<PointCloud3D> sourcePoints, Matrix<double> rotation, Vector<double> translation)
        {
            List<PointCloud3D> transPointCloud3Ds = new List<PointCloud3D>();
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                Matrix<double> point = Matrix<double>.Build.Dense(1, 3);
                point[0, 0] = sourcePoints[i].Pic_X;
                point[0, 1] = sourcePoints[i].Pic_Y;
                point[0, 2] = sourcePoints[i].Pic_Z;

                Matrix<double> newLoction = rotation * point.Row(0).ToColumnMatrix() + translation.ToColumnMatrix();
                transPointCloud3Ds.Add(new PointCloud3D(0, 0, 0, newLoction[0, 0], newLoction[1, 0], newLoction[2, 0]));
            }
            return transPointCloud3Ds;
        }

        private static double calError(Matrix<double> sourcePoints, Matrix<double> targetPoints) {
            Matrix<double> diff = sourcePoints - targetPoints;
            Matrix<double> squared = diff.PointwisePower(2);
            double error = squared.ColumnSums().Sum() / sourcePoints.RowCount;
            return Math.Sqrt(error);
        }
    }
}
