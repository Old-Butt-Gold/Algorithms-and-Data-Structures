using System.Collections;

namespace AaDS.DataStructures.Set;

class BloomFilter<T>
{
    readonly BitArray _filter;
    readonly int _numberOfHashFunctions;
    
    public BloomFilter(int expectedSize, double falsePositiveProbability, int numberOfHashFunctions = 2)
    {
        if (expectedSize <= numberOfHashFunctions)
            throw new ArgumentException("Expected size cannot be less than or equal to numberOfHashFunctions.");

        _numberOfHashFunctions = numberOfHashFunctions;
        int size = CalculateFilterSize(expectedSize, falsePositiveProbability);
        _filter = new BitArray(size);
    }

    public void AddKey(T key)
    {
        foreach (var hash in GetHashes(key)) 
            _filter[hash % _filter.Length] = true;
    }

    public bool KeyExists(T key)
    {
        foreach (var hash in GetHashes(key))
            if (!_filter[hash % _filter.Length])
                return false;

        return true;
    }

    IEnumerable<int> GetHashes(T key)
    {
        for (int i = 1; i <= _numberOfHashFunctions; i++)
        {
            var obj = new { Key = key, InitialValue = i };
            yield return Math.Abs(obj.GetHashCode());
        }
    }
    
    int CalculateFilterSize(int expectedSize, double falsePositiveProbability)
    {
        int size = (int)Math.Ceiling((expectedSize * Math.Log(falsePositiveProbability)) / Math.Log(1.0 / (Math.Pow(2.0, Math.Log(2.0)))));
        return size;
    }

}