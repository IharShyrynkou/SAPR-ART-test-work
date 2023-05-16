using System.Globalization;

namespace TestApp2
{
    public partial class TestApp2Form : Form
    {
        private readonly GeometryHelper _geometryHelper;
        private readonly Graphics _graphics;
        private readonly Pen _pen;
        private readonly Pen _dotPen;
        private readonly Pen _dashPen;
        public TestApp2Form()
        {
            InitializeComponent();

            _graphics = pictureBox1.CreateGraphics();
            _graphics.TranslateTransform(100, -100);
            _graphics.Clear(Color.White);

            _pen = new Pen(Color.Black, 5);
            _dotPen = new Pen(Color.Black, 1);
            _dashPen = new Pen(Color.DimGray, 3);
            _geometryHelper = new GeometryHelper(pictureBox1.Height);

            //TODO:for the refactoring
            GraphicsExtension.geometryHelper = _geometryHelper;

            var inputData = InitData();

            InitInterface(inputData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _graphics.Clear(Color.White);

            var inputData = GetDataFromInterfaceInterface();

            var pointP5 = _geometryHelper.GetMiddlePoint(inputData.PointP3, inputData.PointP4);

            DrawBaseLines(inputData, pointP5);

            _graphics.DisplayFiledRectangle(inputData.PointP1, inputData.h3, pointP5, Color.Khaki);
            _graphics.DisplayFiledRectangle(inputData.PointP2, inputData.h4, pointP5, Color.Khaki);

            _graphics.DisplayDashLine(_dashPen, inputData.PointP1, pointP5);
            _graphics.DisplayDashLine(_dashPen, inputData.PointP2, pointP5);

            _graphics.DisplayCircle(_dotPen, pointP5, Color.Red, 3);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _graphics.Clear(Color.White);

            var inputData = GetDataFromInterfaceInterface();

            var result = GetResultPoints(inputData,
                out var pointP5,
                out var leftHighIntersection,
                out var rightHighIntersection,
                out var leftBotIntersection,
                out var rightBotIntersection);


            DrawResults(inputData, pointP5, result, leftHighIntersection, rightHighIntersection, leftBotIntersection, rightBotIntersection);

            T1Label.Text = result.PointT1.ToString();
            T2Label.Text = result.PointT2.ToString();
            T3Label.Text = result.PointT3.ToString();
            T4Label.Text = result.PointT4.ToString();
        }

        private void InitInterface(InputData inputData)
        {
            ValueATextBox.Text = inputData.a.ToString(CultureInfo.CurrentCulture);
            ValueH1TextBox.Text = inputData.h1.ToString(CultureInfo.CurrentCulture);
            ValueH2TextBox.Text = inputData.h2.ToString(CultureInfo.CurrentCulture);
            ValueH3TextBox.Text = inputData.h3.ToString(CultureInfo.CurrentCulture);
            ValueH4TextBox.Text = inputData.h4.ToString(CultureInfo.CurrentCulture);

            p1xTextBox.Text = inputData.PointP1.X.ToString(CultureInfo.CurrentCulture);
            p1yTextBox.Text = inputData.PointP1.Y.ToString(CultureInfo.CurrentCulture);

            p2xTextBox.Text = inputData.PointP2.X.ToString(CultureInfo.CurrentCulture);
            p2yTextBox.Text = inputData.PointP2.Y.ToString(CultureInfo.CurrentCulture);

            p3xTextBox.Text = inputData.PointP3.X.ToString(CultureInfo.CurrentCulture);
            p3yTextBox.Text = inputData.PointP3.Y.ToString(CultureInfo.CurrentCulture);

            p4xTextBox.Text = inputData.PointP4.X.ToString(CultureInfo.CurrentCulture);
            p4yTextBox.Text = inputData.PointP4.Y.ToString(CultureInfo.CurrentCulture);

        }

        private InputData GetDataFromInterfaceInterface()
        {
            return new InputData()
            {
                a = float.Parse(ValueATextBox.Text),
                h1 = float.Parse(ValueH1TextBox.Text),
                h2 = float.Parse(ValueH2TextBox.Text),
                h3 = float.Parse(ValueH3TextBox.Text),
                h4 = float.Parse(ValueH4TextBox.Text),

                PointP1 = new PointF(float.Parse(p1xTextBox.Text), float.Parse(p1yTextBox.Text)),
                PointP2 = new PointF(float.Parse(p2xTextBox.Text), float.Parse(p2yTextBox.Text)),
                PointP3 = new PointF(float.Parse(p3xTextBox.Text), float.Parse(p3yTextBox.Text)),
                PointP4 = new PointF(float.Parse(p4xTextBox.Text), float.Parse(p4yTextBox.Text)),
            };
        }

        private void DrawResults(InputData inputData, PointF pointP5, ResultPoints result, PointF leftHighIntersection,
            PointF rightHighIntersection, PointF leftBotIntersection, PointF rightBotIntersection)
        {
            DrawBaseLines(inputData, pointP5);

            _graphics.DisplayFiledRectangle(result.PointT1, inputData.h3, result.PointT2, Color.Khaki);
            _graphics.DisplayFiledRectangle(result.PointT3, inputData.h4, result.PointT4, Color.Khaki);

            _graphics.DisplayCircle(_dotPen, result.PointP5, Color.Red, 2);

            _graphics.DisplayDashLine(_dashPen, result.PointT1, result.PointT2);
            _graphics.DisplayDashLine(_dashPen, result.PointT3, result.PointT4);

            _graphics.DisplayCircle(_dotPen, leftHighIntersection, Color.Green);
            _graphics.DisplayCircle(_dotPen, rightHighIntersection, Color.Green);

            _graphics.DisplayCircle(_dotPen, leftBotIntersection, Color.Green);
            _graphics.DisplayCircle(_dotPen, rightBotIntersection, Color.Green);

            _graphics.DisplayCircle(_dotPen, result.PointT1, Color.Blue);
            _graphics.DisplayCircle(_dotPen, result.PointT2, Color.Blue);

            _graphics.DisplayCircle(_dotPen, result.PointT3, Color.Blue);
            _graphics.DisplayCircle(_dotPen, result.PointT4, Color.Blue);
        }

        private ResultPoints GetResultPoints(InputData inputData, out PointF pointP5, out PointF leftHighIntersection,
            out PointF rightHighIntersection, out PointF leftBotIntersection, out PointF rightBotIntersection)
        {
            pointP5 = _geometryHelper.GetMiddlePoint(inputData.PointP3, inputData.PointP4);

            var pointP5Project = GetPointP5Project(pointP5, inputData, out var p5Projects);

            leftHighIntersection = p5Projects[1];
            rightHighIntersection = p5Projects[0];

            leftBotIntersection = inputData.PointP1 with { Y = inputData.h1 / 2 };
            rightBotIntersection = inputData.PointP2 with { Y = inputData.h1 / 2 };

            var leftMiddlePoint = _geometryHelper.GetMiddlePoint(leftHighIntersection, leftBotIntersection);
            var rightMiddlePoint = _geometryHelper.GetMiddlePoint(rightHighIntersection, rightBotIntersection);

            var leftElementNewPoints = GetLeftLine(leftMiddlePoint, leftHighIntersection, leftBotIntersection, inputData.h3,
                pointP5, inputData);
            var rightElementNewPoints = GetRightLine(rightMiddlePoint, rightHighIntersection, rightBotIntersection,
                inputData.h4, pointP5, inputData);

            var result = new ResultPoints()
            {
                PointP5 = pointP5Project,
                PointT1 = leftElementNewPoints[0],
                PointT2 = leftElementNewPoints[1],
                PointT3 = rightElementNewPoints[0],
                PointT4 = rightElementNewPoints[1],
            };
            return result;
        }

        private PointF GetPointP5Project(PointF pointP5, InputData inputData, out PointF[] p5Projects)
        {
            p5Projects = _geometryHelper.OrthoLine(pointP5, inputData.PointP3, inputData.PointP4, inputData.h2 / 2);
            p5Projects = _geometryHelper.OrthoLine(p5Projects[0], p5Projects[0], p5Projects[1], inputData.a / 2);

            var pointP5Project = _geometryHelper.GetMiddlePoint(p5Projects[0], p5Projects[1]);
            return pointP5Project;
        }

        private static InputData InitData()
        {
            ReadPoints(out var pointP1, out var pointP2, out var pointP3, out var pointP4);
            ReadLengths(out var a, out var h1, out var h2, out var h3, out var h4);


            var data = new InputData()
            {
                a = a,
                h1 = h1,
                h2 = h2,
                h3 = h3,
                h4 = h4,
                PointP1 = pointP1,
                PointP2 = pointP2,
                PointP3 = pointP3,
                PointP4 = pointP4
            };
            return data;
        }

        private PointF[] GetLeftLine(PointF middlePoint, PointF highIntersection, PointF bottomIntersection, float height, PointF pointP5, InputData inputData)
        {
            var acLength = highIntersection.DistanceTo(bottomIntersection);
            var abLength = Math.Sqrt(Math.Pow(acLength, 2) - Math.Pow(height, 2));

            var mainLineA = _geometryHelper.GetCirclesIntersections(bottomIntersection, highIntersection, height, abLength)[0];
            var mainLineB = _geometryHelper.GetCirclesIntersections(highIntersection, bottomIntersection, height, abLength)[0];
            var mainLineMiddle = _geometryHelper.GetMiddlePoint(mainLineA, mainLineB);

            _geometryHelper.GetLineCircleIntersections(mainLineMiddle, inputData.PointP1.DistanceTo(pointP5) / 2,
                inputData.PointP1, inputData.PointP2, out _, out var pointT1);

            _geometryHelper.GetLineCircleIntersections(mainLineMiddle, inputData.PointP1.DistanceTo(pointP5) / 2,
                inputData.PointP4, inputData.PointP3, out _, out var pointT2);

            return new[]
            {
                pointT1,pointT2
            };
        }

        private PointF[] GetRightLine(PointF middlePoint, PointF highIntersection, PointF bottomIntersection, float height, PointF pointP5, InputData inputData)
        {
            var acLength = highIntersection.DistanceTo(bottomIntersection);
            var abLength = Math.Sqrt(Math.Pow(acLength, 2) - Math.Pow(height, 2));

            var mainLineA = _geometryHelper.GetCirclesIntersections(bottomIntersection, highIntersection, height, abLength)[0];
            var mainLineB = _geometryHelper.GetCirclesIntersections(highIntersection, bottomIntersection, height, abLength)[1];
            var mainLineMiddle = _geometryHelper.GetMiddlePoint(mainLineA, mainLineB);

            _geometryHelper.GetLineCircleIntersections(mainLineMiddle, inputData.PointP1.DistanceTo(pointP5) / 2,
                inputData.PointP1, inputData.PointP2, out var pointT3, out _);

            _geometryHelper.GetLineCircleIntersections(mainLineMiddle, inputData.PointP1.DistanceTo(pointP5) / 2,
                inputData.PointP4, inputData.PointP3, out var pointT4, out _);

            return new[]
            {
                pointT3,pointT4
            };
        }

        private void DrawBaseLines(InputData inputData, PointF pointP5)
        {
            _graphics.DisplayFiledRectangle(inputData.PointP1, inputData.h1, inputData.PointP2, Color.DodgerBlue);
            _graphics.DisplayFiledRectangle(inputData.PointP3, inputData.h2, inputData.PointP4, Color.DodgerBlue);

            _graphics.DisplayFiledRectangle(inputData.PointP1, inputData.h1, inputData.PointP2, Color.DodgerBlue);
            _graphics.DisplayFiledRectangle(inputData.PointP3, inputData.h2, inputData.PointP4, Color.DodgerBlue);

            _graphics.DisplayDashLine(_dashPen, inputData.PointP1, inputData.PointP2);
            _graphics.DisplayDashLine(_dashPen, inputData.PointP3, inputData.PointP4);

            _graphics.DisplayPoint(_dotPen, inputData.PointP1);
            _graphics.DisplayPoint(_dotPen, inputData.PointP2);
            _graphics.DisplayPoint(_dotPen, inputData.PointP3);
            _graphics.DisplayPoint(_dotPen, inputData.PointP4);
            //_graphics.DisplayCircle(_dotPen, pointP5, Color.Yellow,inputData.a);
        }

        private static void ReadLengths(out float a, out float h1, out float h2, out float h3, out float h4)
        {
            a = 25;
            h1 = 40;
            h2 = 40;
            h3 = 20;
            h4 = 20;
        }

        private static void ReadPoints(out PointF pointP1, out PointF pointP2, out PointF pointP3, out PointF pointP4)
        {
            //p1p2 = 400 = L
            pointP1 = new Point(0, 0);
            pointP2 = new Point(460, 0);

            pointP3 = new Point(0, 190);
            pointP4 = new Point(460, 220);
        }

        private void T1Label_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(T1Label.Text);
        }

        private void T2Label_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(T2Label.Text);
        }

        private void T3Label_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(T3Label.Text);
        }

        private void T4Label_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(T4Label.Text);
        }
    }
}