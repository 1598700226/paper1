using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sift.PointCloudHandler
{
    public class SPFH
    {
        public int[] alpha;
        public int[] fai;
        public int[] theta;

        // 区间数量
        private int length;

        public SPFH(int[] alpha, int[] fai, int[] theta, int range) { 
            this.alpha = alpha;
            this.fai = fai;
            this.theta = theta;

            this.length = range;
        }

        // 特征直方图 增加特征 range - 1 表示区间索引
        public void AddFeatureStatistic(ref int[] features, double radian) {
            double item = Math.Round(radian, 6);
            if (item < 0) {
                item += 2 * Math.PI;
            }
            int index = (int)((item / (2 * Math.PI)) * (length - 1));
            features[index]++;
        }

        public int[] ToDoubleArray() {
            int[] doubles = new int[length * 3];
            for(int i = 0; i < length; i++)
            {
                doubles[i] = alpha[i];
                doubles[length + i] = fai[i];
                doubles[2 * length + i] = theta[i];
            }
            return doubles;
        }
    }
}
