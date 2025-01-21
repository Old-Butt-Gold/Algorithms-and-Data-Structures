namespace AaDS.PrefixSum;

public static class MinNumberOfMoves
{
    /// <summary>
    /// https://leetcode.com/problems/minimum-number-of-operations-to-move-all-balls-to-each-box/description/?envType=daily-question
    /// </summary>
    /// <param name="boxes"></param>
    /// <returns></returns>
    public static int[] MinOperations(string boxes) {
        int[] answer = new int[boxes.Length];

        int ballsToLeft = 0, movesToLeft = 0;
        int ballsToRight = 0, movesToRight = 0;

        for (int i = 0; i < boxes.Length; i++) {
            answer[i] += movesToLeft;
            ballsToLeft += boxes[i] - '0';
            movesToLeft += ballsToLeft;

            int j = boxes.Length - 1 - i;
            answer[j] += movesToRight;
            ballsToRight += boxes[j] - '0';
            movesToRight += ballsToRight;
        }

        return answer;
    }
}