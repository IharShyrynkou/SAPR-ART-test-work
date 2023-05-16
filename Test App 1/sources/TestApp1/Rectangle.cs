using System.Drawing;

namespace TestApp1
{
    public class Rectangle
    {
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }
        public Point BotLeft { get; set; }
        public Point BotRight { get; set; }
        public double Width => Shape.Width;
        public double Height => Shape.Height;

        public Point[] Vertices => new[] { TopLeft, TopRight, BotRight, BotLeft };

        private RectangleF Shape { get; set; }

        public Rectangle(Point topLeft, Point botRight)
        {
            Shape = new RectangleF((float)topLeft.X, (float)topLeft.Y, (float)(botRight.X - topLeft.X), (float)(botRight.Y - topLeft.Y));
            CalculatePoints();
        }

        public Rectangle(float x, float y, float width, float height)
        {
            Shape = new RectangleF(x, y, width, height);
            CalculatePoints();
        }

        public Rectangle(Point topLeft, double width, double height)
        {
            Shape = new RectangleF((float)topLeft.X, (float)topLeft.Y, (float)width, (float)height);
            CalculatePoints();
        }

        private void CalculatePoints()
        {
            TopLeft = new Point(Shape.Left, Shape.Top);
            TopRight = new Point(Shape.Right, Shape.Top);
            BotRight = new Point(Shape.Right, Shape.Bottom);
            BotLeft = new Point(Shape.Left, Shape.Bottom);
        }


        public static implicit operator RectangleF(Rectangle param)
        {
            return new  RectangleF(
                (float)param.TopLeft.X, (float)param.TopLeft.Y, 
                (float)param.Width, (float)param.Height);
        }

        public static implicit operator Rectangle(RectangleF param)
        {
            return new Rectangle(param.X, param.Y, param.Width, param.Height);
        }

    }
}