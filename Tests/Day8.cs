using NUnit.Framework;
using Day8;

namespace Tests
{
    public class Day8
    {
        [Test]
        public void TestDay8_1()
        {
            var inputLine = "123456789012";
            var solution = Program.Part1Solution(inputLine, 3, 2);

            Assert.AreEqual(solution, 1);
        }
    }
}