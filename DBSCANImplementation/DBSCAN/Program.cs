using DBSCAN.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DBSCAN
{
    class Program
    {
        static void Main(string[] args)
        {
            var points = GeneratePoints(15000);

            double eps = 100.0;
            int minPts = 3;

            var dbScan = new DbScan();
            var dbScanParallel = new DbScanParallel();

            var stopwatch1 = new Stopwatch();
            var stopwatch2 = new Stopwatch();

            stopwatch1.Start();
            List<List<Point>> clusters = dbScan.GetClusters(points, eps, minPts);
            stopwatch1.Stop();

            stopwatch2.Start();
            List<List<Point>> clustersParallel = dbScanParallel.GetClusters(points, eps, minPts);
            stopwatch2.Stop();

            Console.WriteLine($"DBSCAN elapsed time (sync): {stopwatch1.ElapsedMilliseconds}");
            Console.WriteLine($"DBSCAN elapsed time (parallel): {stopwatch2.ElapsedMilliseconds}");
        }

        private static List<Point> GeneratePoints(int size)
        {
            List<Point> points = new List<Point>();

            var randomGenerator = new Random();

            for (int i = 0; i < size; i++)
            {
                var x = randomGenerator.Next(0, 50000);
                var y = randomGenerator.Next(0, 50000);
                points.Add(new Point(x, y));
            }

            return points;
        }
    }
}
