using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Foundation;
using sift.common;

namespace sift.Lucas_Kanade
{
    public class BackwardCompositional
    {


        public static double[] method(Image<Gray, Byte> imgRef, Image<Gray, Byte> imgDef, double[] p, int tempWidth, int tempHeight, int tx, int ty)
        {
            // ux uy  vx vy  u v
            Matrix<double> warp = new Matrix<double>(6, 1);
            warp[0, 0] = p[0];
            warp[1, 0] = p[1];
            warp[2, 0] = p[2];
            warp[3, 0] = p[3];
            warp[4, 0] = p[4];
            warp[5, 0] = p[5];

            Image<Gray, double> imgRefGradientx = new Image<Gray, double>(imgRef.Width, imgRef.Height);
            Image<Gray, double> imgRefGradienty = new Image<Gray, double>(imgRef.Width, imgRef.Height);
            CvInvoke.Sobel(imgRef, imgRefGradientx, Emgu.CV.CvEnum.DepthType.Cv64F, 1, 0, 1);
            CvInvoke.Sobel(imgRef, imgRefGradienty, Emgu.CV.CvEnum.DepthType.Cv64F, 0, 1, 1);

            Image<Gray, double> imgDefGradientx = new Image<Gray, double>(imgDef.Width, imgDef.Height);
            Image<Gray, double> imgDefGradienty = new Image<Gray, double>(imgDef.Width, imgDef.Height);
            CvInvoke.Sobel(imgDef, imgDefGradientx, Emgu.CV.CvEnum.DepthType.Cv64F, 1, 0, 1);
            CvInvoke.Sobel(imgDef, imgDefGradienty, Emgu.CV.CvEnum.DepthType.Cv64F, 0, 1, 1);

            // 获取参考模板和x\y梯度
            Rectangle rectTemp = new Rectangle(tx, ty, tempWidth, tempHeight);
            Image<Gray, byte> imageRefTemp = new Image<Gray, byte>(rectTemp.Size);
            imageRefTemp = imgRef.Copy(rectTemp);
            Image<Gray, double> imageRefTemp_gx = new Image<Gray, double>(rectTemp.Size);
            imageRefTemp_gx = imgRefGradientx.Copy(rectTemp);
            Image<Gray, double> imageRefTemp_gy = new Image<Gray, double>(rectTemp.Size);
            imageRefTemp_gy = imgRefGradienty.Copy(rectTemp);

            // 先计算出黑森矩阵H
            Matrix<double> hession = new Matrix<double>(6, 6);
            Matrix<double> xgx = new Matrix<double>(tempWidth, tempHeight);
            Matrix<double> xgy = new Matrix<double>(tempWidth, tempHeight);
            Matrix<double> ygx = new Matrix<double>(tempWidth, tempHeight);
            Matrix<double> ygy = new Matrix<double>(tempWidth, tempHeight);
            for (int y = 0; y < tempHeight; y++)
            {
                for (int x = 0; x < tempWidth; x++)
                {
                    xgx.Data[y, x] = x * imageRefTemp_gx.Data[y, x, 0];
                    xgy.Data[y, x] = x * imageRefTemp_gy.Data[y, x, 0];
                    ygx.Data[y, x] = y * imageRefTemp_gx.Data[y, x, 0];
                    ygy.Data[y, x] = y * imageRefTemp_gy.Data[y, x, 0];

                    Matrix<double> jacobian = new Matrix<double>(1, 6);
                    jacobian.Data[0, 0] = xgx.Data[y, x];
                    jacobian.Data[0, 1] = xgy.Data[y, x];
                    jacobian.Data[0, 2] = ygx.Data[y, x];
                    jacobian.Data[0, 3] = ygy.Data[y, x];
                    jacobian.Data[0, 4] = imageRefTemp_gx.Data[y, x, 0];
                    jacobian.Data[0, 5] = imageRefTemp_gy.Data[y, x, 0];

                    hession += jacobian.Transpose() * jacobian;
                }
            }
            // 计算H的逆
            Matrix<double> Ihession = new Matrix<double>(6, 6);
            CvInvoke.Invert(hession, Ihession, 0);

            for (int i = 0; i < 50; ++i)
            {
                Matrix<double> residual = new Matrix<double>(6, 1);
                double cost = 0.0;

                for (int y = 0; y < tempHeight; y++)
                {
                    for (int x = 0; x < tempWidth; x++)
                    {

                        double wx = (double)x * (1 + warp[0, 0]) + (double)y * warp[2, 0] + warp[4, 0];
                        double wy = (double)x * warp[1, 0] + (double)y * (1 + warp[3, 0]) + warp[5, 0];
                        //todo wx wy 越界判断

                        double iw = Algorithm.bilinearInterpolation(imgDef, wx, wy);
                        double err = imageRefTemp.Data[y, x, 0] - iw;

                        Matrix<double> jacobian = new Matrix<double>(1, 6);
                        jacobian.Data[0, 0] = xgx.Data[y, x];
                        jacobian.Data[0, 1] = xgy.Data[y, x];
                        jacobian.Data[0, 2] = ygx.Data[y, x];
                        jacobian.Data[0, 3] = ygy.Data[y, x];
                        jacobian.Data[0, 4] = imageRefTemp_gx.Data[y, x, 0];
                        jacobian.Data[0, 5] = imageRefTemp_gy.Data[y, x, 0];

                        residual -= jacobian.Transpose() * err;
                        cost += err * err;
                    }
                }

                Matrix<double> delta_p = Ihession * residual;
                Matrix<double> delta_m, warp_m;
                delta_m = warp_m = new Matrix<double>(3, 3);
                delta_m[0, 0] = 1 + delta_p[0, 0];
                delta_m[0, 1] = delta_p[2, 0];
                delta_m[0, 2] = delta_p[4, 0];
                delta_m[1, 0] = delta_p[1, 0];
                delta_m[1, 1] = 1 + delta_p[3, 0];
                delta_m[1, 2] = delta_p[5, 0];
                delta_m[2, 0] = 0;
                delta_m[2, 1] = 0;
                delta_m[2, 2] = 1;
                Matrix<double> Idelta_m = new Matrix<double>(3, 3);
                CvInvoke.Invert(delta_m, Idelta_m, 0);

                warp_m[0, 0] = 1 + warp[0, 0];
                warp_m[0, 1] = warp[2, 0];
                warp_m[0, 2] = warp[4, 0];
                warp_m[1, 0] = warp[1, 0];
                warp_m[1, 1] = 1 + warp[3, 0];
                warp_m[1, 2] = warp[5, 0];
                warp_m[2, 0] = 0;
                warp_m[2, 1] = 0;
                warp_m[2, 2] = 1;

                Matrix<double> newWarp = warp_m * Idelta_m;
                warp[0, 0] = newWarp[0, 0] - 1;
                warp[1, 0] = newWarp[1, 0];
                warp[2, 0] = newWarp[0, 1];
                warp[3, 0] = newWarp[1, 1] - 1;
                warp[4, 0] = newWarp[0, 2];
                warp[5, 0] = newWarp[1, 2];

                if (delta_p[1, 0] * delta_p[1, 0] +
                    delta_p[2, 0] * delta_p[2, 0] +
                    delta_p[3, 0] * delta_p[3, 0] +
                    delta_p[4, 0] * delta_p[4, 0] +
                    delta_p[5, 0] * delta_p[5, 0] +
                    delta_p[0, 0] * delta_p[0, 0] < 0.0001)
                {
                    break;
                }
            }


            p[0] = warp[0, 0];
            p[1] = warp[1, 0];
            p[2] = warp[2, 0];
            p[3] = warp[3, 0];
            p[4] = warp[4, 0];
            p[5] = warp[5, 0];
            return p;
        }
    }
}
