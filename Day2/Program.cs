using System;
using Shared;
using System.Linq;
using System.Collections.Generic;

namespace Day2
{
    public class Program
    {
        static void Main(string[] args)
        {
            // InputGetter.ReadInputAsString(2019, 2).Wait();
            var inputLine = InputGetter.ReadInputAsString(2019, 2).Result;
            var input = inputLine.Split(',').ToList();
            var program = input.Select(int.Parse).ToList();

            // arrange part 1
            var part1Input = program.Select(ding => ding).ToList();

            part1Input[1] = 12;
            part1Input[2] = 2;

            var part1Solution = Part1Solution(part1Input);
            System.Console.WriteLine(part1Solution);


            // part 2
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    var part2Input = program.Select(ding => ding).ToList();;
                    part2Input[1] = i;
                    part2Input[2] = j;
                    var part2Solution = Part1Solution(part2Input);

                    // System.Console.WriteLine(part2Solution);
                    if (part2Solution == 19690720)
                    {
                        System.Console.WriteLine($"{i} {j}");
                        var part2SolutionOutput = ((100 * i) + j).ToString();
                        System.Console.WriteLine(part2SolutionOutput);
                    }
                }
            }
        }

        public static int Part1Solution(List<int> input)
        {
            var maxLength = input.Count();
            for (int i = 0; i < maxLength; i += 4)
            {
                if (i + 4 >= maxLength)
                {
                    break;
                }
                var opcode = input[i];
                var position1 = input[i + 1];
                var position2 = input[i + 2];
                var position3 = input[i + 3];

                if (position1 >= maxLength || position2 >= maxLength || position3 >= maxLength)
                {
                    break;
                }

                if (opcode == 1)
                {
                    input[position3] = input[position1] + input[position2];
                }
                else if (opcode == 2)
                {
                    input[position3] = input[position1] * input[position2];
                }
                else if (opcode == 99)
                {
                    break;
                }
                else
                {
                    System.Console.WriteLine("Shit kapot");
                }
            }

            return input[0];
        }
    }
}
