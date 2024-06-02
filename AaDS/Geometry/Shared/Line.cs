namespace AaDS.Geometry.Shared;

/// <summary>
/// Line object
/// </summary>
class Line
{
    public Point Start { get; private set; }
    public Point End { get; private set; }

    public bool IsVertical => AreEqual(Start.X, End.X);
    public bool IsHorizontal => AreEqual(Start.Y, End.Y);

    public double Slope { get; private set; }

    public readonly double Precision;

    public Line(Point start, Point end, double precision = 5)
    {
        Precision = Math.Pow(10, -precision);
        
        if (start.X < end.X || (AreEqual(start.X, end.X) && start.Y < end.Y))
        {
            Start = start;
            End = end;
        }
        else
        {
            Start = end;
            End = start;
        }

        Slope = CalcSlope();
    }

    double CalcSlope()
    {
        if (IsVertical)
        {
            return double.PositiveInfinity;
        }

        return (End.Y - Start.Y) / (End.X - Start.X);
    }

    bool AreEqual(double a, double b) => Math.Abs(a - b) < Precision;

    public Line Clone() => new Line(Start.Clone(), End.Clone());
}