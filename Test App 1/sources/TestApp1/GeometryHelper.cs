using System;
using System.Linq;
using System.Collections.Generic;

namespace TestApp1
{
    public class GeometryHelper
    {
        public static Point FindNearestPoint(List<Point> points, Point clickPoint, int accuracy)
        {
            if (points.Count == 0) throw new ArgumentException("Sequence contains no elements");
            if (clickPoint == null) throw new ArgumentNullException($"Null or empty {nameof(clickPoint)}");

            return (from p in points
                    where Math.Abs(p.X - clickPoint.X) < accuracy &&
                          Math.Abs(p.Y - clickPoint.Y) < accuracy
                    select p).FirstOrDefault();
        }

        public static Point[] FindBoundaryPoints(List<Point> points)
        {
            if (points.Count == 0) throw new ArgumentException("Sequence contains no elements");

            float top, bot, left, right;
            Point topLeftPoint, botRightPoint;

            top = (float)points.Max(entry => entry.Y);
            bot = (float)points.Min(entry => entry.Y);
            right = (float)points.Max(entry => entry.X);
            left = (float)points.Min(entry => entry.X);

            topLeftPoint = new Point(left, top);
            botRightPoint = new Point(right, bot);

            return new[]
            {
                topLeftPoint,
                botRightPoint
            };
        }
    }
}
