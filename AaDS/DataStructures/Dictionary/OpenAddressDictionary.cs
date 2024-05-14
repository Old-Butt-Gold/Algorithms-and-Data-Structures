using System.Collections;

namespace AaDS.DataStructures.Dictionary;

public class OpenAddressDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    class HashMapNode
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public HashMapNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    readonly int _initialBucketSize = 32;
    HashMapNode?[] _hashArray;

    public OpenAddressDictionary()
    {
        _hashArray = new HashMapNode[_initialBucketSize];
    }

    int GetHash(TKey key) => Math.Abs(key!.GetHashCode());

    int BucketSize => _hashArray.Length;
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;

    public bool ContainsKey(TKey key)
    {
        var hashCode = GetHash(key);
        var index = hashCode % BucketSize;

        if (_hashArray[index] is null)
            return false;

        var current = _hashArray[index];
        var hitKey = current.Key;

        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                return true;

            index++;

            if (index == BucketSize)
                index = 0;

            current = _hashArray[index];

            if (current != null && EqualityComparer<TKey>.Default.Equals(current.Key, hitKey))
                break;
        }
        return false;
    }

    public void Add(TKey key, TValue value)
    {
        Grow();

        var hashCode = GetHash(key);
        var index = hashCode % BucketSize;

        if (_hashArray[index] is null)
        {
            _hashArray[index] = new HashMapNode(key, value);
        }
        else
        {
            var current = _hashArray[index];
            var hitKey = current!.Key;

            while (current != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                    throw new ArgumentException("An item with the same key has already been added.");

                index++;

                if (index == BucketSize)
                    index = 0;

                current = _hashArray[index];

                if (current != null && EqualityComparer<TKey>.Default.Equals(current.Key, hitKey))
                    break;
            }

            _hashArray[index] = new HashMapNode(key, value);
        }

        Count++;
    }

    public bool Remove(TKey key)
    {
        var hashCode = GetHash(key);
        var curIndex = hashCode % BucketSize;

        if (_hashArray[curIndex] == null)
            return false;

        var current = _hashArray[curIndex];
        var hitKey = current!.Key;

        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                break;

            curIndex++;

            if (curIndex == BucketSize)
                curIndex = 0;

            current = _hashArray[curIndex];

            if (current != null && EqualityComparer<TKey>.Default.Equals(current.Key, hitKey))
                break;
        }

        if (current == null)
            return false;

        _hashArray[curIndex] = null;

        curIndex++;

        if (curIndex == BucketSize)
            curIndex = 0;

        current = _hashArray[curIndex];

        while (current != null)
        {
            _hashArray[curIndex] = null;

            Add(current.Key, current.Value);
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

        _hashArray = new HashMapNode[BucketSize * 2];

        for (var i = 0; i < orgBucketSize; i++)
        {
            var current = currentArray[i];

            if (current != null)
            {
                Add(current.Key, current.Value);
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

            _hashArray = new HashMapNode[BucketSize / 2];

            for (var i = 0; i < orgBucketSize; i++)
            {
                var current = currentArray[i];
                if (current != null)
                {
                    Add(current.Key, current.Value);
                    Count--;
                }
            }

            Array.Clear(currentArray);
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (var node in _hashArray)
        {
            if (node != null)
                yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}