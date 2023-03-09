using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using sift.common;
using System.Drawing.Drawing2D;

namespace sift.Lucas_Kanade
{
    internal class ForwardAdditive
    {

        public static double[] method(Image<Gray, Byte> imgRef, Image<Gray, Byte> imgDef, double[] p, int tempWidth, int tempHeight) {

            Matrix<double> wrap = new Matrix<double>(6, 1);
            wrap[0, 0] = p[0];
            wrap[1, 0] = p[1];
            wrap[2, 0] = p[2];
            wrap[3, 0] = p[3];
            wrap[4, 0] = p[4];
            wrap[5, 0] = p[5];

            Image<Gray, double> imgRefGradientx = new Image<Gray, double>(imgRef.Width, imgRef.Height);
            Image<Gray, double> imgRefGradienty = new Image<Gray, double>(imgRef.Width, imgRef.Height);
            CvInvoke.Sobel(imgRef, imgRefGradientx, Emgu.CV.CvEnum.DepthType.Cv64F, 1, 0, 1);
            CvInvoke.Sobel(imgRef, imgRefGradienty, Emgu.CV.CvEnum.DepthType.Cv64F, 0, 1, 1);

            Image<Gray, double> imgDefGradientx = new Image<Gray, double>(imgDef.Width, imgDef.Height);
            Image<Gray, double> imgDefGradienty = new Image<Gray, double>(imgDef.Width, imgDef.Height);
            CvInvoke.Sobel(imgDef, imgDefGradientx, Emgu.CV.CvEnum.DepthType.Cv64F, 1, 0, 1);
            CvInvoke.Sobel(imgDef, imgDefGradienty, Emgu.CV.CvEnum.DepthType.Cv64F, 0, 1, 1);

            for(int i = 0; i < 1000; i++) {
                double cost = 0;

                Matrix<double> residual = new Matrix<double>(6, 1);
                Matrix<double> hession = new Matrix<double>(6, 6);
                for (int x = 0; x < tempWidth; x++)
                {
                    for (int y = 0; y < tempHeight; y++)
                    {
                        double wx = (1 + wrap[0, 0]) * (double)x + wrap[2, 0] * (double)y + wrap[4, 0];
                        double wy = wrap[1, 0] * (double)x + (1 + wrap[3, 0]) * (double)y + wrap[5, 0];

                        double iw = Algorithm.bilinearInterpolation(imgDef, wx, wy);
                        double err = iw - imgRef.Data[y, x, 0];

                        // 梯度x和y
                        double gx_warped = Algorithm.bilinearInterpolation(imgDefGradientx, wx, wy);
                        double gy_warped = Algorithm.bilinearInterpolation(imgDefGradienty, wx, wy);

                        Matrix<double> jacobian = new Matrix<double>(1, 6);
                        jacobian[0, 0] = x * gx_warped;
                        jacobian[0, 1] = x * gy_warped;
                        jacobian[0, 2] = y * gy_warped;
                        jacobian[0, 3] = y * gx_warped;
                        jacobian[0, 4] = gx_warped;
                        jacobian[0, 5] = gy_warped;

                        hession += jacobian.Transpose() * jacobian;
                        residual -= jacobian.Transpose() * err;

                        cost += err * err;
                    }
                }

                Matrix<double> Ihession = new Matrix<double>(6, 6);
                CvInvoke.Invert(hession, Ihession, 0);
                Matrix<double> delta_p = Ihession * residual;
                wrap += delta_p;

                if (delta_p[1, 0] * delta_p[1, 0] +
                    delta_p[2, 0] * delta_p[2, 0] +
                    delta_p[3, 0] * delta_p[3, 0] +
                    delta_p[4, 0] * delta_p[4, 0] +
                    delta_p[5, 0] * delta_p[5, 0] +
                    delta_p[0, 0] * delta_p[0, 0] < 0.0001) {
                    break;
                }
            }

            return null;
        }
    }
}
