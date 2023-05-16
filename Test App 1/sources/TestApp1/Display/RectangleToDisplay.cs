using System;
using System.Drawing;

namespace TestApp1.Display
{
    public class RectangleToDisplay
    {
        public Guid Id { get; set; }
        public Color Color { get; set; }
        public Rectangle Rectangle { get; set; }

        public RectangleToDisplay(Rectangle rectangle, Color color)
        {
            Color = color;
            Rectangle = rectangle;
            Id = Guid.NewGuid();
            AssignPoints();
        }

        private void AssignPoints()
        {
            foreach (var point in Rectangle.Vertices)
            {
                point.ParentGuid = Id;
            }
        }
    }
}