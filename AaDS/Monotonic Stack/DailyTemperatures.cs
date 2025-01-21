using AaDS.DataStructures.MonotonicStack;

namespace AaDS.Monotonic_Stack;

public static class DailyTemperatures
{
    /// <summary>
    /// Given an array of integers temperatures represents the daily temperatures,
    /// return an array answer such that answer[i] is the number of days you have to wait
    /// after the ith day to get a warmer temperature.
    /// If there is no future day for which this is possible, keep answer[i] == 0 instead.
    /// </summary>
    /// <param name="temperatures"></param>
    /// <returns></returns>
    public static int[] DailyTemperaturesI(int[] temperatures)
    {
        var waitingDays = new int[temperatures.Length];
        var stack = new Stack<int>();

        for (int i = 0; i < waitingDays.Length; i++)
        {
            while (stack.Count > 0 && temperatures[stack.Peek()] < temperatures[i])
            {
                var index = stack.Pop();
                waitingDays[index] = i - index;
            }

            stack.Push(i);
        }

        return waitingDays;
    }
    
    /// <summary>
    /// Using MonotomicStack data structure
    /// </summary>
    /// <param name="temperatures"></param>
    /// <returns></returns>
    public static int[] DailyTemperaturesII(int[] temperatures)
    {
        var waitingDays = new int[temperatures.Length];
        var stack = new MonotonicStack<(int temperature, int index)>((a, b) => a.temperature < b.temperature);

        for (int i = 0; i < waitingDays.Length; i++)
        {
            int currentIndex = i; //because of closure in Action
            stack.Push((temperatures[i], i), tuple =>
            {
                waitingDays[tuple.index] = currentIndex - tuple.index;
            });
        }
        
        return waitingDays;
    }
}

