namespace AaDS.DataStructures.Queue;

/// <summary>
/// Using two stacks
/// </summary>
public class QueueUsingTwoStacks<T>
{
    Stack<T> _input = [];
    Stack<T> _output = [];
    
    public void Enqueue(T x) => _input.Push(x);

    public T Dequeue()
    {
        Peek();
        return _output.Pop();
    }

    public T Peek()
    {
        if (_output.Count == 0)
        {
            while (_input.Count != 0)
            {
                _output.Push(_input.Pop());
            }
        }

        return _output.Peek();
    }

    public bool Empty() => _input.Count == 0 && _output.Count == 0;
}