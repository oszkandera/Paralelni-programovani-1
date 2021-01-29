using System;

namespace DBSCAN.Model
{
    public class Point
    {
        public const int NOISE = -1;
        public const int UNCLASSIFIED = 0;
        public int X, Y, ClusterId;
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public override string ToString()
        {
            return String.Format("({0}, {1})", X, Y);
        }
        public static int DistanceSquared(Point p1, Point p2)
        {
            int diffX = p2.X - p1.X;
            int diffY = p2.Y - p1.Y;
            return diffX * diffX + diffY * diffY;
        }
    }
}
