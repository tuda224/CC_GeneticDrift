using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CC_GeneticDrift
{
    public class Pair
    {
        public int First { get; set; }
        public int Second { get; set; }
    }

    public class Level1
    {
        private static List<Pair> pairs;

        public static void Run()
        {
            var input = ReadInput();
            var splittedInput = input.Split(' ');
            var permutationLength = int.Parse(splittedInput[0]);
            var integerInput = splittedInput.Skip(1).Select(s => int.Parse(s)).ToArray();
            pairs = new List<Pair>();

            FindPairs(integerInput);
            var orderdPairs = pairs.OrderBy(p => p.First);

            Console.Write($"{orderdPairs.Count()} ");
            foreach (var pair in orderdPairs)
            {
                Console.Write($"{pair.First} {pair.Second} ");
            }
            Console.WriteLine();
        }

        private static void FindPairs(int[] numbers)
        {
            // pick first number to start comparing
            for (int i = 0; i < numbers.Length; i++)
            {
                // iterate over number rest to find match
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    // skip numbers if both are positive/negative
                    if (numbers[i] >= 0 == numbers[j] >= 0)
                        continue;

                    // check distance
                    if (Math.Abs(Math.Abs(numbers[i]) - Math.Abs(numbers[j])) == 1)
                    {
                        pairs.Add(new Pair
                        {
                            First = numbers[i],
                            Second = numbers[j]
                        });
                    }
                }
            }
        }

        private static string ReadInput()
        {
            Console.WriteLine("Provide path to input file:");
            var filePath = Console.ReadLine();
            var input = File.ReadAllText(filePath);
            return input;
        }
    }
}