using System.Collections;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.LinkedList;

public class SkipList<T> : IEnumerable<T> where T : IComparable<T>
{
    const double Probability = 0.5;
    readonly int _maxLevel;
    readonly SkipListNode<T> _head;
    readonly Random _random = new();
    public int Count { get; private set; }

    public bool IsEmpty => Count == 0;

    public SkipList(int maxLevel)
    {
        _maxLevel = maxLevel;
        _head = new SkipListNode<T>(default!, maxLevel);
    }

    public void Insert(T value)
    {
        var update = new SkipListNode<T>[_maxLevel + 1];
        var current = _head;

        for (int i = _maxLevel; i > -1; i--)
        {
            while (current!.Forward[i] != null && current.Forward[i]!.Value.CompareTo(value) < 0)
            {
                current = current.Forward[i];
            }

            update[i] = current;
        }

        int level = RandomLevel();
        var newNode = new SkipListNode<T>(value, level);

        for (int i = 0; i <= level; i++)
        {
            newNode.Forward[i] = update[i].Forward[i];
            update[i].Forward[i] = newNode;
        }

        Count++;
        
        int RandomLevel()
        {
            int level = 0;
            while (_random.NextDouble() < Probability && level < _maxLevel)
            {
                level++;
            }

            return level;
        }
    }

    public bool Search(T value)
    {
        var current = _head;

        for (int i = _maxLevel; i > -1; i--)
        {
            while (current!.Forward[i] != null && current.Forward[i]!.Value.CompareTo(value) < 0)
            {
                current = current.Forward[i];
            }
        }
        
        current = current.Forward[0];
            
        return current != null && current.Value.CompareTo(value) == 0;
    }

    public bool Delete(T value)
    {
        var update = new SkipListNode<T>[_maxLevel + 1];
        var current = _head;

        for (int i = _maxLevel; i > -1; i--)
        {
            while (current!.Forward[i] != null && current.Forward[i]!.Value.CompareTo(value) < 0)
            {
                current = current.Forward[i];
            }

            update[i] = current;
        }

        current = current.Forward[0];

        if (current != null && current.Value.CompareTo(value) == 0)
        {
            for (int i = 0; i <= _maxLevel; i++)
            {
                if (update[i].Forward[i] != current)
                {
                    break;
                }

                update[i].Forward[i] = current.Forward[i];
            }

            Count--;
            return true;
        }

        return false;
    }
    
    public void Clear()
    {
        for (int i = 0; i <= _maxLevel; i++)
        {
            _head.Forward[i] = null;
        }
        Count = 0;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        var current = _head.Forward[0];
        while (current != null)
        {
            yield return current.Value;
            current = current.Forward[0];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}