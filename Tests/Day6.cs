using NUnit.Framework;
using Day6;
using System.Collections.Generic;

namespace Tests
{
    public class Day6
    {
        List<string> inputLines;

        [Test]
        public void TestPart1()
        {
            inputLines = new List<string>() { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L"};
            var solution = Program.Part1Solution(inputLines);

            Assert.AreEqual(solution, 42);
        }

        [Test]
        public void TestPart2()
        {
            inputLines = new List<string>() { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L", "K)YOU", "I)SAN" };
            var solution = Program.Part2Solution(inputLines);

            Assert.AreEqual(solution, 4);
        }
    }
}