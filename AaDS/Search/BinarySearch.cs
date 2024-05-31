﻿using System.Collections;

namespace AaDS.Search;

static class BinarySearch<T> where T : IComparable<T>
{
    public static int Search(IList<T> list, T element)
    {
        return Search(list, 0, list.Count - 1, element);
    }

    static int Search(IList<T> list, int left, int right, T element)
    {
        while (left <= right)
        {
            int middle = (left + right) / 2;
            int comparison = element.CompareTo(list[middle]);

            if (comparison < 0)
            {
                right = middle - 1;
            }
            else if (comparison > 0)
            {
                left = middle + 1;
            }
            else
            {
                return middle;
            }
        }

        return -1;
    }

    public static int Search(IList<T> list, T element, int left, int right)
    {
        return Search(list, left, right, element);
    }
}