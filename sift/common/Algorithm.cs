using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Security.Cryptography;
using MathNet.Numerics.Integration;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace sift.common
{
    internal class Algorithm
    {
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

        /**
         * 双线性插值
         */
        public static double bilinearInterpolation(Image<Gray, Byte> image, double x, double y) {
            int row = (int)y;
            int col = (int)x;

            double rr = y - row;
            double cc = x - col;

            if (row + 1 >= image.Height && col + 1 >= image.Width) {
                return image.Data[row, col, 0];
            }
            if (row + 1 >= image.Height)
            {
                return (1 - cc) * image.Data[row, col, 0] +
                       cc * image.Data[row, col + 1, 0];
            }
            if (col + 1 >= image.Width) {
                return (1 - rr) * image.Data[row, col, 0] +
                       rr * image.Data[row + 1, col, 0];
            }

            return (1 - rr) * (1 - cc) * image.Data[row, col, 0] +
                   (1 - rr) * cc * image.Data[row, col + 1, 0] +
                   rr * (1 - cc) * image.Data[row + 1, col, 0] +
                   rr * cc * image.Data[row + 1, col + 1, 0];
        }

        public static double bilinearInterpolation(Image<Gray, float> image, double x, double y)
        {
            int row = (int)y;
            int col = (int)x;

            double rr = y - row;
            double cc = x - col;

            if (row + 1 >= image.Height && col + 1 >= image.Width)
            {
                return image.Data[row, col, 0];
            }
            if (row + 1 >= image.Height)
            {
                return (1 - cc) * image.Data[row, col, 0] +
                       cc * image.Data[row, col + 1, 0];
            }
            if (col + 1 >= image.Width)
            {
                return (1 - rr) * image.Data[row, col, 0] +
                       rr * image.Data[row + 1, col, 0];
            }

            return (1 - rr) * (1 - cc) * image.Data[row, col, 0] +
                   (1 - rr) * cc * image.Data[row, col + 1, 0] +
                   rr * (1 - cc) * image.Data[row + 1, col, 0] +
                   rr * cc * image.Data[row + 1, col + 1, 0];
        }

        public static double bilinearInterpolation(Image<Gray, double> image, double x, double y)
        {
            int row = (int)y;
            int col = (int)x;

            double rr = y - row;
            double cc = x - col;

            if (row + 1 >= image.Height && col + 1 >= image.Width)
            {
                return image.Data[row, col, 0];
            }
            if (row + 1 >= image.Height)
            {
                return (1 - cc) * image.Data[row, col, 0] +
                       cc * image.Data[row, col + 1, 0];
            }
            if (col + 1 >= image.Width)
            {
                return (1 - rr) * image.Data[row, col, 0] +
                       rr * image.Data[row + 1, col, 0];
            }

            return (1 - rr) * (1 - cc) * image.Data[row, col, 0] +
                   (1 - rr) * cc * image.Data[row, col + 1, 0] +
                   rr * (1 - cc) * image.Data[row + 1, col, 0] +
                   rr * cc * image.Data[row + 1, col + 1, 0];
        }

        public static double bilinearInterpolation(byte[] data, int width, int height, double x, double y)
        {
            int row = (int)y;
            int col = (int)x;

            double rr = y - row;
            double cc = x - col;

            if (row + 1 >= height && col + 1 >= width)
            {
                return data[row * width + col];
            }
            if (row + 1 >= height)
            {
                return (1 - cc) * data[row * width + col] +
                        cc * data[row * width + col + 1];
            }
            if (col + 1 >= width)
            {
                return (1 - rr) * data[row * width + col] +
                       rr * data[(row + 1) * width + col];
            }

            return (1 - rr) * (1 - cc) * data[row * width + col] +
                   (1 - rr) * cc * data[row * width + col + 1] +
                   rr * (1 - cc) * data[(row + 1)* width + col] +
                   rr * cc * data[(row + 1) * width + col + 1];
        }

        public static double bilinearInterpolation(ushort[] data, int width, int height, double x, double y)
        {
            int row = (int)y;
            int col = (int)x;

            double rr = y - row;
            double cc = x - col;

            if (row + 1 >= height && col + 1 >= width)
            {
                return data[row * width + col];
            }
            if (row + 1 >= height)
            {
                return (1 - cc) * data[row * width + col] +
                        cc * data[row * width + col + 1];
            }
            if (col + 1 >= width)
            {
                return (1 - rr) * data[row * width + col] +
                       rr * data[(row + 1) * width + col];
            }

            return (1 - rr) * (1 - cc) * data[row * width + col] +
                   (1 - rr) * cc * data[row * width + col + 1] +
                   rr * (1 - cc) * data[(row + 1) * width + col] +
                   rr * cc * data[(row + 1) * width + col + 1];
        }

        public static bool IsInPolygonF(PointF checkPoint, List<PointF> polygonPoints)
        {
            bool inside = false;
            int pointCount = polygonPoints.Count;
            PointF p1, p2;
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i, i++)//第一个点和最后一个点作为第一条线，之后是第一个点和第二个点作为第二条线，之后是第二个点与第三个点，第三个点与第四个点...  
            {
                p1 = polygonPoints[i];
                p2 = polygonPoints[j];
                if (checkPoint.Y < p2.Y)
                {//p2在射线之上  
                    if (p1.Y <= checkPoint.Y)
                    {//p1正好在射线中或者射线下方  
                        if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) > (checkPoint.X - p1.X) * (p2.Y - p1.Y))//斜率判断,在P1和P2之间且在P1P2右侧  
                        {
                            //射线与多边形交点为奇数时则在多边形之内，若为偶数个交点时则在多边形之外。  
                            //由于inside初始值为false，即交点数为零。所以当有第一个交点时，则必为奇数，则在内部，此时为inside=(!inside)  
                            //所以当有第二个交点时，则必为偶数，则在外部，此时为inside=(!inside)  
                            inside = (!inside);
                        }
                    }
                }
                else if (checkPoint.Y < p1.Y)
                {
                    //p2正好在射线中或者在射线下方，p1在射线上  
                    if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) < (checkPoint.X - p1.X) * (p2.Y - p1.Y))//斜率判断,在P1和P2之间且在P1P2右侧  
                    {
                        inside = (!inside);
                    }
                }
            }
            return inside;
        }

        public static bool IsInPolygon(Point checkPoint, List<Point> polygonPoints)
        {
            bool inside = false;
            int pointCount = polygonPoints.Count;
            Point p1, p2;
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i, i++)//第一个点和最后一个点作为第一条线，之后是第一个点和第二个点作为第二条线，之后是第二个点与第三个点，第三个点与第四个点...  
            {
                p1 = polygonPoints[i];
                p2 = polygonPoints[j];
                if (checkPoint.Y < p2.Y)
                {//p2在射线之上  
                    if (p1.Y <= checkPoint.Y)
                    {//p1正好在射线中或者射线下方  
                        if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) > (checkPoint.X - p1.X) * (p2.Y - p1.Y))//斜率判断,在P1和P2之间且在P1P2右侧  
                        {
                            //射线与多边形交点为奇数时则在多边形之内，若为偶数个交点时则在多边形之外。  
                            //由于inside初始值为false，即交点数为零。所以当有第一个交点时，则必为奇数，则在内部，此时为inside=(!inside)  
                            //所以当有第二个交点时，则必为偶数，则在外部，此时为inside=(!inside)  
                            inside = (!inside);
                        }
                    }
                }
                else if (checkPoint.Y < p1.Y)
                {
                    //p2正好在射线中或者在射线下方，p1在射线上  
                    if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) < (checkPoint.X - p1.X) * (p2.Y - p1.Y))//斜率判断,在P1和P2之间且在P1P2右侧  
                    {
                        inside = (!inside);
                    }
                }
            }
            return inside;
        }

        // 转弧度
        public static double ToRadians(double degrees)
        {
            return degrees * (double)Math.PI / 180.0f;
        }
        // 转角度
        public static double ToDegrees(double radians)
        {
            return radians * 180.0f / (double)Math.PI;
        }

        // 欧拉角转为旋转矩阵
        public static MathNet.Numerics.LinearAlgebra.Matrix<double> EulerToMatrix(MathNet.Numerics.LinearAlgebra.Vector<double> Euler_xyz)
        {
            MathNet.Numerics.LinearAlgebra.Matrix<double> Rmatrix = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.Dense(3, 3);
            MathNet.Numerics.LinearAlgebra.Matrix<double> Rz = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(
                new double[,] { 
                    { Math.Cos(Euler_xyz[2]), -Math.Sin(Euler_xyz[2]), 0 }, 
                    { Math.Sin(Euler_xyz[2]), Math.Cos(Euler_xyz[2]), 0 },
                    { 0, 0, 1} 
                });
            MathNet.Numerics.LinearAlgebra.Matrix<double> Ry = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(
                new double[,] { 
                    { Math.Cos(Euler_xyz[1]), 0 , Math.Sin(Euler_xyz[1])},
                    { 0, 1, 0},
                    { -Math.Sin(Euler_xyz[1]), 0 , Math.Cos(Euler_xyz[1])} 
                });
            MathNet.Numerics.LinearAlgebra.Matrix<double> Rx = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(
                new double[,] {
                    { 1, 0, 0},
                    { 0 , Math.Cos(Euler_xyz[0]),- Math.Sin(Euler_xyz[0])},
                    { 0 , Math.Sin(Euler_xyz[0]), Math.Cos(Euler_xyz[0])}
                });

            Rmatrix = Rz * Ry * Rz;
            return Rmatrix;
        }
        // 旋转矩阵转为欧拉角,弧度
        public static MathNet.Numerics.LinearAlgebra.Vector<double> MatrixToEuler(MathNet.Numerics.LinearAlgebra.Matrix<double> R)
        {
            double x,y,z;
            double sy = Math.Sqrt(R[0, 0] * R[0, 0] + R[1, 0] * R[1, 0]);
            bool singular = sy < 1e-6;
            if (!singular) {
                x = Math.Atan2(R[2, 1], R[2, 2]);
                y = Math.Atan2(-R[2, 0], sy);
                z = Math.Atan2(R[1, 0], R[0, 0]);

            } else {
                x = Math.Atan2(-R[1, 2], R[1, 1]);
                y = Math.Atan2(-R[2, 0], sy);
                z = 0;
            }
            return MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(new double[3] { x, y, z });
        }


    }
}
