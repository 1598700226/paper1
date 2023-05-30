using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sift.common
{
    internal class SparkUtils
    {
        int width = 500;
        int height = 500;
        int sparkNum = 15;      // 散斑数量
        int diameter = 15;       // 散斑直径（单位：像素）
        float density=0.6f;     // 密排度（区间：0-1，图像上的散斑密度）
        float variation=0.5f;   // 偏移度（区间：0-1，图像上的散斑随机排布程度，0时即为圆点整列）
        int background=1;       // 图像背景颜色（0：黑色，1：白色）

        public PictureBox pictureBox;

        public SparkUtils(PictureBox pictureBox) {
            this.pictureBox = pictureBox;
        }

        public void createSimulateSparks() {

            // 散斑个数
            int spacing = (int)(diameter / density);
            int rows = height / spacing;
            int cols = width / spacing;
            int radius = diameter / 2;

            Random rd = new Random();
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
/*            Brush bush = new SolidBrush(Color.Black);*/
    

            for (int xn = 0; xn < rows; xn++) {
                for (int yn = 0; yn < cols; yn++) {
                    int midx = xn * spacing + rd.Next(radius, spacing - radius);
                    int midy = yn * spacing + rd.Next(radius, spacing - radius);
                    Pen pen = new Pen(Color.FromArgb(255, rd.Next(0,30), rd.Next(0, 30), rd.Next(0, 30)), 5);
                    Brush bush = new SolidBrush(Color.FromArgb(255, rd.Next(0, 30), rd.Next(0, 30), rd.Next(0, 30)));
                    g.DrawEllipse(pen, midx - radius, midy - radius, diameter, diameter);
                }
            }
            pictureBox.Image = bitmap;
        }
    }
}
