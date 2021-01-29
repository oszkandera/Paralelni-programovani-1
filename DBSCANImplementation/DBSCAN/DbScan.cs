using DBSCAN.Model;
using System.Collections.Generic;
using System.Linq;

namespace DBSCAN
{
    class DbScan
    {
        public List<List<Point>> GetClusters(List<Point> points, double eps, int minPts)
        {
            if (points == null)
            {
                return null;
            }

            List<List<Point>> clusters = new List<List<Point>>();
            
            eps *= eps;
            
            int clusterId = 1;
            
            for (int i = 0; i < points.Count; i++)
            {
                Point p = points[i];
                if (p.ClusterId == Point.UNCLASSIFIED)
                {
                    if (ExpandCluster(points, p, clusterId, eps, minPts)) clusterId++;
                }
            }
            
            int maxClusterId = points.OrderBy(p => p.ClusterId).Last().ClusterId;
            
            if (maxClusterId < 1)
            {
                return clusters;
            }
            for (int i = 0; i < maxClusterId; i++)
            {
                clusters.Add(new List<Point>());
            }

            foreach (Point p in points)
            {
                if (p.ClusterId > 0) clusters[p.ClusterId - 1].Add(p);
            }

            return clusters;
        }
        private List<Point> GetRegion(List<Point> points, Point p, double eps)
        {
            List<Point> region = new List<Point>();
            for (int i = 0; i < points.Count; i++)
            {
                int distSquared = Point.DistanceSquared(p, points[i]);
                if (distSquared <= eps) region.Add(points[i]);
            }
            return region;
        }
        private bool ExpandCluster(List<Point> points, Point p, int clusterId, double eps, int minPts)
        {
            List<Point> seeds = GetRegion(points, p, eps);
            if (seeds.Count < minPts)
            {
                p.ClusterId = Point.NOISE;
                return false;
            }
            else
            {
                for (int i = 0; i < seeds.Count; i++)
                {
                    seeds[i].ClusterId = clusterId;
                }
                
                seeds.Remove(p);
                
                while (seeds.Count > 0)
                {
                    Point currentP = seeds[0];
                    List<Point> result = GetRegion(points, currentP, eps);
                
                    if (result.Count >= minPts)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            Point resultP = result[i];
                            if (resultP.ClusterId == Point.UNCLASSIFIED || resultP.ClusterId == Point.NOISE)
                            {
                                if (resultP.ClusterId == Point.UNCLASSIFIED) seeds.Add(resultP);
                                resultP.ClusterId = clusterId;
                            }
                        }
                    }
                    seeds.Remove(currentP);
                }
                return true;
            }
        }
    }
}
