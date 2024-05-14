using AaDS.shared;

namespace AaDS.Sortings;

static class RadixSort
{
    public static List<int> Sort(IEnumerable<int> array, SortDirection sortDirection = SortDirection.Ascending)
    {
        var @base = 1;

        List<int> arr = array.ToList();

        if (arr.Count < 2)
            return arr;

        var max = arr.Max(Math.Abs);

        while (max / @base > 0)
        {
            // Создаем корзины для цифр от -9 до 9
            var buckets = new List<int>[19];
            for (int i = 0; i < 19; i++)
                buckets[i] = new();

            for (int i = 0; i < arr.Count; i++)
            {
                var bucketIndex = arr[i] / @base % 10 + 9; // Для отрицательных чисел
                buckets[bucketIndex].Add(arr[i]);
            }

            // Обновляем массив тем, что находится в корзинах
            var orderedBuckets = sortDirection == SortDirection.Ascending ? buckets : buckets.Reverse();

            int index = 0;
            foreach (var bucket in orderedBuckets.Where(x => x.Count > 0))
                foreach (var item in bucket)
                    arr[index++] = item;

            @base *= 10;
        }

        return arr;
    }
}