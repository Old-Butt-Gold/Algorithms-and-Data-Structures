namespace AaDS.PrefixSum;

public static class HighestAltitude
{
    /// <summary>
    /// There is a biker going on a road trip. The road trip consists of n + 1 points at different altitudes. The biker starts his trip on point 0 with altitude equal 0.
    /// You are given an integer array gain of length n where gain[i] is the net gain in altitude between points i and i + 1 for all (0 &lt;= i &lt; n).
    /// </summary>
    /// <param name="gain"></param>
    /// <returns>the highest altitude of a point.</returns>
    public static int LargestAltitude(int[] gain)
    {
        int maxAltitude = 0;
        int currentAltitude = 0;

        for (int i = 0; i < gain.Length; i++)
        {
            currentAltitude += gain[i];
            maxAltitude = Math.Max(maxAltitude, currentAltitude);
        }

        return maxAltitude;
    }
}