using System;
using System.IO;
using System.Linq;
using TestApp1.i18n;
using System.Drawing;
using TestApp1.Display;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace TestApp1
{
    public partial class TestApp1Form : Form
    {
        private readonly ILocale _locale;
        private ILogger _logger;

        private const float XOffset = 100;
        private const float YOffset = 100;
        private const int ClickPointAccuracy = 20;

        private Graphics _graphics;
        private delegate void ColorFunction(Color color);
        private ColorFunction _currentColorFunction;

        private Color _pickedColor;
        private readonly Color GraphicsBackColor = Color.White;
        private List<Color> ignoredColors = new List<Color>();
        private List<Color> rectanglesColors = new List<Color>();
        private PenToDisplayCollection _penToDisplayCollection;
        private RectangleToDisplayRepository _rectanglesRepository = new RectangleToDisplayRepository();

        public TestApp1Form()
        {
            _locale = new RussianLocale();

            SetupLogger();

            _logger.LogDebug("Application started.");

            InitializeComponent();
            Setup();

            _logger.LogDebug("Setup successfully ended.");
        }



        #region Setup

        private void Setup()
        {
            ColorButton.Text = _locale.ColorButtonText;

            var rectList = GetDummyRectangles();
            InitRectangleArray(rectList);

            SetupGraphics();
            ReDraw();
        }

        private void SetupLogger()
        {
            var pattern = "MM-dd-yy__HH_MM_ss";
            var logFileName = "log " + DateTime.Now.ToString(pattern) + ".txt";
            var folderPath = Path.Combine(Environment.CurrentDirectory, "logs");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            _logger = new FileLogger(Path.Combine(folderPath, logFileName));
        }

        private void SetUpInterface()
        {
            //color button
            ColorButton.Enabled = true;
            ColorButtonLabel.Visible = true;
            var buttonColor = rectanglesColors.Count == 0 ? Color.White : rectanglesColors.First();
            _pickedColor = buttonColor;
            ColorButtonLabel.Text = _locale.ColorButtonLabelText;

            if(rectanglesColors.Count > 0)
                ColorListClickAction(_pickedColor, _locale.IgnoreThisColorButtonText, IgnoreColor);

            //labels
            ColorsListBoxLabel.Visible = true;
            IgnoredColorsListBoxLabel.Visible = true;
            ColorsListBoxLabel.Text = _locale.ColorListLabelText;
            IgnoredColorsListBoxLabel.Text = _locale.IgnoredColorListLabelText;

            //colorList boxes

            ColorsListBox.Visible = true;
            IgnoredColorsListBox.Visible = true;
            UpdateColorsListBox();
        }

        private void InitRectangleArray(List<RectangleToDisplay> rectList)
        {
            if (rectList == null) throw new ArgumentNullException();
            if (rectList.Count == 0) throw new ArgumentException("Empty collection");

            foreach (var rectangleToDisplay in rectList)
            {
                _rectanglesRepository.Create(rectangleToDisplay);

                if (!rectanglesColors.Contains(rectangleToDisplay.Color))
                    rectanglesColors.Add(rectangleToDisplay.Color);
            }

            _logger.LogInformation($"{rectList.Count} rectangles were added.");

            for (var i = 0; i < rectList.Count; i++)
            {
                var pointsCoordinates = GetStringPointCoordinates(rectList[i].Rectangle.Vertices.ToList());

                _logger.LogDebug($"Rectangle {i + 1} points: {pointsCoordinates} ;");
            }
        }

        //for the debug purposes
        private string GetStringPointCoordinates(List<Point> points)
        {
            if (points == null) throw new ArgumentNullException();
            if (points.Count == 0) throw new ArgumentException("Empty collection");

            var pointsCoordinates = string.Empty;

            foreach (var point in points)
            {
                pointsCoordinates += $"{GetStringPointCoordinates(point)},";
            }

            //remove last dot
            pointsCoordinates = pointsCoordinates.Remove(pointsCoordinates.Length - 1);
            return pointsCoordinates;
        }

        private string GetStringPointCoordinates(Point point)
        {
            if (point == null) throw new ArgumentNullException();

            return $"({point.X}, {point.Y})";
        }

        private List<RectangleToDisplay> GetDummyRectangles()
        {
            Random r = new Random();
            var rect1 = new Rectangle(new Point(r.Next(0, 50) -50, r.Next(0, 50) -50), r.Next(50, 100), r.Next(50, 100));
            var rect2 = new Rectangle(new Point(r.Next(0, 100) +100, r.Next(0, 100)), r.Next(50, 100), r.Next(50, 100));
            var rect3 = new Rectangle(new Point(r.Next(0, 100) +200 , r.Next(0, 100) +200), r.Next(50, 100), r.Next(50, 100));

            var rect4 = new Rectangle(new Point(-50, 100), 50, 100);
            var rect5 = new Rectangle(new Point(100, 200), 50, 100);


            var rectList = new List<RectangleToDisplay>
            {
                new RectangleToDisplay(rect1, Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))),
                new RectangleToDisplay(rect2, Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))),
                new RectangleToDisplay(rect3, Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))),
                //not random
                new RectangleToDisplay(rect4, Color.FromArgb(0, 255, 0)),
                new RectangleToDisplay(rect5,  Color.FromArgb(0, 255, 0))
            };

            return rectList;
        }

        private void SetupGraphics()
        {
            _graphics = pictureBox1.CreateGraphics();
            _graphics.TranslateTransform(XOffset, YOffset);

            var mainPen = new Pen(Color.Black, 2);
            var selectPenLines = new Pen(Color.Blue, mainPen.Width + 1);
            var selectPenPoints = new Pen(Color.Red, selectPenLines.Width);

            _penToDisplayCollection = new PenToDisplayCollection(
                mainPen: mainPen,
                selectPenPoints: selectPenPoints,
                selectPenLines: selectPenLines);
        }

        private void ReDraw()
        {
            _graphics.Clear(GraphicsBackColor);
            _graphics.DrawRectangles(_rectanglesRepository.Get().ToList(), _penToDisplayCollection);
            UpdateColorsListBox();
            DrawMainRectangle();
        }
        #endregion
        #region Events

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var coordinate = (MouseEventArgs)e;
            var x = Convert.ToInt32(coordinate.X - _graphics.Transform.OffsetX);
            var y = Convert.ToInt32(coordinate.Y - _graphics.Transform.OffsetY);
            var clickPoint = new Point(x, y);

            SelectPoint(clickPoint);

            ReDraw();

            if (!ColorButton.Enabled) 
                SetUpInterface(); 
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorButton.Enabled = false;
            _currentColorFunction(_pickedColor);
        }

        private void ColorsList_MouseClick(object sender, MouseEventArgs e)
        {
            if(ColorsListBox.SelectedItem == null) return;

            ColorListClickAction(
                pickedColor: (Color)ColorsListBox.SelectedItem,
                colorButtonText: _locale.IgnoreThisColorButtonText,
                colorFunction: IgnoreColor);
            _pickedColor = (Color)ColorsListBox.SelectedItem;
        }

        private void ColorsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ColorsListBox.SelectedItem == null) return;

            ColorListDoubleClickAction(
                pickedColor: (Color)ColorsListBox.SelectedItem,
                colorButtonText: _locale.IgnoreThisColorButtonText,
                colorFunction: IgnoreColor);
        }

        private void IgnoredColorsList_MouseClick(object sender, MouseEventArgs e)
        {
            if (IgnoredColorsListBox.SelectedItem == null) return;

            ColorListClickAction(
                pickedColor: (Color)IgnoredColorsListBox.SelectedItem,
                colorButtonText: _locale.StopIgnoreThisColorButtonText,
                colorFunction: StopIgnoreColor);
        }

        private void IgnoredColorsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (IgnoredColorsListBox.SelectedItem == null) return;

            ColorListDoubleClickAction(
                pickedColor: (Color)IgnoredColorsListBox.SelectedItem,
                colorButtonText: _locale.StopIgnoreThisColorButtonText,
                colorFunction: StopIgnoreColor);
        }

        #endregion
        #region Main functionality

        private void SelectPoint(Point clickPoint)
        {
            var points = new List<Point>();

            foreach (var rectangleToDisplay in _rectanglesRepository.Get().ToList())
            {
                points.AddRange(rectangleToDisplay.Rectangle.Vertices);
            }

            var nearestPoint = GeometryHelper.FindNearestPoint(points, clickPoint, ClickPointAccuracy);

            if (nearestPoint != null)
            {
                nearestPoint.IsChosen = !nearestPoint.IsChosen;
            }
            else
            {
                _logger.LogInformation($"There are no dots in the selected position: {GetStringPointCoordinates(clickPoint)}");
            }

            _logger.LogInformation(clickPoint.IsChosen
                ? $"Point unselected: ({clickPoint.X}, {clickPoint.Y});"
                : $"Point selected: ({clickPoint.X}, {clickPoint.Y});");
        }

        private void DrawMainRectangle()
        {
            var rect = GetMainRectangle();

            if( rect == null ) return;

            _logger.LogInformation($"The boundaries of the main rectangle were found at the following points: {GetStringPointCoordinates(rect.Vertices.ToList())}");

            _graphics.DrawLine(_penToDisplayCollection.SelectPenLines, rect.BotLeft, rect.BotRight);
            _graphics.DrawLine(_penToDisplayCollection.SelectPenLines, rect.TopRight, rect.BotRight);
            _graphics.DrawLine(_penToDisplayCollection.SelectPenLines, rect.BotLeft, rect.TopLeft);
            _graphics.DrawLine(_penToDisplayCollection.SelectPenLines, rect.TopLeft, rect.TopRight);
        }

        private Rectangle GetMainRectangle()
        {
            var points = GetSuitablePoints(_rectanglesRepository.Get(r=>!ignoredColors.Contains(r.Color)).ToList());

            if( points == null ) return null;

            var bounds = GeometryHelper.FindBoundaryPoints(points);

            return new Rectangle(bounds[0], bounds[1]);
        }

        private List<Point> GetSuitablePoints(List<RectangleToDisplay> rectangles)
        {
            if (rectangles == null) throw new ArgumentNullException();
            if(rectangles.Count == 0) return null;

            var suitableRectangles = rectangles.Where(r => !ignoredColors.Contains(r.Color)).ToList();
            var suitablePoints = new List<Point>();

            foreach (var rectangleToDisplay in suitableRectangles)
            {
                foreach (var point in rectangleToDisplay.Rectangle.Vertices)
                {
                    if (!point.IsChosen)
                        suitablePoints.Add(point);
                }
            }

            _logger.LogInformation($"The following suitable points were found: {GetStringPointCoordinates(suitablePoints)}");

            return suitablePoints;
        }

        #endregion
        #region IgnoreColor functionality

        private void ColorListClickAction(Color pickedColor, string colorButtonText, ColorFunction colorFunction)
        {
            _pickedColor = pickedColor;
            ColorButton.Enabled = true;
            ColorButton.Text = colorButtonText;
            ColorButton.BackColor = pickedColor;
            _currentColorFunction = colorFunction;

            if (ColorButton.Visible == false)
            {
                ColorButton.Visible = true;
                ReDraw();
            }

        }

        private void ColorListDoubleClickAction(Color pickedColor, string colorButtonText, ColorFunction colorFunction)
        {
            _pickedColor = pickedColor;
            ColorButton.Enabled = false;
            ColorButton.Text = colorButtonText;
            ColorButton.BackColor = pickedColor;
            _currentColorFunction = colorFunction;

            _currentColorFunction(pickedColor);
            ReDraw();
        }

        private void UpdateColorsListBox()
        {
            ColorsListBox.Items.Clear();
            foreach (var rectanglesColor in rectanglesColors)
            {
                if (!ColorsListBox.Items.Contains(rectanglesColor))
                    ColorsListBox.Items.Add(rectanglesColor);
            }

            IgnoredColorsListBox.Items.Clear();

            foreach (var rectanglesColor in ignoredColors)
            {
                if (!IgnoredColorsListBox.Items.Contains(rectanglesColor))
                    IgnoredColorsListBox.Items.Add(rectanglesColor);
            }
        }

        private void IgnoreColor(Color color)
        {
            rectanglesColors.Remove(color);
            ignoredColors.Add(color);
            ColorsListBox.Items.Remove(color);
            IgnoredColorsListBox.Items.Add(color);
            ColorButton.Text = _locale.ThisColorIgnoredButtonText;

            _logger.LogInformation($"Color {color} added to the ignore list.");

            ReDraw();
        }

        private void StopIgnoreColor(Color color)
        {
            ignoredColors.Remove(color);
            rectanglesColors.Add(color);
            IgnoredColorsListBox.Items.Remove(color);
            ColorsListBox.Items.Add(color);
            ColorButton.Text = _locale.ThisColorNotIgnoredButtonText;

            _logger.LogInformation($"Color {color} removed from ignore list.");

            ReDraw();
        }

        #endregion
    }
}