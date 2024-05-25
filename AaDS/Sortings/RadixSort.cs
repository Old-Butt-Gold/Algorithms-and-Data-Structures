using AaDS.shared;

namespace AaDS.Sortings;

static class RadixSort
{
    public static void Sort(IList<int> array, SortDirection sortDirection = SortDirection.Ascending) =>
        RadixSortIntegers(array, sortDirection);

    static void RadixSortIntegers(IList<int> array, SortDirection sortDirection)
    {
        var exponent = 1;

        if (array.Count < 2)
            return;

        var max = array.Max(Math.Abs);

        while (max / exponent > 0)
        {
            // Создаем корзины для цифр от -9 до 9
            var buckets = new List<int>[19];
            for (int i = 0; i < 19; i++)
            {
                buckets[i] = [];
            }

            for (int i = 0; i < array.Count; i++)
            {
                var bucketIndex = array[i] / exponent % 10 + 9; // Для отрицательных чисел
                buckets[bucketIndex].Add(array[i]);
            }

            // Обновляем массив тем, что находится в корзинах
            var orderedBuckets = sortDirection == SortDirection.Ascending ? buckets : buckets.Reverse();

            int index = sortDirection == SortDirection.Ascending ? 0 : buckets.Length - 1;
            foreach (var bucket in orderedBuckets.Where(x => x.Count > 0))
            {
                foreach (var item in bucket)
                {
                    array[index++] = item;
                }
            }

            exponent *= 10;
        }
    }

    public static void Sort(IList<string> array, SortDirection sortDirection = SortDirection.Ascending) =>
        RadixSortStrings(array, sortDirection);

    static void RadixSortStrings(IList<string> array, SortDirection sortDirection)
    {
        if (array.Count < 2)
            return;
        
        // Найдем максимальную длину строки
        int maxLength = array.Max(str => str.Length);
        var maxChar = array.SelectMany(str => str).Max() + 1;

        // Сортируем с конца строки к началу
            
        for (int digit = maxLength - 1; digit > -1; digit--)
        {
            var buckets = new List<string>[maxChar];
            for (int i = 0; i < maxChar; i++)
            {
                buckets[i] = [];
            }

            foreach (var str in array)
            {
                int bucketIndex = digit < str.Length ? str[digit] : 0; // Используем 0 для отсутствующих символов
                buckets[bucketIndex].Add(str);
            }

            var orderedBuckets = sortDirection == SortDirection.Ascending ? buckets : buckets.Reverse();

            int index = 0;
            foreach (var bucket in orderedBuckets.Where(x => x.Count > 0))
            {
                foreach (var item in bucket)
                {
                    array[index++] = item;
                }
            }
        }
    }
}