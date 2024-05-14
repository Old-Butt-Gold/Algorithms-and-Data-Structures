using System.Numerics;

namespace AaDS.Numerical;

static class MillerRabin
{
    public static bool IsPrime(BigInteger number, int tests)
    {
        if (number <= 1)
            return false;

        if (number == 2 || number == 3)
            return true;

        if (number.IsEven)
            return false;

        int k = Math.Min(tests, 10);

        Random random = new Random();
        for (int i = 0; i < k; i++)
        {
            BigInteger witness = GenerateRandomWitness(2, number - 2, random);
            BigInteger result = BigInteger.ModPow(witness, number - 1, number);

            if (result != 1)
                return false;
        }

        return true;
    }

    static BigInteger GenerateRandomWitness(int minValue, BigInteger maxValue, Random random)
    {
        byte[] bytes = maxValue.ToByteArray();
        BigInteger randomWitness;
        do
        {
            random.NextBytes(bytes);
            bytes[^1] &= 0x7F; // Ensure the number is positive
            randomWitness = new BigInteger(bytes);
        } while (randomWitness >= maxValue || randomWitness < minValue);

        return randomWitness;
    }
}