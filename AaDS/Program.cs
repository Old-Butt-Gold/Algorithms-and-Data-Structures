using AaDS.Combinatorics;

Console.WriteLine();

var t = Permutation.Generate([1,2, 3, 4], 2);
foreach (var item in t)
{
    Console.WriteLine(string.Join(" ", item));
}
Console.WriteLine(t.Count);