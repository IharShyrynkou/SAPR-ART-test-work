using g3;

namespace TestApp2
{
    public static class GraphicsExtension
    {
        public static GeometryHelper geometryHelper { get; set; }

        public static void DisplayFiledRectangle(this Graphics _graphics, PointF pointP1, float h1, PointF pointP2, Color color)
        {
            var rr1 = geometryHelper.OrthoLine(pointP1, pointP1, pointP2, h1 / 2);
            var rr2 = geometryHelper.OrthoLine(pointP2, pointP1, pointP2, h1 / 2);

            _graphics.FillPolygon(new SolidBrush(color), new[]
            {
                geometryHelper.ToCartesian(new PointF(rr1[0].X, rr1[0].Y)),
                geometryHelper.ToCartesian(new PointF(rr1[1].X, rr1[1].Y)),
                geometryHelper.ToCartesian(new PointF(rr2[1].X, rr2[1].Y)),
                geometryHelper.ToCartesian(new PointF(rr2[0].X, rr2[0].Y))
            });
        }

        public static void DisplayPoint(this Graphics _graphics, Pen pen, PointF point)
        {
            DisplayCircle(_graphics, pen, point, pen.Color, pen.Width/2);
        }

        public static void DisplayCircle(this Graphics _graphics, Pen pen, PointF point, Color color, float radius = Single.NaN)
        {
            var width = new SizeF(pen.Width, pen.Width);

            if (float.IsNaN(radius))
            {
                radius = pen.Width;
            }
            else
            {
                width = new SizeF(radius, radius);
            }

            _graphics.DrawEllipse(new Pen(color, pen.Width+5), new RectangleF(geometryHelper.ToCartesian(new PointF(point.X - radius, point.Y + radius)), 
                    2 * width));
        }

        public static void DisplayLine(this Graphics _graphics, Pen pen, PointF point1, PointF point2)
        {
            _graphics.DrawLine(pen, geometryHelper.ToCartesian(point1), geometryHelper.ToCartesian(point2));
        }

        public static void DisplayDashLine(this Graphics _graphics, Pen pen, PointF point1, PointF point2)
        {
            var dashPen = new Pen(pen.Color, pen.Width);
            dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            _graphics.DrawLine(dashPen, geometryHelper.ToCartesian(point1), geometryHelper.ToCartesian(point2));
        }

        public static float DistanceTo(this PointF current, PointF other)
        {
            return (float)(Math.Sqrt(Math.Pow(current.X - other.X, 2) + Math.Pow(current.Y - other.Y, 2)));
        }
    }
}
