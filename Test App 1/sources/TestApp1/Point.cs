using System;
using System.Drawing;

namespace TestApp1
{
    public class Point
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public bool IsChosen { get; set; }

        public Guid ParentGuid { get; set; }

        public Point(double x, double y, bool isChosen = false)
        {
            X=x; 
            Y=y;
            IsChosen = isChosen;
        }

        //cast point into PointF
        public static implicit operator PointF(Point param)
        {
            return new PointF((float)param.X, (float)param.Y);
        }

        //public static implicit operator Point(PointF param)
        //{
        //    return new Point(param.X, param.Y);
        //}
    }
}