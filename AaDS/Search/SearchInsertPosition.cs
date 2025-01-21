namespace AaDS.Search;

public class SearchInsertPosition<T> where T : IComparable<T>
{
    /// <summary>
    /// https://leetcode.com/problems/search-insert-position/description/
    /// </summary>
    /// <param name="array"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static int BinarySearchInsertIndex(IList<T> array, T element)
    {
        int left = 0;
        int right = array.Count;
 
        while (left < right)
        {
            int mid = left + (right - left) / 2; // to avoid overflow of int
 
            if (array[mid].CompareTo(element) < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }
 
        return left;
    }
}