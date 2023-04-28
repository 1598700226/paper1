using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sift.PointCloudHandler
{
    public static class PLY
    {
        public static void writePlyFile_xyzn(String filename, List<PointCloud3D> pointCloud3Ds) {

            int num = 0;
            foreach (PointCloud3D point in pointCloud3Ds)
            {
                if (point.hasNormal)
                {
                    num++;
                }
            }

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("ply");
                writer.WriteLine("format ascii 1.0");
                writer.WriteLine("element vertex " + num);
                writer.WriteLine("property float x");
                writer.WriteLine("property float y");
                writer.WriteLine("property float z");
                writer.WriteLine("property float nx");
                writer.WriteLine("property float ny");
                writer.WriteLine("property float nz");
                writer.WriteLine("element face 0");
                writer.WriteLine("property list uchar int vertex_indices");
                writer.WriteLine("end_header");

                foreach (PointCloud3D point in pointCloud3Ds)
                {
                    if (point.hasNormal)
                    {
                        writer.WriteLine(point.X + " " + point.Y + " " + point.Z + " " + point.n[0, 0] + " " + point.n[0, 1] + " " + point.n[0, 2]);
                    }
                }
            }
        }

        public static void writePlyFile_xyz(String filename, List<PointCloud3D> pointCloud3Ds)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("ply");
                writer.WriteLine("format ascii 1.0");
                writer.WriteLine("element vertex " + pointCloud3Ds.Count);
                writer.WriteLine("property float x");
                writer.WriteLine("property float y");
                writer.WriteLine("property float z");
                writer.WriteLine("element face 0");
                writer.WriteLine("property list uchar int vertex_indices");
                writer.WriteLine("end_header");

                foreach (PointCloud3D point in pointCloud3Ds)
                {
                    writer.WriteLine(point.X + " " + point.Y + " " + point.Z);
                }
            }
        }

        public static void writePlyFile_xyzrgb(String filename, List<PointCloud3D> pointCloud3Ds)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("ply");
                writer.WriteLine("format ascii 1.0");
                writer.WriteLine("element vertex " + pointCloud3Ds.Count);
                writer.WriteLine("property float x");
                writer.WriteLine("property float y");
                writer.WriteLine("property float z");
                writer.WriteLine("property uchar red");
                writer.WriteLine("property uchar green");
                writer.WriteLine("property uchar blue");
                writer.WriteLine("element face 0");
                writer.WriteLine("property list uchar int vertex_indices");
                writer.WriteLine("end_header");

                foreach (PointCloud3D point in pointCloud3Ds)
                {
                    writer.WriteLine(point.X + " " + point.Y + " " + point.Z + " " + 
                        point.color.R + " " + point.color.G + " " + point.color.B);
                }
            }
        }

        public static void test()
        {
            List<PointCloud3D> points = new List<PointCloud3D>();
            points.Add(new PointCloud3D(0, 0, 0));
            points.Add(new PointCloud3D(1, 0, 0));
            points.Add(new PointCloud3D(0, 1, 0));
            points.Add(new PointCloud3D(0, 0, 1));
            points.Add(new PointCloud3D(1, 1, 1));

            writePlyFile_xyzn("testPly.ply", points);
        }

        public static void writeOFFFile_xyzrgb(String filename, List<PointCloud3D> pointCloud3Ds)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("off");
                writer.WriteLine(pointCloud3Ds.Count + " 0 0");

                foreach (PointCloud3D point in pointCloud3Ds)
                {
                    writer.WriteLine(point.X + " " + point.Y + " " + point.Z + " " +
                        point.color.R + " " + point.color.G + " " + point.color.B);
                }
            }
        }

    }
}
