using AnyCAD.Forms;
using AnyCAD.Foundation;
using sift.PointCloudHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sift
{
    public partial class _3DShow : Form
    {
        RenderControl mRenderView;
        Float32Buffer mPositions = new Float32Buffer(0);
        Float32Buffer mColors = new Float32Buffer(0);

        public _3DShow(List<PointCloud3D> pointClouds, List<Color> colors)
        {
            InitializeComponent();
            mRenderView = new RenderControl(this.panel);

            foreach (PointCloud3D item in pointClouds)
            {
                mPositions.Append((float)item.X, (float)item.Y, (float)item.Z);
            }

            if (colors == null)
            {
                foreach (PointCloud3D item in pointClouds)
                {
                    mColors.Append(255, 0, 0);
                }
            }
            else {
                foreach (Color item in colors)
                {
                    mColors.Append((float)item.R, (float)item.G, (float)item.B);
                }
            }

        }

        public _3DShow(List<PointCloud3D> pointClouds1, List<Color> colors1,
            List<PointCloud3D> pointClouds2, List<Color> colors2)
        {
            InitializeComponent();
            mRenderView = new RenderControl(this.panel);

            foreach (PointCloud3D item in pointClouds1)
            {
                mPositions.Append((float)item.X, (float)item.Y, (float)item.Z);
            }

            if (colors1 == null)
            {
                foreach (PointCloud3D item in pointClouds1)
                {
                    mColors.Append(255, 0, 0);
                }
            }
            else
            {
                foreach (Color item in colors1)
                {
                    mColors.Append((float)item.R, (float)item.G, (float)item.B);
                }
            }

            foreach (PointCloud3D item in pointClouds2)
            {
                mPositions.Append((float)item.X, (float)item.Y, (float)item.Z);
            }

            if (colors2 == null)
            {
                foreach (PointCloud3D item in pointClouds2)
                {
                    mColors.Append(0, 0, 255);
                }
            }
            else
            {
                foreach (Color item in colors2)
                {
                    mColors.Append((float)item.R, (float)item.G, (float)item.B);
                }
            }

        }

        private void _3DShow_Load(object sender, EventArgs e)
        {
            
            PointCloud node = PointCloud.Create(mPositions, mColors, null, 1);
            mRenderView.ShowSceneNode(node);
            mRenderView.ViewContext.GetSceneManager().SelectSubShape(node.GetUuid(), EnumShapeFilter.Vertex, 0);
        }
    }
}
