using System.Collections;

namespace AaDS.DataStructures.List;

public class MyList<T> : IList<T> //Comparer<T> ломает, EqualityComparer не ломает
{
    T[] _list;
    int _current;
    const int DefaultSize = 8;
    
    public MyList() : this(DefaultSize) { }

    public MyList(IEnumerable<T> collection)
    {
        foreach (var item in collection)
            Add(item);
    }
    
    public MyList(int size) => _list = new T[size];

    public int Count => _current;
    public bool IsReadOnly { get; }

    void Resize()
    {
        T[] temp = new T[_list.Length * 2];
        Array.Copy(_list, temp, _list.Length);
        _list = temp;
    }
    
    public void Add(T item)
    {
        if (_current == _list.Length)
            Resize();
        _list[_current++] = item;
    }

    public void Clear()
    {
        Array.Clear(_list);
        _current = 0;
    }
    
    public bool Contains(T item)
    {
        for (int i = 0; i < _current; i++)
            if (EqualityComparer<T>.Default.Equals(item, _list[i]))
                return true;
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (arrayIndex < 0 || arrayIndex > _current)
            throw new ArgumentException("Invalid arrayIndex");

        if (array.Length - arrayIndex < _current)
            throw new ArgumentException("Array is too small to hold the elements");

        Array.Copy(_list, 0, array, arrayIndex, _current);
    }


    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if (index >= 0)
        {
            for (int i = index; i < _current - 1; i++)
                _list[i] = _list[i + 1];
            _current--;
            return true;
        }
        return false;
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < _current; i++)
            if (EqualityComparer<T>.Default.Equals(item, _list[i]))
                return i;
        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > _current)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

        if (_current == _list.Length)
            Resize();

        for (int i = _current; i > index; i--)
            _list[i] = _list[i - 1];
        
        _list[index] = item;
        _current++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _current)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

        for (int i = index; i < _current - 1; i++)
            _list[i] = _list[i + 1];

        _current--;
    }


    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _current)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            return _list[index];
        }
        set
        {
            if (index < 0 || index >= _current)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            _list[index] = value;
        }
    }
    
    public void Reverse()
    {
        for (int i = 0; i < _current / 2; i++)
            (_list[i], _list[_current - 1 - i]) = (_list[_current - 1 - i], _list[i]);
    }
    
    public void ClearRange(int index, int count)
    {
        if (index < 0 || index + count > _current)
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid range.");

        for (int i = index; i < index + count; i++)
            _list[i] = default(T);

        for (int i = index + count; i < _current; i++)
            _list[i - count] = _list[i];
        _current -= count;
    }

    public MyList<T> FindAll(Predicate<T> match)
    {
        MyList<T> result = new();
        for (int i = 0; i < _current; i++)
        {
            if (match(_list[i]))
                result.Add(_list[i]);
        }
        return result;
    }

    public void ForEach(Action<T> action)
    {
        for (int i = 0; i < _current; i++)
            action(_list[i]);
    }

    
    public T[] ToArray()
    {
        T[] result = new T[_current];
        
        for (int i = 0; i < _current; i++)
            result[i] = _list[i];
        return result;
    }
    
    public void Sort() => Array.Sort(_list);
    
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _current; i++)
            yield return _list[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}