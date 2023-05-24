using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace sift.common
{
    public class QRCode
    {
        public static bool getPosition(Bitmap bitmap, out PointF leftDown, out PointF leftup, out PointF rightUp) {

            // 创建解码器,
            IBarcodeReader barcodeReader = new BarcodeReader();

            // 解码图片中的QR码
            Result result = barcodeReader.Decode(bitmap);
            leftDown = new PointF();
            leftup = new PointF();
            rightUp = new PointF();

            if (result != null)
            {
                // 输出结果 
                //  口(1)    口(2)
                //
                //       口(3)
                //  口(0) 
                Console.WriteLine("QR Code Content: " + result.Text);
                Console.WriteLine("QR Code Location: " + result.ResultPoints[0] + ", " + result.ResultPoints[1] + ", " + result.ResultPoints[2]);
                leftDown.X = result.ResultPoints[0].X;
                leftup.X = result.ResultPoints[1].X;
                rightUp.X = result.ResultPoints[2].X;
                leftDown.Y = result.ResultPoints[0].Y;
                leftup.Y = result.ResultPoints[1].Y;
                rightUp.Y = result.ResultPoints[2].Y;
                return true;
            }
            else
            {
                Console.WriteLine("No QR Code found.");
                return false;
            }
        }
    }
}
