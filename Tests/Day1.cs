using NUnit.Framework;
using Day1;
using System.Collections.Generic;

namespace Tests
{
    public class Day1
    {
        [Test]
        public void TestPart1()
        {
            var solution = Program.Day1Solution(new List<string> () { "12", "14", "1969", "100756" });

            Assert.AreEqual(solution, 34241);
        }


        [Test]
        public void TestPart2()
        {
            var solution = Program.CalculatoreTotalFuelForModule(100756);

            Assert.AreEqual(solution, 50346);
        }
    }
}