namespace AaDS.String;

class LevenshteinDistance
{
    static int IsCharEqual(char a, char b) => a == b ? 0 : 1;

    static int Min(int a, int b, int c) => Math.Min(Math.Min(a, b), c);
    
    static int GetLevenshteinDistance(string one, string two)
    {
        int[,] distanceMatrix = new int[one.Length + 1, two.Length + 1];
        
        for (int i = 0; i < one.Length; i++)
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