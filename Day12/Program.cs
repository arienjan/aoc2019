using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Day12
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Part 1
            var jupiterMoonSystem = new JupiterMoonSystem();
            jupiterMoonSystem.AddMoon(new Moon(1, 4, 4));
            jupiterMoonSystem.AddMoon(new Moon(-4, -1, 19));
            jupiterMoonSystem.AddMoon(new Moon(-15, -14, 12));
            jupiterMoonSystem.AddMoon(new Moon(-17, 1, 10));

            var solution = Part1Solution(jupiterMoonSystem, 1000);
            System.Console.WriteLine(solution);

            // Part 2
            jupiterMoonSystem = new JupiterMoonSystem();
            jupiterMoonSystem.AddMoon(new Moon(1, 4, 4));
            jupiterMoonSystem.AddMoon(new Moon(-4, -1, 19));
            jupiterMoonSystem.AddMoon(new Moon(-15, -14, 12));
            jupiterMoonSystem.AddMoon(new Moon(-17, 1, 10));

            var jupiterMoonSystemInitial = new JupiterMoonSystem();
            jupiterMoonSystemInitial.AddMoon(new Moon(1, 4, 4));
            jupiterMoonSystemInitial.AddMoon(new Moon(-4, -1, 19));
            jupiterMoonSystemInitial.AddMoon(new Moon(-15, -14, 12));
            jupiterMoonSystemInitial.AddMoon(new Moon(-17, 1, 10));

            var solution2 = Part2Solution(jupiterMoonSystem, jupiterMoonSystemInitial);
            System.Console.WriteLine(solution2);
        }

        public static int Part1Solution(JupiterMoonSystem jupiterMoonSystem, int steps)
        {
            for (int t = 0; t < steps; t++)
            {
                foreach (var moon in jupiterMoonSystem.GetMoons())
                {
                    var otherMoons = jupiterMoonSystem.Moons.Where(m => m != moon);
                    foreach (var otherMoon in otherMoons)
                    {
                        moon.V.vX += (otherMoon.X - moon.X) == 0 ? (otherMoon.X - moon.X) : (otherMoon.X - moon.X) / Math.Abs(otherMoon.X - moon.X);
                        moon.V.vY += (otherMoon.Y - moon.Y) == 0 ? (otherMoon.Y - moon.Y) : (otherMoon.Y - moon.Y) / Math.Abs(otherMoon.Y - moon.Y);
                        moon.V.vZ += (otherMoon.Z - moon.Z) == 0 ? (otherMoon.Z - moon.Z) : (otherMoon.Z - moon.Z) / Math.Abs(otherMoon.Z - moon.Z);
                    }
                }

                foreach (var moon in jupiterMoonSystem.GetMoons())
                {
                    moon.Tick();
                }
            }

            return jupiterMoonSystem.getTotalEnergy();
        }

        // theoretisch gezien werkt het....
        public static long Part2Solution(JupiterMoonSystem jupiterMoonSystem, JupiterMoonSystem initialSystem)
        {
            long steps = 0;
            var historyDoesntRepeat = true;
            var moonEqualityComparer = new MoonEqualityComparer();
            var velocityEqualityComparer = new VelocityEqualityComparer();
            while (historyDoesntRepeat)
            {
                steps++;
                foreach (var moon in jupiterMoonSystem.GetMoons())
                {
                    var otherMoons = jupiterMoonSystem.Moons.Where(m => m != moon); //hier kun je ook excepten als sneller;
                    foreach (var otherMoon in otherMoons)
                    {
                        moon.V.vX += (otherMoon.X - moon.X) == 0 ? (otherMoon.X - moon.X) : (otherMoon.X - moon.X) / Math.Abs(otherMoon.X - moon.X);
                        moon.V.vY += (otherMoon.Y - moon.Y) == 0 ? (otherMoon.Y - moon.Y) : (otherMoon.Y - moon.Y) / Math.Abs(otherMoon.Y - moon.Y);
                        moon.V.vZ += (otherMoon.Z - moon.Z) == 0 ? (otherMoon.Z - moon.Z) : (otherMoon.Z - moon.Z) / Math.Abs(otherMoon.Z - moon.Z);
                    }
                }

                foreach (var moon in jupiterMoonSystem.GetMoons())
                {
                    moon.Tick();
                }

                if (!initialSystem.Moons.Except(jupiterMoonSystem.Moons, moonEqualityComparer).Any()
                    && !initialSystem.MoonVelocities.Except(jupiterMoonSystem.MoonVelocities, velocityEqualityComparer).Any())
                {
                    historyDoesntRepeat = false;
                }
            }

            return steps;
        }
    }

    class MoonEqualityComparer : IEqualityComparer<Moon>
    {
        public bool Equals(Moon b1, Moon b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null || b2 == null)
                return false;
            else if (b1.X == b2.X && b1.Y == b2.Y
                                && b1.Z == b2.Z)
                return true;
            else
                return false;
        }

        public int GetHashCode(Moon bx)
        {
            int hCode = bx.X ^ bx.Y ^ bx.Z;
            return hCode.GetHashCode();
        }
    }

    class VelocityEqualityComparer : IEqualityComparer<Velocity>
    {
        public bool Equals(Velocity b1, Velocity b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null || b2 == null)
                return false;
            else if (b1.vX == b2.vX && b1.vY == b2.vY
                                && b1.vZ == b2.vZ)
                return true;
            else
                return false;
        }

        public int GetHashCode(Velocity bx)
        {
            int hCode = bx.vX ^ bx.vY ^ bx.vZ;
            return hCode.GetHashCode();
        }
    }

    public class Moon
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Velocity V { get; set; }

        public Moon(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            V = new Velocity();
        }

        public void Tick()
        {
            X += V.vX;
            Y += V.vY;
            Z += V.vZ;
        }

        public override string ToString()
        {
            return $"Moon has position ({X}, {Y}, {Z})";
        }

        public int GetEnergy()
        {
            var ePot = Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
            var eKin = Math.Abs(V.vX) + Math.Abs(V.vY) + Math.Abs(V.vZ);
            return ePot * eKin;
        }
    }

    public class Velocity
    {
        public int vX { get; set; }
        public int vY { get; set; }
        public int vZ { get; set; }
    }

    public class JupiterMoonSystem
    {
        public List<Moon> Moons { get; set; }

        public List<Velocity> MoonVelocities
        {
            get
            {
                return Moons.Select(m => m.V).ToList();
            }
        }

        public JupiterMoonSystem()
        {
            Moons = new List<Moon>();
        }

        public void AddMoon(Moon moon)
        {
            this.Moons.Add(moon);
        }

        public IEnumerable<Moon> GetMoons()
        {
            foreach (var moon in Moons)
            {
                yield return moon;
            }
        }

        public int getTotalEnergy()
        {
            var energy = 0;
            foreach (var moon in GetMoons())
            {
                energy += moon.GetEnergy();
            }
            return energy;
        }
    }

}
