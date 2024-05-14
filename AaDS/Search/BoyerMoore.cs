// Поиск элемента, встречающегося в последовательности более чем половину раз
// (или для определения того, что такого элемента нет)
namespace AaDS.Search;

public class BoyerMoore<T> where T : IComparable<T>
{
    public static T FindMajority(IEnumerable<T> input)
    {
        var candidate = FindMajorityCandidate(input);
        return Verify(input, input.Count(), candidate) ? candidate : default;
    }

    static T FindMajorityCandidate(IEnumerable<T> input)
    {
        var count = 1;
        var candidate = input.First();

        foreach (var element in input.Skip(1)) //перебор со 2 элемента
        {
            if (candidate.Equals(element))
                count++;
            else
                count--;

            if (count == 0)
            {
                candidate = element;
                count = 1;
            }
        }

        return candidate;
    }

    // Проверка, действительно ли кандидат является большинством
    static bool Verify(IEnumerable<T> input, int size, T candidate) => input.Count(x => x.Equals(candidate)) > size / 2;
}