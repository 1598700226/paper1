using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using OpenTK.Graphics.ES20;
using sift.PFH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace sift.PointCloudHandler
{
    public static class ICP
    {
        /**
         *   sPointCloud3Ds与tPointCloud3Ds是已经粗对齐过的点云
         */
        public static int iteration_point2point(List<PointCloud3D> sPointCloud3Ds, List<PointCloud3D> tPointCloud3Ds,
            out Matrix<double> rotation, out Vector<double> translation, double limitDistance = 20.0) {
            KdTree kdTree = new KdTree(tPointCloud3Ds);

            int iterations = 0;
            double tolerance = 0.0001;
            double error = double.MaxValue;

            rotation = Matrix<double>.Build.DenseIdentity(3);
            translation = Vector<double>.Build.Dense(3);
            List<PointCloud3D> spcs = new List<PointCloud3D>(sPointCloud3Ds);
            List<PointCloud3D> tpcs = new List<PointCloud3D>(tPointCloud3Ds);

            List<Matrix<double>> rotations = new List<Matrix<double>>();
            List<Vector<double>> translations = new List<Vector<double>>();

            while (iterations < 200) {
                // 计算旋转平移
                SvdRT.RegisterPointCloud(spcs, tpcs, out Matrix<double> tempR, out Vector<double> tempT);
                // 转换到新的位置
                spcs = transformListPointClouds(spcs, tempR, tempT);
                // 求新位置与目标点云的最近邻点
                tpcs = getNearestPointClouds(spcs, kdTree, out List<PointCloud3D> itemSpc, limitDistance);
                spcs = itemSpc;

                rotations.Add(tempR);
                translations.Add(tempT);
                // 计算错误的变化率,变化率过低提前结束
                double itemError = calError(spcs, tpcs);
                if (Math.Abs(error - itemError) < tolerance)
                {
                    break;
                }
                else {
                    error = itemError;
                }
                iterations++;
            }
            getRotationsAndTranslation(rotations, translations, out rotation, out translation);
            return iterations;
        }

        /*
         * p2p icp改进
         */
        public static int iteration_point2point(List<PointCloud3D> sPointCloud3Ds, List<PointCloud3D> tPointCloud3Ds, 
            Matrix<double> init_r, Vector<double> init_t, 
            out Matrix<double> rotation, out Vector<double> translation,
            double limitDistance = 20.0)
        {
            KdTree kdTree = new KdTree(tPointCloud3Ds);

            int iterations = 0;
            double tolerance = 0.0001;
            double error = double.MaxValue;

            rotation = Matrix<double>.Build.DenseIdentity(3);
            translation = Vector<double>.Build.Dense(3);
            List<PointCloud3D> spcs = new List<PointCloud3D>(sPointCloud3Ds);
            List<PointCloud3D> tpcs = new List<PointCloud3D>(tPointCloud3Ds);

            List<Matrix<double>> rotations = new List<Matrix<double>>();
            List<Vector<double>> translations = new List<Vector<double>>();

            Matrix<double> tempR = init_r;
            Vector<double> tempT = init_t;

            while (iterations < 200)
            {
                // 转换到新的位置
                spcs = transformListPointClouds(spcs, tempR, tempT);
                // 求新位置与目标点云的最近邻点， 找到对应点
                tpcs = getNearestPointClouds(spcs, kdTree, out List<PointCloud3D> itemSpc, limitDistance);
                spcs = itemSpc;
                // 计算错误的变化率,变化率过低提前结束
                double itemError = calError(spcs, tpcs);
                if (Math.Abs(error - itemError) < tolerance)
                {
                    break;
                }
                else
                {
                    error = itemError;
                }

                // 计算旋转平移
                rotations.Add(tempR);
                translations.Add(tempT);
                SvdRT.RegisterPointCloud(spcs, tpcs, out tempR, out tempT);   
                iterations++;
            }
            getRotationsAndTranslation(rotations, translations, out rotation, out translation);
            return iterations;
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
                Matrix<double> pointW = Matrix<double>.Build.Dense(1, 3);
                pointW[0, 0] = sourcePoints[i].X;
                pointW[0, 1] = sourcePoints[i].Y;
                pointW[0, 2] = sourcePoints[i].Z;
                point[0, 0] = sourcePoints[i].Pic_X;
                point[0, 1] = sourcePoints[i].Pic_Y;
                point[0, 2] = sourcePoints[i].Pic_Z;

                Matrix<double> newLoctionW = rotation * pointW.Row(0).ToColumnMatrix() + translation.ToColumnMatrix();
                Matrix<double> newLoction = rotation * point.Row(0).ToColumnMatrix() + translation.ToColumnMatrix();

                transPointCloud3Ds.Add(new PointCloud3D(newLoctionW[0, 0], newLoctionW[1, 0], newLoctionW[2, 0], 
                    newLoction[0, 0], newLoction[1, 0], newLoction[2, 0]));
            }
            return transPointCloud3Ds;
        }

        // 注意顺序，矩阵运算不满足交换律
        public static List<PointCloud3D> transformListPointClouds(List<PointCloud3D> sourcePoints, List<Matrix<double>> rotations, List<Vector<double>> translations)
        {
            // 变成齐次的方式
            List<Matrix<double>> transforms = new List<Matrix<double>>();
            for (int i = 0; i < rotations.Count; i++)
            {
                Matrix<double> rotation = rotations[i];
                Vector<double> translation = translations[i];
                Matrix<double> transformItem = Matrix<double>.Build.Dense(4, 4, 0);
                transformItem.SetSubMatrix(0, 0, rotation);
                transformItem.SetSubMatrix(0, 3, translation.ToColumnMatrix());
                transformItem[3, 3] = 1;
                transforms.Add(transformItem);
            }
            Matrix<double> transform = Matrix<double>.Build.DenseIdentity(4, 4);
            for (int i = 0; i < transforms.Count; i++)
            {
                transform = transform * transforms[i];
            } 

            List<PointCloud3D> transPointCloud3Ds = new List<PointCloud3D>();
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                Matrix<double> point = Matrix<double>.Build.Dense(4, 1);
                Matrix<double> pointWorld = Matrix<double>.Build.Dense(4, 1);
                point[0, 0] = sourcePoints[i].Pic_X;
                point[1, 0] = sourcePoints[i].Pic_Y;
                point[2, 0] = sourcePoints[i].Pic_Z;
                point[3, 0] = 1;
                pointWorld[0, 0] = sourcePoints[i].X;
                pointWorld[1, 0] = sourcePoints[i].Y;
                pointWorld[2, 0] = sourcePoints[i].Z;
                pointWorld[3, 0] = 1;
                Matrix<double> newLoction = Matrix<double>.Build.Dense(4, 1);
                point.CopyTo(newLoction);
                Matrix<double> newLoctionW = Matrix<double>.Build.Dense(4, 1);
                pointWorld.CopyTo(newLoctionW);
                newLoction = transform * newLoction;
                newLoctionW = transform * newLoctionW;
                transPointCloud3Ds.Add(new PointCloud3D(newLoctionW[0, 0], newLoctionW[1, 0], newLoctionW[2, 0], newLoction[0, 0], newLoction[1, 0], newLoction[2, 0]));
            }
            return transPointCloud3Ds;
        }

        private static double calError(Matrix<double> sourcePoints, Matrix<double> targetPoints) {
            Matrix<double> diff = sourcePoints - targetPoints;
            Matrix<double> squared = diff.PointwisePower(2);
            double error = squared.ColumnSums().Sum() / sourcePoints.RowCount;
            return Math.Sqrt(error);
        }

        private static double calError(List<PointCloud3D> sourcePoints, List<PointCloud3D> targetPoints)
        {
            Matrix<double> ms = SvdRT.GeneratePointCloudByWorldPosition(sourcePoints);
            Matrix<double> mt = SvdRT.GeneratePointCloudByWorldPosition(targetPoints);
            return calError(ms, mt);
        }

        // 输出对应点的点云，根据距离剔除误匹配点
        public static List<PointCloud3D> getNearestPointClouds(List<PointCloud3D> sPointCloud3Ds, KdTree targetKdTree, out List<PointCloud3D> spc, double limitDistance) {
            List<PointCloud3D> tpc = new List<PointCloud3D>();
            spc = new List<PointCloud3D>();
            for (int i = 0; i < sPointCloud3Ds.Count; i++)
            {
                PointCloud3D sp = sPointCloud3Ds[i];
                PointCloud3D tp = targetKdTree.KnumNearestNeighbors(sp, 1)[0];
                if (sp.Distance(tp) <= limitDistance) {
                    spc.Add(sp);
                    tpc.Add(tp);
                }
            }
            return tpc;
        }

        public static void getRotationsAndTranslation(List<Matrix<double>> rotations, List<Vector<double>> translations, 
            out Matrix<double> rotation, out Vector<double> translation) {
            // 变成齐次的方式
            List<Matrix<double>> transforms = new List<Matrix<double>>();
            for (int i = 0; i < rotations.Count; i++)
            {
                Matrix<double> item_r = rotations[i];
                Vector<double> item_t = translations[i];
                Matrix<double> transformItem = Matrix<double>.Build.Dense(4, 4, 0);
                transformItem.SetSubMatrix(0, 0, item_r);
                transformItem.SetSubMatrix(0, 3, item_t.ToColumnMatrix());
                transformItem[3, 3] = 1;
                transforms.Add(transformItem);
            }
            Matrix<double> transform = Matrix<double>.Build.DenseIdentity(4, 4);
            for (int i = 0; i < transforms.Count; i++)
            {
                transform = transform * transforms[i];
            }

            rotation = transform.SubMatrix(0, 3, 0, 3);
            translation = transform.SubMatrix(0, 3, 3, 1).Transpose().Row(0);
        }

        /****
         *  Icp Point to Plane
         * **/
        public static int iteration_point2plane(List<PointCloud3D> sPointCloud3Ds, List<PointCloud3D> tPointCloud3Ds,
            Matrix<double> init_r, Vector<double> init_t,
            out Matrix<double> rotation, out Vector<double> translation,
            double limitDistance = 20.0)
        {
            int normalRange = 15;
            KdTree kdTree = new KdTree(tPointCloud3Ds);
            PointFetures.getNormals(kdTree, tPointCloud3Ds, normalRange);
            kdTree = new KdTree(tPointCloud3Ds);

            int iterations = 0;
            double tolerance = 0.0001;
            double error = double.MaxValue;

            rotation = Matrix<double>.Build.DenseIdentity(3);
            translation = Vector<double>.Build.Dense(3);
            List<PointCloud3D> spcs = new List<PointCloud3D>(sPointCloud3Ds);
            List<PointCloud3D> tpcs = new List<PointCloud3D>(tPointCloud3Ds);

            List<Matrix<double>> rotations = new List<Matrix<double>>();
            List<Vector<double>> translations = new List<Vector<double>>();

            Matrix<double> tempR = init_r;
            Vector<double> tempT = init_t;

            while (iterations < 200)
            {
                // 转换到新的位置
                spcs = transformListPointClouds(spcs, tempR, tempT);
                // 求新位置与目标点云的最近邻点， 找到对应点, 并计算其法向量
                tpcs = getNearestPointCloudsAndNormal(spcs, kdTree, out List<PointCloud3D> itemSpc, limitDistance);
                spcs = itemSpc;
                double itemError = calErrorHasNormal(spcs, tpcs);
                if (Math.Abs(error - itemError) < tolerance)
                {
                    break;
                }
                else
                {
                    error = itemError;
                }

                rotations.Add(tempR);
                translations.Add(tempT);
                calculateRTAccordingToPoint2Plane(spcs, tpcs, out tempR, out tempT);
                iterations++;
            }
            getRotationsAndTranslation(rotations, translations, out rotation, out translation);
            return iterations;
        }

        // 输出对应点的点云和法向量，根据距离剔除误匹配点
        public static List<PointCloud3D> getNearestPointCloudsAndNormal(List<PointCloud3D> sPointCloud3Ds, KdTree targetKdTree, out List<PointCloud3D> spc, double limitDistance)
        {
            List<PointCloud3D> tpc = new List<PointCloud3D>();
            spc = new List<PointCloud3D>();
            for (int i = 0; i < sPointCloud3Ds.Count; i++)
            {
                PointCloud3D sp = sPointCloud3Ds[i];
                PointCloud3D tp = targetKdTree.KnumNearestNeighbors(sp, 1)[0];
                if (sp.Distance(tp) <= limitDistance && tp.hasNormal)
                {
                    spc.Add(sp);
                    tpc.Add(tp);
                }
            }
            return tpc;
        }

        // 计算旋转和平移矩阵，sPointCloud3Ds和tPointCloud3Ds必须是已经确定对应点并且tPointCloud3Ds需要计算出法向量
        public static void calculateRTAccordingToPoint2Plane(List<PointCloud3D> sPointCloud3Ds, List<PointCloud3D> tPointCloud3Ds, 
            out Matrix<double> rotation, out Vector<double> translation) {

            Matrix<double> A = Matrix<double>.Build.Dense(sPointCloud3Ds.Count, 6);
            Matrix<double> b = Matrix<double>.Build.Dense(sPointCloud3Ds.Count, 1);
            // 组成最小二乘法的矩阵 Ax = b;
            for (int i = 0; i < tPointCloud3Ds.Count; i++) { 
                PointCloud3D si = sPointCloud3Ds[i];
                PointCloud3D di = tPointCloud3Ds[i];
                double nix = di.n[0, 0];
                double niy = di.n[0, 1];
                double niz = di.n[0, 2];

                double ai1 = niz * si.Y - niy * si.Z;
                double ai2 = nix * si.Z - niz * si.X;
                double ai3 = niy * si.Y - nix * si.Y;

                Vector<double> Ai = Vector<double>.Build.DenseOfArray(new double[] { ai1, ai2, ai3, nix, niy, niz});
                A.SetRow(i, Ai);

                double bii = nix * di.X + niy * di.Y + niz * di.Z - nix * si.X - niy * si.Y - niz * si.Z;
                Vector<double> bi = Vector<double>.Build.DenseOfArray(new double[] { bii });
                b.SetRow(i, bi);
            }
             
            // 对A进行svd分解 Ax = b A = U * W * V_t
            Svd<double> svd = A.Svd();
            // 计算 U_t * b
            Matrix<double> y = svd.U.Transpose() * b;
            // 计算 W_-1 * U_t * b
            Matrix<double> w = svd.W; //需要看看是否是方阵
            for (int i = 0; i < w.RowCount; i++) {
                for (int j = 0; j < w.ColumnCount; j++) {
                    double data = w[i, j];
                    if (data.Equals(0))
                    {
                        continue;
                    }
                    else {
                        w[i, j] = 1.0 / data;
                    }
                }
            }
            Matrix<double> temp_b = Matrix<double>.Build.Dense(y.RowCount, 1);
            temp_b = w.Transpose() * y;
            // 计算x = V * W_-1 * U_t * b
            Matrix<double> tempx = svd.VT.Transpose() * temp_b;
            double angle_a = tempx[0, 0]; //x
            double angle_b = tempx[1, 0]; //y
            double angle_y = tempx[2, 0]; //z

            double r11 = Math.Cos(angle_y) * Math.Cos(angle_b);
            double r12 = -Math.Sin(angle_y) * Math.Cos(angle_a) + Math.Cos(angle_y) * Math.Sin(angle_b) * Math.Sin(angle_a);
            double r13 = Math.Sin(angle_y) * Math.Sin(angle_a) + Math.Cos(angle_y) * Math.Sin(angle_b) * Math.Cos(angle_a);
            double r21 = Math.Sin(angle_y) * Math.Cos(angle_b);
            double r22 = Math.Cos(angle_y) * Math.Cos(angle_a) + Math.Sin(angle_y) * Math.Sin(angle_b) * Math.Sin(angle_a);
            double r23 = -Math.Cos(angle_y) * Math.Sin(angle_a) + Math.Sin(angle_y) * Math.Sin(angle_b) * Math.Cos(angle_a);
            double r31 = -Math.Sin(angle_b);
            double r32 = Math.Cos(angle_b) * Math.Sin(angle_a);
            double r33 = Math.Cos(angle_b) * Math.Cos(angle_a);

            double[,] doubleRotation = new double[,] { { r11, r12, r13}, 
                { r21, r22, r23}, 
                { r31, r32, r33} 
            };
            rotation = Matrix<double>.Build.DenseOfArray(doubleRotation);
            double[] doubleTranslation = new double[] { tempx[3, 0], tempx[4, 0], tempx[5, 0] };
            translation = Vector<double>.Build.DenseOfArray(doubleTranslation);
        }

        // 计算错误值，point to plane 包含法线
        public static double calErrorHasNormal(List<PointCloud3D> sPointCloud3Ds, List<PointCloud3D> tPointCloud3Ds) {
            double error = 0;
            for (int i = 0; i < tPointCloud3Ds.Count; i++) { 
                PointCloud3D si = sPointCloud3Ds[i];
                PointCloud3D di = tPointCloud3Ds[i];
                if (di.hasNormal) {
                    double distance = (si.X - di.X) * di.n[0, 0] + (si.Y - di.Y) * di.n[0, 1] + (si.Z - di.Z) * di.n[0, 2];
                    error = Math.Pow(distance, 2);
                } else {
                    Console.WriteLine("【calErrorHasNormal】error：计算的点没有法向量！");
                }
            }
            return error;
        }
    }
}
