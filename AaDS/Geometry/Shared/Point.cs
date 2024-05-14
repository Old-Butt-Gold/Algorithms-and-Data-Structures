namespace AaDS.Geometry.Shared;

/// <summary>
///     Point object.
/// </summary>
class Point
{
    public Point(double x, double y) => (X, Y) = (x, y);

    public double X { get; }
    public double Y { get; }

    public override string ToString() => X.ToString("F") + " " + Y.ToString("F");

    public Point Clone() => new(X, Y);
}