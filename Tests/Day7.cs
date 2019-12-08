using NUnit.Framework;
using Day7;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Day7
    {
        [Test]
        public void TestThrusters1()
        {
            var inputLine = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            var input = inputLine.Split(',').ToList();
            var program = input.Select(int.Parse).ToList();
            var solution = Program.Part1Solution(program);

            Assert.AreEqual(solution, 43210);
        }

        [Test]
        public void TestThrusters2()
        {
            var inputLine = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
            var input = inputLine.Split(',').ToList();
            var program = input.Select(int.Parse).ToList();
            var solution = Program.Part1Solution(program);

            Assert.AreEqual(solution, 54321);
        }
        
        [Test]
        public void TestThrusters3()
        {
            var inputLine = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
            var input = inputLine.Split(',').ToList();
            var program = input.Select(int.Parse).ToList();
            var solution = Program.Part1Solution(program);

            Assert.AreEqual(solution, 65210);
        }

        [Test]
        public void TestThrustersLoop1()
        {
            var inputLine = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            var input = inputLine.Split(',').ToList();
            var program = input.Select(int.Parse).ToList();
            var solution = Program.Part2Solution(program);

            Assert.AreEqual(solution, 139629729);
        }
    }
}