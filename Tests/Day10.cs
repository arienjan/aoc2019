using NUnit.Framework;
using Day10;
using System.Collections.Generic;

namespace Tests
{
    public class Day10
    {
        List<string> inputLines;

        [SetUp]
        public void Setup()
        {
            inputLines = new List<string>();
        }

        [Test]
        public void TestDay10_0()
        {
            inputLines.Add(".#..#");
            inputLines.Add(".....");
            inputLines.Add("#####");
            inputLines.Add("....#");
            inputLines.Add("...##");

            var part1Solution = Program.Solution(inputLines, true);
            Assert.AreEqual(8, part1Solution.Item1);
        }

        [Test]
        public void TestDay10_1()
        {
            inputLines.Add("......#.#.");
            inputLines.Add("#..#.#....");
            inputLines.Add("..#######.");
            inputLines.Add(".#.#.###..");
            inputLines.Add(".#..#.....");
            inputLines.Add("..#....#.#");
            inputLines.Add("#..#....#.");
            inputLines.Add(".##.#..###");
            inputLines.Add("##...#..#.");
            inputLines.Add(".#....####");

            var part1Solution = Program.Solution(inputLines, true);
            Assert.AreEqual(33, part1Solution.Item1);
        }

        [Test]
        public void TestDay10_2()
        {
            inputLines.Add(".#..##.###...#######");
            inputLines.Add("##.############..##.");
            inputLines.Add(".#.######.########.#");
            inputLines.Add(".###.#######.####.#.");
            inputLines.Add("#####.##.#.##.###.##");
            inputLines.Add("..#####..#.#########");
            inputLines.Add("####################");
            inputLines.Add("#.####....###.#.#.##");
            inputLines.Add("##.#################");
            inputLines.Add("#####.##.###..####..");
            inputLines.Add("..######..##.#######");
            inputLines.Add("####.##.####...##..#");
            inputLines.Add(".#####..#.######.###");
            inputLines.Add("##...#.##########...");
            inputLines.Add("#.##########.#######");
            inputLines.Add(".####.#.###.###.#.##");
            inputLines.Add("....##.##.###..#####");
            inputLines.Add(".#.#.###########.###");
            inputLines.Add("#.#.#.#####.####.###");
            inputLines.Add("###.##.####.##.#..##");

            var part1Solution = Program.Solution(inputLines);
            Assert.AreEqual(802, part1Solution.Item2);
        }
    }
}