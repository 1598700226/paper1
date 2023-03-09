using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return (1 - rr) * (1 - cc) * image.Data[row, col, 0] +
                   (1 - rr) * cc * image.Data[row, col + 1, 0] +
                   rr * (1 - cc) * image.Data[row + 1, col, 0] +
                   rr * cc * image.Data[row + 1, col + 1, 0];
        }
    }
}
