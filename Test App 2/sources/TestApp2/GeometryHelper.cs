using g3;

namespace TestApp2
{
    public class GeometryHelper
    {
        private readonly int _pictureBoxHeight;

        public GeometryHelper()
        {
            
        }

        public GeometryHelper(int pictureBoxHeight)
        {
            _pictureBoxHeight = pictureBoxHeight;
        }

        public PointF[] OrthoLine(PointF point, PointF linePoint1, PointF linePoint2, double distance)
        {
            GetLineCircleIntersections(
                point, linePoint1.DistanceTo(linePoint2),
                linePoint1, linePoint2,
                out var lineCiricleIntersection1, out var lineCiricleIntersection2);

            //TODO:refactor
            var orthoLinePoints = GetCirclesIntersections(lineCiricleIntersection1, lineCiricleIntersection2, 1.1 * linePoint1.DistanceTo(linePoint2), 1.1 * linePoint1.DistanceTo(linePoint2));

            GetLineCircleIntersections(
                point, (float)distance,
                orthoLinePoints[0], orthoLinePoints[1],
                out var result1, out var result2);


            return new[]
            {
            result1, result2
        };
        }

        public PointF[][] GetParallelLinesPointsArray(PointF point1, PointF point2, double distance)
        {
            var orthoLinePoints = OrthoLine(point1, point1, point2, distance);
            return new[]
            {
            OrthoLine(orthoLinePoints[0], orthoLinePoints[0], point1, point1.DistanceTo(point2)),
            OrthoLine(orthoLinePoints[1], orthoLinePoints[1], point1, point1.DistanceTo(point2))
        };
        }
        
        public PointF GetMiddlePoint(PointF pointP1, PointF pointP2)
        {
            return new PointF((pointP1.X + pointP2.X) / 2, (pointP1.Y + pointP2.Y) / 2);
        }

        public Line2f GetLineByPoints(PointF point1, PointF point2)
        {
            //y=кх+в
            var k = (point2.Y - point1.Y) / (point2.X - point1.X);
            var b = -(point1.X * point2.Y - point2.X * point1.Y) / (point2.X - point1.X);

            return new Line2f
            {
                Direction = new Vector2f
                {
                    x = 1,
                    y = k
                },
                Origin = new Vector2f
                {
                    x = point1.X,
                    y = point1.Y + b
                }
            };
        }

        public int GetLineCircleIntersections(PointF center, float radius, PointF point1, PointF point2,
            out PointF intersection1, out PointF intersection2)
        {
            float cx, cy, dx, dy, A, B, C, det, t;
            
            cx = center.X;
            cy = center.Y;
            dx = point2.X - point1.X;
            dy = point2.Y - point1.Y;

            A = dx * dx + dy * dy;
            B = 2 * (dx * (point1.X - cx) + dy * (point1.Y - cy));
            C = (point1.X - cx) * (point1.X - cx) + (point1.Y - cy) * (point1.Y - cy) - radius * radius;

            det = B * B - 4 * A * C;
            if ((A <= 0.0000001) || (det < 0))
            {
                // No real solutions.
                intersection1 = new PointF(float.NaN, float.NaN);
                intersection2 = new PointF(float.NaN, float.NaN);
                return 0;
            }
            else if (det == 0)
            {
                // One solution.
                t = -B / (2 * A);
                intersection1 = new PointF(point1.X + t * dx, point1.Y + t * dy);
                intersection2 = new PointF(float.NaN, float.NaN);
                return 1;
            }
            else
            {
                // Two solutions.
                t = (float)((-B + Math.Sqrt(det)) / (2 * A));
                intersection1 = new PointF(point1.X + t * dx, point1.Y + t * dy);
                t = (float)((-B - Math.Sqrt(det)) / (2 * A));
                intersection2 = new PointF(point1.X + t * dx, point1.Y + t * dy);
                return 2;
            }
        }

        public PointF[] GetCirclesIntersections(PointF center1, PointF center2, double radius1, double? radius2 = null)
        {

            var (r1, r2) = (radius1, radius2 ?? radius1);
            (double x1, double y1, double x2, double y2) = (center1.X, center1.Y, center2.X, center2.Y);
            // d = distance from center1 to center2
            double d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            // Return an empty array if there are no intersections
            if (!(Math.Abs(r1 - r2) <= d && d <= r1 + r2)) { return new PointF[0]; }

            // Intersections i1 and possibly i2 exist
            var dsq = d * d;
            var (r1sq, r2sq) = (r1 * r1, r2 * r2);
            var r1sq_r2sq = r1sq - r2sq;
            var a = r1sq_r2sq / (2 * dsq);
            var c = Math.Sqrt(2 * (r1sq + r2sq) / dsq - (r1sq_r2sq * r1sq_r2sq) / (dsq * dsq) - 1);

            var fx = (x1 + x2) / 2 + a * (x2 - x1);
            var gx = c * (y2 - y1) / 2;

            var fy = (y1 + y2) / 2 + a * (y2 - y1);
            var gy = c * (x1 - x2) / 2;

            var i1 = new PointF((float)(fx + gx), (float)(fy + gy));
            var i2 = new PointF((float)(fx - gx), (float)(fy - gy));

            return i1 == i2 ? new PointF[] { i1 } : new PointF[] { i1, i2 };
        }

        public PointF FromCartesian(PointF point)
        {
            return new PointF(point.X, point.Y - _pictureBoxHeight);
        }

        public PointF ToCartesian(PointF point)
        {
            return new PointF(point.X, _pictureBoxHeight - point.Y);
        }
    }
}
