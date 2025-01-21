namespace AaDS.Monotonic_Stack;

/// <summary>
/// https://leetcode.com/problems/online-stock-span/?envType=study-plan-v2
/// </summary>
public class StockSpanner
{
    Stack<(int price, int span)> _stack = [];
    
    public int Next(int price)
    {
        int span = 1;
        while (_stack.Count > 0 && _stack.Peek().price <= price)
        {
            span += _stack.Pop().span;
        }

        _stack.Push((price, span));
        return span;
    }
}