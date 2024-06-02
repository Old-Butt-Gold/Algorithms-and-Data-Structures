using AaDS.Geometry.Shared;

namespace AaDS.Geometry;

/// <summary>
/// You are given an array coordinates, coordinates[i] = [x, y], where [x, y] represents the coordinate of a point. Check if these points make a straight line in the XY plane.
/// </summary>

static class StraightLine
{
    public static bool CheckStraightLine(int[][] coordinates)
    {
        if (coordinates.Length < 3) return true;
            
        int x0 = coordinates[0][0];
        int y0 = coordinates[0][1];
        int x1 = coordinates[1][0];
        int y1 = coordinates[1][1];
        
        //slope = m = (y1 - y) / (x1 - x);

        int dx = x1 - x0;
        int dy = y1 - y0;

        for (int i = 2; i < coordinates.Length; i++)
        {
            int x = coordinates[i][0];
            int y = coordinates[i][1];
                
            if (dy * (x - x0) != dx * (y - y0))
            {
                return false;
            }
        }

        return true;
    }
    
    const double Epsilon = 1e-9; // допустимая погрешность для сравнения

    public static bool CheckStraightLine(Point[] coordinates)
    {
        if (coordinates.Length < 3) return true;
        
        var x0 = coordinates[0].X;
        var y0 = coordinates[0].Y;
        var x1 = coordinates[1].X;
        var y1 = coordinates[1].Y;
        
        //slope = m = (y1 - y) / (x1 - x);

        var dx = x1 - x0;
        var dy = y1 - y0;

        for (int i = 2; i < coordinates.Length; i++)
        {
            var x = coordinates[i].X;
            var y = coordinates[i].Y;
                
            if (Math.Abs(dy * (x - x0) - dx * (y - y0)) > Epsilon)
            {
                return false;
            }
        }
        
        return true;
    }
}