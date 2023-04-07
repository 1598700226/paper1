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


    /**
     * 前加性算法， 
     */
    public class ForwardAdditive
    {
        /**
         
         wrap
        [ 1+P1 P3 P5 ][X]
        [ P2 1+P4 P6 ][Y]
                      [1]

        jacobian = 梯度I * wrap/A 
        [Ix Iy] * [x 0 y 0 1 0] 
                  [0 x 0 y 0 1]

        */
        public static double[] method(Image<Gray, Byte> imgRef, Image<Gray, Byte> imgDef, double[] p, int tempWidth, int tempHeight, int tx, int ty) {

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

            for(int i = 0; i < 50; i++) {
                double cost = 0;

                Matrix<double> residual = new Matrix<double>(6, 1);
                Matrix<double> hession = new Matrix<double>(6, 6);
                for (int x = 0; x < tempWidth; x++)
                {
                    for (int y = 0; y < tempHeight; y++)
                    {
                        double wx = (1 + warp[0, 0]) * (double)x + warp[2, 0] * (double)y + warp[4, 0];
                        double wy = warp[1, 0] * (double)x + (1 + warp[3, 0]) * (double)y + warp[5, 0];

                        double iw = Algorithm.bilinearInterpolation(imgDef, wx, wy);
                        double err = iw - imageRefTemp.Data[y, x, 0];

                        // 梯度x和y
                        double gx_warped = Algorithm.bilinearInterpolation(imgDefGradientx, wx, wy);
                        double gy_warped = Algorithm.bilinearInterpolation(imgDefGradienty, wx, wy);

                        // 计算雅可比
                        Matrix<double> jacobian = new Matrix<double>(1, 6);
                        jacobian[0, 0] = x * gx_warped;
                        jacobian[0, 1] = x * gy_warped;
                        jacobian[0, 2] = y * gy_warped;
                        jacobian[0, 3] = y * gx_warped;
                        jacobian[0, 4] = gx_warped;
                        jacobian[0, 5] = gy_warped;

                        // 计算黑森矩阵
                        hession += jacobian.Transpose() * jacobian;
                        residual -= jacobian.Transpose() * err;

                        cost += err * err;
                    }
                }

                Matrix<double> Ihession = new Matrix<double>(6, 6);
                CvInvoke.Invert(hession, Ihession, 0);
                Matrix<double> delta_p = Ihession * residual;
                warp += delta_p;

                if (delta_p[1, 0] * delta_p[1, 0] +
                    delta_p[2, 0] * delta_p[2, 0] +
                    delta_p[3, 0] * delta_p[3, 0] +
                    delta_p[4, 0] * delta_p[4, 0] +
                    delta_p[5, 0] * delta_p[5, 0] +
                    delta_p[0, 0] * delta_p[0, 0] < 0.0001) {
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
