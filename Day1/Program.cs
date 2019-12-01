using System;
using System.Collections.Generic;
using Shared;

namespace Day1
{
    public class Program
    {
        static void Main()
        {
            InputGetter.ReadInputAsString(2019, 1).Wait();
            var inputLines = InputGetter.ReadInputAsLines(2019, 1).Result;

            // Part 1
            var day1Solution = Day1Solution(inputLines);
            Console.WriteLine(day1Solution);

            // Part 2
            var day2solution = Day2Solution(inputLines);
            Console.WriteLine(day2solution);
        }

        public static int Day1Solution(List<string> inputLines)
        {
            var totalMass = 0;
            foreach (var bla in inputLines)
            {
                totalMass += CalculateFeulForModule(int.Parse(bla));
            }

            return totalMass;
        }

        public static int Day2Solution(List<string> inputLines)
        {
            var totalMass = 0;
            foreach (var bla in inputLines)
            {
                totalMass += CalculatoreTotalFuelForModule(int.Parse(bla));
            }

            return totalMass;
        }

        public static int CalculatoreTotalFuelForModule(int fuelRequirement)
        {
            var totalMass2 = 0;
            while (fuelRequirement > 0)
            {
                fuelRequirement = CalculateFeulForModule(fuelRequirement);
                fuelRequirement = fuelRequirement > -1 ? fuelRequirement : 0;
                totalMass2 += fuelRequirement;
            }

            return totalMass2;
        }

        private static int CalculateFeulForModule(int mass)
        {
            var fuel = mass / 3;
            return fuel - 2;
        }
    }
}
