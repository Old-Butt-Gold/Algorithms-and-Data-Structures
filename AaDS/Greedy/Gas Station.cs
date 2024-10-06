namespace AaDS.Greedy;

public static class Gas_Station
{
    /// <summary>
    /// There are n gas stations along a circular route, where the amount of gas at the ith station is gas[i].
    /// You have a car with an unlimited gas tank and it costs cost[i] of gas to travel from the ith station to its next (i + 1)th station. You begin the journey with an empty tank at one of the gas stations.
    /// Given two integer arrays gas and cost, return the starting gas station's index if you can travel around the circuit once in the clockwise direction, otherwise return -1. If there exists a solution, it is guaranteed to be unique
    /// </summary>
    /// <param name="gas"></param>
    /// <param name="cost"></param>
    /// <returns></returns>
    public static int CanCompleteCircuit(int[] gas, int[] cost)
    {
        var totalGas = gas.Sum();
        var totalCost = cost.Sum();
        int start = 0;
        int tank = 0;

        if (totalGas < totalCost) return -1;

        for (int i = 0; i < gas.Length; i++)
        {
            tank += gas[i];
            tank -= cost[i];

            if (tank < 0)
            {
                start = i + 1;
                tank = 0;
            }
        }

        return start;
    }
}