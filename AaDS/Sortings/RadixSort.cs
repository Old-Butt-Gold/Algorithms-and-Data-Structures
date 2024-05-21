using AaDS.shared;

namespace AaDS.Sortings;

static class RadixSort
{
    public static void Sort(IList<int> array, SortDirection sortDirection = SortDirection.Ascending)
    {
        var @base = 1;

        if (array.Count < 2)
            return;

        var max = array.Max(Math.Abs);

        while (max / @base > 0)
        {
            // Создаем корзины для цифр от -9 до 9
            var buckets = new List<int>[19];
            for (int i = 0; i < 19; i++)
                buckets[i] = new();

            for (int i = 0; i < array.Count; i++)
            {
                var bucketIndex = array[i] / @base % 10 + 9; // Для отрицательных чисел
                buckets[bucketIndex].Add(array[i]);
            }

            // Обновляем массив тем, что находится в корзинах
            var orderedBuckets = sortDirection == SortDirection.Ascending ? buckets : buckets.Reverse();

            int index = 0;
            foreach (var bucket in orderedBuckets.Where(x => x.Count > 0))
                foreach (var item in bucket)
                    array[index++] = item;

            @base *= 10;
        }
    }
}