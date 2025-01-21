namespace AaDS.DynamicProgramming.MultiDimensionalDP;

class LevenshteinDistance
{
    static int IsCharEqual(char a, char b) => a == b ? 0 : 1;

    static int Min(int a, int b, int c) => Math.Min(Math.Min(a, b), c);

    
    public static int GetDistanceRecursive(string one, string two)
    {
        return Distance(one.Length, two.Length);

        int Distance(int i, int j)
        {
            Console.WriteLine($"{i}:{j}");
            if (i is 0) return j;
            if (j is 0) return i;

            int insert = Distance(i, j - 1) + 1;
            int delete = Distance(i - 1, j) + 1;
            int sub = Distance(i - 1, j - 1) + IsCharEqual(one[i - 1], two[j - 1]);
            return Min(insert, sub, delete);
        }
    }
    
    public static int GetLevenshteinDistance(string one, string two)
    {
        int[,] distanceMatrix = new int[one.Length + 1, two.Length + 1];
        
        for (int i = 0; i <= one.Length; i++)
            distanceMatrix[i, 0] = i;

        for (int i = 0; i <= two.Length; i++)
            distanceMatrix[0, i] = i;

        for (int i = 1; i < distanceMatrix.GetLength(0); i++)
        for (int j = 1; j < distanceMatrix.GetLength(1); j++)
        {
            int diff = IsCharEqual(one[i - 1], two[j - 1]);
            distanceMatrix[i, j] = Min(
                distanceMatrix[i - 1, j] + 1,     // удаление
                distanceMatrix[i, j - 1] + 1,     // вставка
                distanceMatrix[i - 1, j - 1] + diff  // замена
            );
        }
        return distanceMatrix[one.Length, two.Length];
    }
}