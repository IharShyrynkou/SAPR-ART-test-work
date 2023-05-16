namespace TestApp1.Tests;

public class GeometryHelperTests
{
    [Theory]
    [InlineData(150,100,130,100,30)]
    [InlineData(100, 100, 100, 100, 10)]
    [InlineData(150, 150, 160, 140, 20)]
    [InlineData(100, 100, 91, 91, 10)]
    public void FindNearestPoint(int expectedResultX, int expectedResultY, int clickPointX, int clickPointY, int accuracy)
    {
        // Arrange
        var expectedResult = new Point(expectedResultX, expectedResultY);

        var points = new List<Point>()
        {
            new Point(100, 100),
            new Point(150, 150),
            new Point(150, 100),
            new Point(100, 150),
        };
        var clickPoint = new Point(clickPointX, clickPointY);

        // Act
        var actualResult = GeometryHelper.FindNearestPoint(points, clickPoint,accuracy);

        // Assert
        Assert.True(PointsAreEqual(expectedResult, actualResult));
    }

    [Theory]
    [InlineData(
        100, 200, 
        200, 100, 
        100, 150, 
        200, 100,
        120, 120,
        100, 200
        )]
    [InlineData(
        -10, 300,
        200, -20,
        160, 100,
        200, 300,
        -10, 90,
        100, -20
    )]
    public void FindBoundaryPoints( int expectedResult0X, int expectedResult0Y, int expectedResult1X, int expectedResult1Y,
        int point1X, int point1Y, int point2X, int point2Y,
        int point3X, int point3Y, int point4X, int point4Y)
    {
        // Arrange
        var expectedResult = new List<Point>
        {
            new Point(expectedResult0X, expectedResult0Y),
            new Point(expectedResult1X, expectedResult1Y),
        };

        var points = new List<Point>()
        {
            new Point(point1X, point1Y),
            new Point(point2X, point2Y),
            new Point(point3X, point3Y),
            new Point(point4X, point4Y),
        };

        // Act
        var actualResult = GeometryHelper.FindBoundaryPoints(points);

        // Assert
        for (int i = 0; i < actualResult.Length; i++)
        {
            Assert.True(PointsAreEqual(expectedResult[i], actualResult[i]));
        }
    }

    private bool PointsAreEqual(Point point1, Point point2)
    {
        var TOLERANCE = 0.00001;

        var a = Math.Abs(point1.X - point2.X) <= TOLERANCE;
        return (Math.Abs(point1.X - point2.X) <= TOLERANCE) && (Math.Abs(point1.Y - point2.Y) <= TOLERANCE);
    }
}