using System;
using GAF;
using Math = System.Math;

namespace SnakeSharp
{
    public static class PointExtensions
    {
        public static double CalculateDistanceFrom(this Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }


    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point() : this(-1, -1)
        {
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
