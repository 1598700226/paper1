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
    /**
     * 和前向加性的区别是 先计算变形图像再求梯度 还是先求梯度再插值成变形图像
     * 
     */
    public class ForwardCompositional
    {
        public static double[] method(Image<Gray, Byte> imgRef, Image<Gray, Byte> imgDef, double[] p, int tempWidth, int tempHeight, int tx, int ty)
        {
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

            Rectangle rectTemp = new Rectangle(tx, ty, tempWidth, tempHeight);
            Image<Gray, byte> imageRefTemp = new Image<Gray, byte>(rectTemp.Size);
            imageRefTemp = imgRef.Copy(rectTemp);

            int i = 0;
            for (i = 0; i < 50; i++)
            {
                Image<Gray, double> warp_I = new Image<Gray, double>(tempWidth, tempHeight);
                Image<Gray, double> warp_I_gx = new Image<Gray, double>(tempWidth, tempHeight);
                Image<Gray, double> warp_I_gy = new Image<Gray, double>(tempWidth, tempHeight);
                for (int x = 0; x < tempWidth; x++)
                {
                    for (int y = 0; y < tempHeight; y++)
                    {
                        double wx = (1 + warp[0, 0]) * (double)x + warp[2, 0] * (double)y + warp[4, 0];
                        double wy = warp[1, 0] * (double)x + (1 + warp[3, 0]) * (double)y + warp[5, 0];
                        warp_I.Data[y, x, 0] = Algorithm.bilinearInterpolation(imgDef, wx, wy);
                    }
                }
                CvInvoke.Sobel(warp_I, warp_I_gx, Emgu.CV.CvEnum.DepthType.Cv64F, 1, 0, 1);
                CvInvoke.Sobel(warp_I, warp_I_gy, Emgu.CV.CvEnum.DepthType.Cv64F, 0, 1, 1);

                Matrix<double> residual = new Matrix<double>(6, 1);
                Matrix<double> hession = new Matrix<double>(6, 6);
                for (int y = 0; y < tempHeight; y++)
                {
                    for (int x = 0; x < tempWidth; x++)
                    {
                        double err = warp_I.Data[y, x, 0] - imageRefTemp.Data[y, x, 0];

                        Matrix<double> jacobian = new Matrix<double>(1, 6);
                        double gx_warped = warp_I_gx.Data[y, x, 0];
                        double gy_warped = warp_I_gy.Data[y, x, 0];
                        jacobian[0, 0] = x * gx_warped;
                        jacobian[0, 1] = x * gy_warped;
                        jacobian[0, 2] = y * gx_warped;
                        jacobian[0, 3] = y * gy_warped;
                        jacobian[0, 4] = gx_warped;
                        jacobian[0, 5] = gy_warped;

                        hession += jacobian.Transpose() * jacobian;
                        residual -= jacobian.Transpose() * err;
                    }
                }
                Matrix<double> Ihession = new Matrix<double>(6, 6);
                CvInvoke.Invert(hession, Ihession, 0);
                Matrix<double> delta_p = Ihession * residual;

                // w = w * 🔺w
                warp[0, 0] += (warp[0, 0] * delta_p[0, 0] + warp[2, 0] * delta_p[1, 0]);
                warp[1, 0] += (warp[1, 0] * delta_p[0, 0] + warp[3, 0] * delta_p[1, 0]);
                warp[2, 0] += (warp[0, 0] * delta_p[2, 0] + warp[2, 0] * delta_p[3, 0]);
                warp[3, 0] += (warp[1, 0] * delta_p[2, 0] + warp[3, 0] * delta_p[3, 0]);
                warp[4, 0] += (warp[0, 0] * delta_p[4, 0] + warp[2, 0] * delta_p[5, 0]);
                warp[5, 0] += (warp[1, 0] * delta_p[4, 0] + warp[3, 0] * delta_p[5, 0]);

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
            Console.WriteLine("FC迭代次数为:{0} result:{1}", i, string.Join(",", p));
            return p;
        }

    }
}
