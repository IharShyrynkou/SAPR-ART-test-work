using System;
using System.Drawing;
using TestApp1.Display;
using System.Collections.Generic;


namespace TestApp1
{
    public static class GraphicsExtension
    {
        public static void DrawRectangles(this Graphics instance, List<RectangleToDisplay> rectanglesList,  PenToDisplayCollection penCollection)
        {
            if (rectanglesList == null) throw new ArgumentNullException();
            if (penCollection == null) throw new ArgumentNullException();
            if (rectanglesList.Count == 0) throw new ArgumentException();


            foreach (var rectangleToDisplay in rectanglesList)
            {
                instance.DrawRectangle(rectangleToDisplay, penCollection);
            }
        }

        public static void DrawPoint(this Graphics instance, Point point, Pen selectPenPoints)
        {
            if (point == null) throw new ArgumentNullException();
            if (selectPenPoints == null) throw new ArgumentNullException();

            var radius = selectPenPoints.Width+4;
            instance.DrawEllipse(selectPenPoints, new RectangleF((float)(point.X - radius / 2), (float)(point.Y - radius / 2), radius, radius));
        }

        public static void DrawRectangle(this Graphics instance, RectangleToDisplay rectangleToDisplay, PenToDisplayCollection penCollection)
        {
            if (rectangleToDisplay == null) throw new ArgumentNullException();
            if (penCollection == null) throw new ArgumentNullException();

            var rectangle = rectangleToDisplay.Rectangle;
            instance.DrawRectangle(penCollection.MainPen, (float)rectangle.TopLeft.X, (float)rectangle.TopLeft.Y, (float)rectangle.Width, (float)rectangle.Height);
            instance.FillRectangle(new SolidBrush(rectangleToDisplay.Color), rectangle);

            foreach (var point in rectangle.Vertices)
            {
                if(point.IsChosen)
                    instance.DrawPoint(point, penCollection.SelectPenPoints);
            }
        }
    }
}