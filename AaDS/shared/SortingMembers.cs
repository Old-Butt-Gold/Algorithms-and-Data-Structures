namespace AaDS.shared;

enum SortDirection
{
    Ascending,
    Descending
}

class CustomComparer<T> : IComparer<T>
{
    readonly SortDirection _sortDirection;
    readonly IComparer<T> _defaultComparer;

    public CustomComparer(SortDirection sortDirection, IComparer<T> defaultComparer)
    {
        _sortDirection = sortDirection;
        _defaultComparer = defaultComparer;
    }

    public int Compare(T x, T y)
    {
        int result = _defaultComparer.Compare(x, y);
        return _sortDirection == SortDirection.Ascending ? result : -result;
    }
}