namespace AaDS.Arrays;

/// <summary>
/// https://leetcode.com/problems/smallest-number-in-infinite-set/?envType=study-plan-v2
/// </summary>
public class SmallestInfiniteSet
{
    private bool[] _isTaken;
    private int _minIndex; // Начинаем с индекса 0 (число 1)

    public SmallestInfiniteSet(int maxValue)
    {
        _isTaken = new bool[maxValue + 1];         
    }

    public int PopSmallest()
    {
        while (_isTaken[_minIndex])
        {
            _minIndex++;
        }

        _isTaken[_minIndex] = true; // Помечаем число как занятое
        return _minIndex + 1;       // Возвращаем число (индекс + 1)
    }

    public void AddBack(int num)
    {
        if (num < 1 || num > _isTaken.Length - 1)
        {
            throw new ArgumentOutOfRangeException(nameof(num), "Number must be between 1 and 1000.");
        }

        int index = num - 1;

        // Возвращаем число только если оно было занято
        if (_isTaken[index])
        {
            _isTaken[index] = false;          // Помечаем число как доступное
            _minIndex = Math.Min(_minIndex, index); // Обновляем минимальный индекс
        }
    }
}