using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CC_GeneticDrift
{
    public class Level3
    {
        private static List<Pair> pairs;
        private static List<InversionGroup> inversions;

        public static void Run()
        {
            var input = ReadInput();
            var splittedInput = input.Split(' ');

            var permutationLength = int.Parse(splittedInput[0]);
            var integerInput = splittedInput.Skip(1).Take(permutationLength).Select(s => int.Parse(s)).ToArray();
            var inversionInput = splittedInput.Skip(1 + permutationLength).Select(s => int.Parse(s)).ToArray();

            pairs = new List<Pair>();
            inversions = new List<InversionGroup>();
            for (int i = 0; i < inversionInput.Length; i++)
            {
                inversions.Add(new InversionGroup
                {
                    Xi = inversionInput[i],
                    I = inversionInput[++i],
                    Xj = inversionInput[++i],
                    J = inversionInput[++i]
                });
            }

            var result = BuildInversion(integerInput);
            FindPairs(result);

            Console.WriteLine(pairs.Count);
        }

        private static int[] BuildInversion(int[] numbers)
        {
            var inversionResult = new int[numbers.Length];

            // foreach inversion
            foreach (var inversion in inversions)
            {
                var positiveOrder = inversion.Xi + inversion.Xj == 1;
                // fill result until first index is reached
                var i = 0;
                while (i < inversion.I)
                {
                    inversionResult[i] = numbers[i];
                    i++;
                }

                int j = 0;
                if (!positiveOrder)
                {
                    inversionResult[i] = numbers[i];
                    i++;
                    while (i <= inversion.J)
                    {
                        inversionResult[i] = numbers[inversion.J - j] * -1;
                        i++;
                        j++;
                    }
                }
                else
                {
                    j++;
                    while (i < inversion.J)
                    {
                        inversionResult[i] = numbers[inversion.J - j] * -1;
                        i++;
                        j++;
                    }
                }

                while (i < numbers.Length)
                {
                    inversionResult[i] = numbers[i];
                    i++;
                }
            }

            return inversionResult;
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