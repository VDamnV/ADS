using System;

namespace CombinatoricsLab
{
    class Program
    {
        static void Main()
        {
            Console.Write("Enter the number of teams (n): ");
            int teamsCount = int.Parse(Console.ReadLine());

            Console.Write("Enter the number of winning places (k): ");
            int placesCount = int.Parse(Console.ReadLine());

            long winningCombinations = CalculateArrangement(teamsCount, placesCount);
            Console.WriteLine($"Winning combinations: {winningCombinations}\n");

            Console.Write("Enter the number of available letters (n): ");
            int lettersCount = int.Parse(Console.ReadLine());

            Console.Write("Enter the word length (k): ");
            int wordLength = int.Parse(Console.ReadLine());

            long wordCombinations = (long)Math.Pow(lettersCount, wordLength);
            Console.WriteLine($"Word combinations: {wordCombinations}");
        }

        static long CalculateArrangement(int n, int k)
        {
            if (k > n || n < 0 || k < 0) return 0;

            long result = 1;
            for (int i = 0; i < k; i++)
            {
                result *= (n - i);
            }
            return result;
        }
    }
}