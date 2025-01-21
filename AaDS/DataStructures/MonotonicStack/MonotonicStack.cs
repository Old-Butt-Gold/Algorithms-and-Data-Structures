namespace AaDS.DataStructures.MonotonicStack;

public class MonotonicStack<T> where T : IComparable<T>
{
    private readonly Stack<T> _stack = [];
    private readonly Func<T, T, bool> _comparison;

    /// <summary>
    /// Создает экземпляр MonotonicStack.
    /// </summary>
    /// <param name="comparison">Функция сравнения для поддержания монотонности.</param>
    public MonotonicStack(Func<T, T, bool> comparison)
    {
        _comparison = comparison;
    }

    /// <summary>
    /// Добавляет элемент в стек.
    /// </summary>
    /// <param name="item">Элемент для добавления.</param>
    /// <param name="postAction">Действия, после удаления</param>
    public void Push(T item, Action<T>? postAction = null)
    {
        while (_stack.Count > 0 && _comparison(_stack.Peek(), item))
        {
            var lastItem = _stack.Pop();
            postAction?.Invoke(lastItem);
        }
        _stack.Push(item);
    }

    /// <summary>
    /// Удаляет верхний элемент из стека.
    /// </summary>
    public void Pop()
    {
        if (_stack.Count > 0)
        {
            _stack.Pop();
        }
    }

    /// <summary>
    /// Возвращает верхний элемент стека без его удаления.
    /// </summary>
    /// <returns>Верхний элемент стека.</returns>
    public T Peek()
    {
        if (_stack.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty.");
        }
        return _stack.Peek();
    }

    /// <summary>
    /// Проверяет, пуст ли стек.
    /// </summary>
    /// <returns>True, если стек пуст, иначе False.</returns>
    public bool IsEmpty => _stack.Count == 0;

    /// <summary>
    /// Возвращает количество элементов в стеке.
    /// </summary>
    public int Count => _stack.Count;

    /// <summary>
    /// Возвращает элементы стека в виде массива.
    /// </summary>
    /// <returns>Массив элементов стека.</returns>
    public T[] ToArray()
    {
        return _stack.ToArray();
    }
}