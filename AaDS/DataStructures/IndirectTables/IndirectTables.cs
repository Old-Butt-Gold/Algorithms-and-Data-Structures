namespace AaDS.DataStructures.IndirectTables;

public class IndirectTable<T>(int size)
{
    T[] _data = new T[size];
    int[] _indexTable = new int[size];

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _indexTable.Length)
                throw new IndexOutOfRangeException("Index is out of range");
            return _data[_indexTable[index]];
        }
        set
        {
            if (index < 0 || index >= _indexTable.Length)
                throw new IndexOutOfRangeException("Index is out of range");
                
            _data[_indexTable[index]] = value;
            _indexTable[index] = index;
        }
    }
}