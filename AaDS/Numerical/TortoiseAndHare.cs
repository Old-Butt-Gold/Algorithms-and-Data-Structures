namespace AaDS.Numerical;

static class TortoiseAndHare
{
    static int FindAnyRepeatedNumber(int[] arr) {
        int slow = arr[0];
        int fast = arr[0];
        do {
            slow = arr[slow];
            fast = arr[arr[fast]];
        } while(slow != fast);
    
        slow = arr[0];
        while (slow != fast) {
            slow = arr[slow];
            fast = arr[fast];
        }
        return fast;
    }

    //Находит дубликат элемента, в массиве от 1 до n элементов, размер n + 1

    //Алгоритм Floid's Tortoise and Hare
}