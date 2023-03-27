using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace sift.PointCloud
{
    public class KdTree
    {
        private class KdNode
        {
            public PointCloud3D Point { get; set; }
            public KdNode Left { get; set; }
            public KdNode Right { get; set; }
            public KdNode(PointCloud3D point)
            {
                Point = point;
                Left = null;
                Right = null;
            }
        }
    
        private KdNode root;

        public KdTree(List<PointCloud3D> points)
        {
            root = BuildTree(points, 0);
        }

        private KdNode BuildTree(List<PointCloud3D> points, int depth)
        {
            if (points.Count == 0) return null;

            int axis = depth % 3;

            points.Sort((a, b) => axis == 0 ? a.X.CompareTo(b.X) : axis == 1 ? a.Y.CompareTo(b.Y) : a.Z.CompareTo(b.Z));

            int medianIndex = points.Count / 2;

            KdNode node = new KdNode(points[medianIndex]);
            node.Left = BuildTree(points.GetRange(0, medianIndex), depth + 1);
            node.Right = BuildTree(points.GetRange(medianIndex + 1, points.Count - medianIndex - 1), depth + 1);

            return node;
        }

        /// <summary>
        /// 最近的k个点云
        /// </summary>
        public List<PointCloud3D> KnumNearestNeighbors(PointCloud3D point, int k)
        {
            List<PointCloud3D> neighbors = new List<PointCloud3D>();
            KnumNearestNeighbors(root, point, k, 0, neighbors);
            return neighbors;
        }

        static int it_num = 0;
        private void KnumNearestNeighbors(KdNode node, PointCloud3D point, int k, int depth, List<PointCloud3D> neighbors)
        {
            //it_num ++;
            if (node == null)
            {
                return;
            }
            double distance = node.Point.Distance(point);

            // isAdd目的是减少递归
            bool isAdd = false;
            if (neighbors.Count < k || distance < neighbors[neighbors.Count - 1].Distance(point))
            {
                isAdd = true;
                neighbors.Add(node.Point);
                neighbors.Sort((a, b) => a.Distance(point).CompareTo(b.Distance(point)));
                if (neighbors.Count > k) 
                    neighbors.RemoveAt(neighbors.Count - 1);
            }

            if (isAdd)
            {
                KnumNearestNeighbors(node.Left, point, k, depth + 1, neighbors);
                KnumNearestNeighbors(node.Right, point, k, depth + 1, neighbors);
            }
            else
            {
                int axis = depth % 3;
                double r = neighbors[neighbors.Count - 1].Distance(point);
                double tempDistance = Math.Abs(axis == 0 ? point.X - node.Point.X : axis == 1 ? point.Y - node.Point.Y : point.Z - node.Point.Z);
                if (tempDistance <= r)
                {
                    KnumNearestNeighbors(node.Left, point, k, depth + 1, neighbors);
                    KnumNearestNeighbors(node.Right, point, k, depth + 1, neighbors);
                }
                else
                {
                    if (axis == 0 ? point.X < node.Point.X : axis == 1 ? point.Y < node.Point.Y : point.Z < node.Point.Z)
                    {
                        KnumNearestNeighbors(node.Left, point, k, depth + 1, neighbors);
                    }
                    else
                    {
                        KnumNearestNeighbors(node.Right, point, k, depth + 1, neighbors);
                    }
                }
            }
        }

        public List<PointCloud3D> RNearestNeighbors(PointCloud3D point, double r)
        {
            List<PointCloud3D> neighbors = new List<PointCloud3D>();
            RNearestNeighbors(root, point, r, 0, neighbors);
            return neighbors;
        }

        /// <summary>
        /// 距离R范围内邻点
        /// </summary>
        private void RNearestNeighbors(KdNode node, PointCloud3D point, double r, int depth, List<PointCloud3D> neighbors)
        {
            it_num ++;
            if (node == null)
            {
                return;
            }

            double distance = node.Point.Distance(point);

            // isAdd目的是减少递归
            bool isAdd = false;
            if (distance <= r)
            {
                isAdd = true;
                neighbors.Add(node.Point);
            }

            if (isAdd)
            {
                RNearestNeighbors(node.Left, point, r, depth + 1, neighbors);
                RNearestNeighbors(node.Right, point, r, depth + 1, neighbors);
            }
            else {
                int axis = depth % 3;
                double tempDistance = Math.Abs(axis == 0 ? point.X - node.Point.X : axis == 1 ? point.Y - node.Point.Y : point.Z - node.Point.Z);
                if (tempDistance <= r)
                {
                    RNearestNeighbors(node.Left, point, r, depth + 1, neighbors);
                    RNearestNeighbors(node.Right, point, r, depth + 1, neighbors);
                }
                else {
                    if (axis == 0 ? point.X < node.Point.X : axis == 1 ? point.Y < node.Point.Y : point.Z < node.Point.Z)
                    {
                        RNearestNeighbors(node.Left, point, r, depth + 1, neighbors);
                    }
                    else
                    {
                        RNearestNeighbors(node.Right, point, r, depth + 1, neighbors);
                    }
                }
            }
        }

        /// <summary>
        /// 用于测试，验证算法正确性
        /// </summary>
        public static void test() {

            // 构造点云
            List<PointCloud3D> points = new List<PointCloud3D>();
            for (int i = -5; i < 5; i++) {
                for (int j = -5; j < 5; j++)
                {
                    points.Add(new PointCloud3D(i, j, 0.01));
                    //for (int z = -5; z < 5; z++)
                        
                }
                   
            }

            // 构造kd
            KdTree kdTree = new KdTree(points);

            // 定义查询点
            PointCloud3D queryPoint = new PointCloud3D(1.01, 0.01, 0.01);

            // 定义半径和近邻数量
            int k = 9;
            double r = 1;
            List<PointCloud3D> kneighbors = kdTree.KnumNearestNeighbors(queryPoint, k);
            List<PointCloud3D> rneighbors = kdTree.RNearestNeighbors(queryPoint, r);

            Console.WriteLine("Nearest neighbors of point ({0}, {1}, {2}):", queryPoint.X, queryPoint.Y, queryPoint.Z);

            Console.WriteLine("----范围r:{0} 查询----", r);
            foreach (PointCloud3D neighbor in rneighbors)
            {
                Console.WriteLine("({0}, {1}, {2})", neighbor.X, neighbor.Y, neighbor.Z);
            }
            Console.WriteLine("----近邻k:{0} 个查询----", k);
            foreach (PointCloud3D neighbor in kneighbors)
            {
                Console.WriteLine("({0}, {1}, {2})", neighbor.X, neighbor.Y, neighbor.Z);
            }

            Console.ReadLine();
        }
    }
}
