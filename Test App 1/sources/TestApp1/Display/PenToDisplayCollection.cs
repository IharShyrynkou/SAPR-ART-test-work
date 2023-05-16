using System.Drawing;

namespace TestApp1.Display
{
    public class PenToDisplayCollection
    {
        public Pen MainPen { get; set; }
        public Pen SelectPenLines { get; set; }
        public Pen SelectPenPoints { get; set; }

        public PenToDisplayCollection(Pen mainPen, Pen selectPenLines, Pen selectPenPoints)
        {
            MainPen = mainPen;
            SelectPenLines = selectPenLines;
            SelectPenPoints = selectPenPoints;
        }
    }
}