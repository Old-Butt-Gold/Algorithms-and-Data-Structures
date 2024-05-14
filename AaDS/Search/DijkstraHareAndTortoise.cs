//массив из N + 1 целых чисел, который содержит элементы в диапазоне [1, N]
namespace AaDS.Search;

static class DijkstraHareAndTortoise
{
    public static int FindAnyRepeatedNumber(int[] arr)
    {
        int slow = arr[0];
        int fast = arr[0];
        do
        {
            slow = arr[slow];
            fast = arr[arr[fast]];
        } while (slow != fast);

        slow = arr[0];
        while (slow != fast)
        {
            slow = arr[slow];
            fast = arr[fast];
        }
        return fast;
    }
}