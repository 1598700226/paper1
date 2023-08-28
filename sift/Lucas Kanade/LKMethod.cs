using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sift.Lucas_Kanade
{
    public enum LKMethodName
    {
        FAGN, // 前向加性高斯牛顿 ，LK本体
        FCGN, // 前向组合
        ICGN, // 反向组合
        IAGN  // 反向加性
    }

    public class LKMethod
    {

        // return  ux uy  vx vy  u v;
        // ux vy 代表x\y方向的拉伸 uy vx 表示x\y方向的剪切 u v代表位移
        /**
 
         wrap
        [ 1+P1 P3 P5 ][X]
        [ P2 1+P4 P6 ][Y]
                      [1]

        jacobian = 梯度I * wrap/A 
        [Ix Iy] * [x 0 y 0 1 0] 
                  [0 x 0 y 0 1]

        */
        public static double[] invoke(String picPath1, String picPath2, int tempSize,
            int tx, int ty, double offsetx, double offsety, LKMethodName lKMethodName) {
 
            {
                Image<Gray, byte> imageRef = new Image<Gray, byte>(picPath1);
                Image<Gray, byte> imageDef = new Image<Gray, byte>(picPath2);
                // ux uy u vx vy v
                // todo 减去边界的宽度 不然就在左上角，不是中心点可能不准
                double[] p = new double[] { 0.01, 0.01, 0.0, 0.0, tx + offsetx + 0.5, ty + offsety};
                
                switch(lKMethodName) {
                    case LKMethodName.ICGN:
                        return BackwardCompositional.method(imageRef, imageDef, p, tempSize, tempSize, tx, ty);
                    case LKMethodName.IAGN:
                        return BackwardAdditive.method(imageRef, imageDef, p, tempSize, tempSize, tx, ty);
                    case LKMethodName.FAGN:
                        return ForwardAdditive.method(imageRef, imageDef, p, tempSize, tempSize, tx, ty);
                    case LKMethodName.FCGN:
                        return ForwardCompositional.method(imageRef, imageDef, p, tempSize, tempSize, tx, ty);
                    default:
                        Console.WriteLine("传入的请求参数 LKMethodName 不正确");
                        return null;
                }
            }
        }

        public static double[] invoke(Image<Gray, byte> imageRef, Image<Gray, byte> imageDef, int tempSize,
            int tx, int ty, LKMethodName lKMethodName, double[] p0)
        {
            // ux uy u vx vy v
            // todo 减去边界的宽度 不然就在左上角，不是中心点可能不准
            double[] p = new double[6];
            if (p0 == null) {
                p = new double[] { 0, 0, 0, 0, tx, ty};
            }
            else {
                p0.CopyTo(p, 0);
            }


            switch (lKMethodName)
            {
                case LKMethodName.ICGN:
                    return BackwardCompositional.method(imageRef, imageDef, p, tempSize, tempSize, tx, ty);
                case LKMethodName.IAGN:
                    return BackwardAdditive.method(imageRef, imageDef, p, tempSize, tempSize, tx, ty);
                case LKMethodName.FAGN:
                    return ForwardAdditive.method(imageRef, imageDef, p, tempSize, tempSize, tx, ty);
                case LKMethodName.FCGN:
                    return ForwardCompositional.method(imageRef, imageDef, p, tempSize, tempSize, tx, ty);
                default:
                    Console.WriteLine("传入的请求参数 LKMethodName 不正确");
                    return null;
            }
        }
    }
}
