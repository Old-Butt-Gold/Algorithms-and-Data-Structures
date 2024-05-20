namespace AaDS.Search;

static class BinarySearch<T> where T : IComparable<T>
{
    public static int Search(IEnumerable<T> input, T element)
    {
        var list = input.ToList();
        return Search(list, 0, list.Count - 1, element);
    }

    static int Search(List<T> input, int left, int right, T element)
    {
        if (left <= right)
        {
            int middle = (left + right) / 2;
            
            int comparison = element.CompareTo(input[middle]);
            if (comparison < 0)
                return Search(input, left, middle - 1, element);
            if (comparison > 0)
                return Search(input, middle + 1, right, element);
            if (comparison == 0)
                return middle;
        }

        return -1;
    }

    public static int Search(IEnumerable<T> input, T element, int left, int right)
    {
        var list = input.ToList();
        return Search(list, left, right, element);
    }
}