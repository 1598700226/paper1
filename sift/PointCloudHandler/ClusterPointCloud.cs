using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sift.PointCloudHandler
{
    public class ClusterPointCloud
    {
        // 半径和邻域个数
        public static List<List<PointCloud3D>> DBSCAN(List<PointCloud3D> sourcePointClouds, double eps, double minNum) {
            foreach (PointCloud3D point in sourcePointClouds) {
                point.isVisited = false;
                point.clusterId = 0;
            }
            List<List<PointCloud3D>> clusters = new List<List<PointCloud3D>>();
            // 构建kd树，并初始化分类集合
            KdTree kdTree = new KdTree(sourcePointClouds, "");

            int clusterId = 1;
            for (int i = 0; i < sourcePointClouds.Count; i++)
            {
                PointCloud3D item = sourcePointClouds[i];
                if (item.isVisited) {
                    continue;
                }
                item.isVisited = true;
                List<PointCloud3D> nearPts = kdTree.RNearestNeighbors(item, eps);
                if (nearPts.Count < minNum)
                {
                    item.clusterId = 0;
                }
                else 
                {
                    item.clusterId = clusterId;
                    List<PointCloud3D> newCluster = new List<PointCloud3D>
                    {
                        item
                    };
                    expendCluster(kdTree, nearPts, newCluster, eps, minNum, ref clusterId);

                    //将新的簇加入到聚类结果中
                    clusters.Add(newCluster);
                }
            }

            return clusters;
        }

        public static void expendCluster(KdTree kdTree, List<PointCloud3D> pNeighbor,
            List<PointCloud3D> cluster, double eps, double minNum, ref int clusterId) {

            for (int i = 0; i < pNeighbor.Count; i++) { 
                PointCloud3D item = pNeighbor[i];
                if (item.isVisited) {
                    continue;
                }
                item.isVisited = true;

                List<PointCloud3D> nearPts = kdTree.RNearestNeighbors(item, eps);
                if (nearPts.Count >= minNum)
                {
                    foreach (PointCloud3D nearPt in nearPts) {
                        if (!pNeighbor.Contains(nearPt)) { 
                            pNeighbor.Add(nearPt);
                        }
                    }
                }

                //如果该点没有归属簇，则将其加入到该簇中
                if (item.clusterId == 0)
                {
                    item.clusterId = clusterId;
                    cluster.Add(item);
                }
            }

            clusterId++;
        }
    }
}
