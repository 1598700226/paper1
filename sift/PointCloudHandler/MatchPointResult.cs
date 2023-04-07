using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sift.PointCloudHandler
{
    public class MatchPointResult
    {
        public double X;
        public double Y;

        public double match_X;
        public double match_Y;

        // 相关性
        public double R;

        public MatchPointResult(double X, double Y, double match_X, double match_Y, double R) {
            this.X = X;
            this.Y = Y;
            this.match_X = match_X;
            this.match_Y = match_Y;
            this.R = R;
        }
    }
}
