using AaDS.Geometry.Shared;

namespace AaDS.Geometry;

/// <summary>
///     Rotates given point by given angle about given center.
/// </summary>
static class PointRotation
{
    public static Point Rotate(Point center, Point point, int angle)
    {
        var angleInRadians = angle * (float)(Math.PI / 180);
        
        var cosTheta = (float)Math.Cos(angleInRadians);
        var sinTheta = (float)Math.Sin(angleInRadians);

        var x = cosTheta * (point.X - center.X) -
            sinTheta * (point.Y - center.Y) + center.X;

        var y = sinTheta * (point.X - center.X) +
                cosTheta * (point.Y - center.Y) + center.Y;

        return new Point(x, y);
    }
}