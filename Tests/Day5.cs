using NUnit.Framework;
using Day5;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Day5
    {
        List<int> instructions;

        [SetUp]
        public void Setup() {
            var inputString = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
            var input = inputString.Split(',').ToList();
            instructions = input.Select(int.Parse).ToList();
        }

        [Test]
        public void TestPart1()
        {
            var solution = Program.Part2Solution(instructions, 7);

            Assert.AreEqual(solution, 999);
        }
        
        [Test]
        public void TestPart2()
        {
            var solution = Program.Part2Solution(instructions, 8);

            Assert.AreEqual(solution, 1000);
        }
        
        [Test]
        public void TestPart3()
        {
            var solution = Program.Part2Solution(instructions, 9);

            Assert.AreEqual(solution, 1001);
        }

        

        [Test]
        public void TestPart4()
        {
            var solution = Program.Part2Solution(instructions, 6);

            Assert.AreEqual(solution, 999);
        }

        

        [Test]
        public void TestPart5()
        {
            var solution = Program.Part2Solution(instructions, 10);

            Assert.AreEqual(solution, 1001);
        }
    }
}