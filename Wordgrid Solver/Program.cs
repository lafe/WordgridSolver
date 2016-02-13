using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordgridSolver
{
    class Program
    {
        private static void Main(string[] args)
        {
            var fileLoader = new FileLoader();
            //File should contain a word on each line. Lines starting with # will be ignored (e.g. comment lines).
            //Examples for such dictionaries can be found at http://www.winedt.org/Dict/
            var dictionary = fileLoader.LoadDictionary("US.dic");

            var input = GetInput();

            while (!string.IsNullOrWhiteSpace(input))
            {
                var computer = new WordComputation(input, dictionary);
                var words = computer.GetWords();
                var counter = 0;
                foreach (var word in words)
                {
                    counter++;
                    Console.WriteLine("{1:D3}. {0}", word, counter);
                }
                input = GetInput();
            }
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        private static string GetInput()
        {
            Console.WriteLine("Please type the available characters (from left to right, top to bottom (leave empty to quit):");
            var input = Console.ReadLine();
            return input.ToUpperInvariant();
        }
    }
}
