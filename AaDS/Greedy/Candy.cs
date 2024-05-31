namespace AaDS.Greedy;

public static class Candy
{
    /// <summary>
    /// There are n children standing in a line. Each child is assigned a rating value given in the integer array ratings.
    /// You are giving candies to these children subjected to the following requirements:
    /// Each child must have at least one candy.
    /// Children with a higher rating get more candies than their neighbors.
    /// Return the minimum number of candies you need to have to distribute the candies to the children.
    /// </summary>
    /// <param name="ratings"></param>
    /// <returns></returns>
    public static int LeastCandy(int[] ratings)
    {
        int[] candies = new int[ratings.Length];
        candies[0] = 1;

        for (int i = 1; i < ratings.Length; i++)
        {
            candies[i] = ratings[i] > ratings[i - 1] 
                ? candies[i - 1] + 1 
                : 1;
        }

        for (int i = ratings.Length - 2; i > -1; i--)
        {
            //ratings is higher, and more candies than neighbor => get candies neighbor + 1
            if (ratings[i] > ratings[i + 1] && candies[i] <= candies[i + 1])
            {
                candies[i] = candies[i + 1] + 1;
            }
        }

        return candies.Sum();
    }
}