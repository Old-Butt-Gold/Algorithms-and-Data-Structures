namespace AaDS.Greedy;

public static class LemonadeChange
{
    /// <summary>
    /// At a lemonade stand, each lemonade costs $5. Customers are standing in a queue to buy from you and order one at a time (in the order specified by bills). Each customer will only buy one lemonade and pay with either a $5, $10, or $20 bill. You must provide the correct change to each customer so that the net transaction is that the customer pays $5.
    /// Note that you do not have any change in hand at first.
    /// </summary>
    /// <param name="bills"></param>
    /// <returns></returns>
    public static bool LemonadeChangeI(int[] bills)
    {
        int five = 0;
        int ten = 0;

        foreach (var bill in bills)
        {
            switch (bill)
            {
                case 5:
                    five++;
                    break;
                case 10:
                    five--;
                    ten++;
                    break;
                case 20 when ten > 0:
                    five--;
                    ten--;
                    break;
                case 20:
                    five -= 3;
                    break;
            }

            if (five < 0)
                return false;
        }

        return true;
    }
}