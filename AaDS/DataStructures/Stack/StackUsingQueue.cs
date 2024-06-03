using System.Collections.Immutable;

namespace AaDS.DataStructures.Stack;

/// <summary>
/// One queue. When to Use: Choose this approach when you expect pop and top operations to be more frequent than push.
/// </summary>
public class StackUsingOneQueue<T>
{
    Queue<T> _queue = [];

    public void Push(T x)
    {
        _queue.Enqueue(x);
        for (int i = 0; i < _queue.Count - 1; i++)
        {
            _queue.Enqueue(_queue.Dequeue());
        }
    }

    public T Pop() => _queue.Dequeue();

    public T Peek() => _queue.Peek();

    public bool Empty() => _queue.Count == 0;
}

/// <summary>
/// Two queues. When to Use: Choose this approach when the frequency of push and empty operations is higher than pop and top.
/// </summary>
/// <typeparam name="T"></typeparam>
public class StackUsingTwoQueues<T>
{
    Queue<T> _first = [];
    Queue<T> _second = [];

    public void Push(T x)
    {
        _first.Enqueue(x);
    }

    public T Pop()
    {
        while (_first.Count > 1)
        {
            _second.Enqueue(_first.Dequeue());
        }

        var popped = _first.Dequeue();

        (_first, _second) = (_second, _first);

        return popped;
    }

    public T Peek()
    {
        while (_first.Count > 1)
        {
            _second.Enqueue(_first.Dequeue());
        }

        var peek = _first.Peek();
        
        _second.Enqueue(_first.Dequeue());
        
        (_first, _second) = (_second, _first);

        return peek;
    }

    public bool Empty() => _first.Count == 0;
}