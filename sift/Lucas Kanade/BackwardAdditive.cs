using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sift.common;

namespace sift.Lucas_Kanade
{
    public class BackwardAdditive
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
            Image<Gray, byte> imageRefTemp = imgRef.Copy(rectTemp);
            Image<Gray, double> imageRefTemp_gx = imgRefGradientx.Copy(rectTemp);
            Image<Gray, double> imageRefTemp_gy = imgRefGradienty.Copy(rectTemp);

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
            Matrix<double> Ihession = new Matrix<double>(6, 6);
            CvInvoke.Invert(hession, Ihession, 0);

            int i;
            for (i = 0; i < 50; ++i)
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

                Matrix<double> sigma = new Matrix<double>(6, 6);
                sigma[0, 0] = 1 + warp[0, 0];
                sigma[0, 1] = warp[2, 0];
                sigma[1, 0] = warp[1, 0];
                sigma[1, 1] = 1 + warp[3, 0];

                sigma[2, 2] = 1 + warp[0, 0];
                sigma[2, 3] = warp[2, 0];
                sigma[3, 2] = warp[1, 0];
                sigma[3, 3] = 1 + warp[3, 0];

                sigma[4, 4] = 1 + warp[0, 0];
                sigma[4, 5] = warp[2, 0];
                sigma[5, 4] = warp[1, 0];
                sigma[5, 5] = 1 + warp[3, 0];
                Matrix<double> delta_p = sigma * Ihession * residual;

                warp[0, 0] -= delta_p[0, 0];
                warp[1, 0] -= delta_p[1, 0];
                warp[2, 0] -= delta_p[2, 0];
                warp[3, 0] -= delta_p[3, 0];
                warp[4, 0] -= delta_p[4, 0];
                warp[5, 0] -= delta_p[5, 0];

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
            Console.WriteLine("IA迭代次数为:{0} result:{1}", i, string.Join(",", p));
            return p; 
        }
    }
}
