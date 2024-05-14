namespace AaDS.Numerical;

public static class SieveOfEratosthene
{
    public static List<int> GeneratePrimes(int n)
    {
        if (n < 2)
            throw new ArgumentException("Input should be at least 2", nameof(n));

        bool[] isPrime = new bool[n + 1];
        
        List<int> primes = new();

        for (int i = 2; i < isPrime.Length; i++)
            isPrime[i] = true;

        for (int i = 2; i * i < isPrime.Length; i++)
            if (isPrime[i])
                for (int j = i * i; j < isPrime.Length; j += i)
                    isPrime[j] = false;

        
        for (int i = 2; i < isPrime.Length; i++)
            if (isPrime[i])
                primes.Add(i);

        return primes;
    }
}