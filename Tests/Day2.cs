using NUnit.Framework;
using Day2;
using System.Collections.Generic;

namespace Tests
{
    public class Day2
    {
        [Test]
        public void Part1Test1()
        {
            var solution = Program.Part1Solution(new List<int> () { 1, 0, 0, 0, 99 });

            Assert.AreEqual(solution, 2);
        }

        [Test]
        public void Part1Test2()
        {
            var solution = Program.Part1Solution(new List<int> () { 2, 3, 0, 3, 99 });

            Assert.AreEqual(solution, 2);
        }
        
        [Test]
        public void Part1Test3()
        {
            var solution = Program.Part1Solution(new List<int> () { 2, 4, 4, 5, 99, 0 });

            Assert.AreEqual(solution, 2);
        }
        
        [Test]
        public void Part1Test4()
        {
            var solution = Program.Part1Solution(new List<int> () { 1, 1, 1, 4, 99, 5, 6, 0, 99 });

            Assert.AreEqual(solution, 30);
        }
    }
}