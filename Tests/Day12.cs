using NUnit.Framework;
using Day12;
using System.Collections.Generic;

namespace Tests
{
    public class Day12
    {
        JupiterMoonSystem jupiterMoonSystem;

        [SetUp]
        public void Setup()
        {
            jupiterMoonSystem = new JupiterMoonSystem();
        }

        [Test]
        public void Day12_0() {
            jupiterMoonSystem.AddMoon(new Moon(-1, 0, 2));
            jupiterMoonSystem.AddMoon(new Moon(2, -10, -7));
            jupiterMoonSystem.AddMoon(new Moon(4, -8, 8));
            jupiterMoonSystem.AddMoon(new Moon(3, 5, -1));

            var solution = Program.Part1Solution(jupiterMoonSystem, 10);
            Assert.AreEqual(179, solution);
        }

        [Test]
        public void Day12_1() {
            jupiterMoonSystem.AddMoon(new Moon(-1, 0, 2));
            jupiterMoonSystem.AddMoon(new Moon(2, -10, -7));
            jupiterMoonSystem.AddMoon(new Moon(4, -8, 8));
            jupiterMoonSystem.AddMoon(new Moon(3, 5, -1));

            var initialSystem = new JupiterMoonSystem();
            
            initialSystem.AddMoon(new Moon(-1, 0, 2));
            initialSystem.AddMoon(new Moon(2, -10, -7));
            initialSystem.AddMoon(new Moon(4, -8, 8));
            initialSystem.AddMoon(new Moon(3, 5, -1));

            var solution = Program.Part2Solution(jupiterMoonSystem, initialSystem);
            Assert.AreEqual(2772, solution);
        }
    }
}