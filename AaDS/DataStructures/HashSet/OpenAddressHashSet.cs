using System.Collections;

namespace AaDS.DataStructures.HashSet;


public class OpenAddressHashSet<T> : IEnumerable<T>
{
    class HashSetNode<T>
    {
        public T Value { get; set; }
        public HashSetNode(T value) => Value = value;
    }
    
    readonly int _initialBucketSize = 32;
    HashSetNode<T>?[] _hashArray;
    
    public OpenAddressHashSet()
    {
        _hashArray = new HashSetNode<T>[_initialBucketSize];
    }

    int GetHash(T value) => Math.Abs(value!.GetHashCode());

    int BucketSize => _hashArray.Length;
    public int Count { get; private set; }
    public bool IsEmpty => Count is 0;
    
    public bool Contains(T value)
    {
        var hashCode = GetHash(value);
        var index = hashCode % BucketSize;

        if (_hashArray[index] is null) 
            return false;

        var current = _hashArray[index];

        //keep track of this so that we won't circle around infinitely
        var hitKey = current.Value;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
                return true;

            index++;

            if (index == BucketSize)
                index = 0;

            current = _hashArray[index];

            if (current is not null && EqualityComparer<T>.Default.Equals(current.Value, hitKey)) 
                break;
        }
        return false;
    }

    public void Add(T value)
    {
        Grow();

        var hashCode = GetHash(value);

        var index = hashCode % BucketSize;

        if (_hashArray[index] is null)
        {
            _hashArray[index] = new(value);
        }
        else
        {
            var current = _hashArray[index];
            //keep track of this so that we won't circle around infinitely
            var hitKey = current!.Value;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                    break;
                
                index++;

                if (index == BucketSize)
                    index = 0;

                current = _hashArray[index];

                if (current != null && EqualityComparer<T>.Default.Equals(current.Value, hitKey))
                    break;
            }

            _hashArray[index] = new(value);
        }

        Count++;
    }
    
    public bool Remove(T value)
    {
        var hashCode = GetHash(value);
        var curIndex = hashCode % BucketSize;

        if (_hashArray[curIndex] == null)
            return false;

        var current = _hashArray[curIndex];

        //prevent circling around infinitely
        var hitKey = current!.Value;

        HashSetNode<T> target = null;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
            {
                target = current;
                break;
            }

            curIndex++;

            if (curIndex == BucketSize)
                curIndex = 0;

            current = _hashArray[curIndex];

            if (current != null && EqualityComparer<T>.Default.Equals(current.Value, hitKey))
                break;
        }

        if (target == null)
        {
            return false;
        }

        _hashArray[curIndex] = null;

        //now time to cleanup subsequent broken hash elements due to this emptied cell
        curIndex++;

        if (curIndex == BucketSize)
            curIndex = 0;

        current = _hashArray[curIndex];

        while (current != null)
        {
            _hashArray[curIndex] = null;

            Add(current.Value);
            Count--;

            curIndex++;

            if (curIndex == BucketSize)
                curIndex = 0;

            current = _hashArray[curIndex];
        }

        Count--;

        Shrink();

        return true;
    }
    
    public void Clear()
    {
        Array.Clear(_hashArray);
        Count = 0;
    }
    
    void Grow()
    {
        if (Count <= BucketSize * 0.75) return;

        var orgBucketSize = BucketSize;
        var currentArray = _hashArray;

        _hashArray = new HashSetNode<T>[BucketSize * 2];

        for (var i = 0; i < orgBucketSize; i++)
        {
            var current = currentArray[i];

            if (current != null)
            {
                Add(current.Value);
                Count--;
            }
        }

        Array.Clear(currentArray);
    }


    private void Shrink()
    {
        if (Count <= BucketSize * 0.3 && BucketSize > _initialBucketSize)
        {
            var orgBucketSize = BucketSize;

            var currentArray = _hashArray;

            _hashArray = new HashSetNode<T>[BucketSize / 2];

            for (var i = 0; i < orgBucketSize; i++)
            {
                var current = currentArray[i];
                if (current != null)
                {
                    Add(current.Value);
                    Count--;
                }
            }

            Array.Clear(currentArray);
        }
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var node in _hashArray)
        {
            if (node != null)
                yield return node.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}