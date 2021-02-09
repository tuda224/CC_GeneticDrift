using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CC_GeneticDrift
{
    public class Level3
    {
        public static void Run()
        {
            var input = ReadInput();
            var splittedInput = input.Split();
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