using System;
using Shared;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputLines = InputGetter.ReadInputAsString(2019, 4).Result;

            var codes = inputLines.Split('-').Select(int.Parse).ToList();

            var part1Solution = Part1Solution(codes);
            System.Console.WriteLine(part1Solution);

            var part2Solution = Part2Solution(codes);
            System.Console.WriteLine(part2Solution);
        }

        private static int Part1Solution(List<int> codes)
        {
            var amountOfPasswords = 0;

            for (int i = codes[0] + 1; i < codes[1]; i++)
            {

                if (CheckPassword(i))
                {
                    amountOfPasswords++;
                }
            }
            return amountOfPasswords;
        }

        private static int Part2Solution(List<int> codes)
        {
            var amountOfPasswords = 0;

            for (int i = codes[0] + 1; i < codes[1]; i++)
            {

                if (CheckPassword2(i))
                {
                    amountOfPasswords++;
                }
            }
            return amountOfPasswords;
        }

        private static bool CheckPassword(int password)
        {
            var bla = Regex.Matches(password.ToString(), "(11|22|33|44|55|66|77|88|99|00)").Count();
            if (bla > 0)
            {
                // dit kan vast een stuk slimmer
                return CheckNumberSequence(password);
            }
            return false;
        }

        private static bool CheckPassword2(int password)
        {
            var matches = Regex.Matches(password.ToString(), "(11+|22+|33+|44+|55+|66+|77+|88+|99+|00+)").ToList();
            var bla = matches.Count();
            if (bla > 0)
            {
                    if (matches.Select(match => match.Length).Any(match => match == 2))
                    {
                        return CheckNumberSequence(password);
                    }
                    else
                    {
                        return false;
                    }
            }
            return false;
        }

        private static bool CheckNumberSequence(int password)
        {
            var numberCharArray = password.ToString().ToCharArray();
            for (int j = 0; j < 5; j++)
            {
                if (numberCharArray[j] > numberCharArray[j + 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
