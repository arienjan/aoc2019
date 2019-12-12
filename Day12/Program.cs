using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
            jupiterMoonSystem.AddMoon(new Moon(-1, 0, 2));
            jupiterMoonSystem.AddMoon(new Moon(2, -10, -7));
            jupiterMoonSystem.AddMoon(new Moon(4, -8, 8));
            jupiterMoonSystem.AddMoon(new Moon(3, 5, -1));

            var jupiterMoonSystemInitial = new JupiterMoonSystem();
            // jupiterMoonSystemInitial.AddMoon(new Moon(1, 4, 4));
            // jupiterMoonSystemInitial.AddMoon(new Moon(-4, -1, 19));
            // jupiterMoonSystemInitial.AddMoon(new Moon(-15, -14, 12));
            // jupiterMoonSystemInitial.AddMoon(new Moon(-17, 1, 10));
            
            jupiterMoonSystemInitial.AddMoon(new Moon(-1, 0, 2));
            jupiterMoonSystemInitial.AddMoon(new Moon(2, -10, -7));
            jupiterMoonSystemInitial.AddMoon(new Moon(4, -8, 8));
            jupiterMoonSystemInitial.AddMoon(new Moon(3, 5, -1));

            var solution2 = Part2Solution(jupiterMoonSystem, jupiterMoonSystemInitial);
            System.Console.WriteLine(solution);
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

        public static int Part2Solution(JupiterMoonSystem jupiterMoonSystem, JupiterMoonSystem initialSystem)
        {
            var steps = 0;
            var initialMoonSet = new HashSet<Moon>(initialSystem.Moons);
            var historyDoesntRepeat = true;
            while (historyDoesntRepeat)
            {
                steps++;
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

                if (!initialSystem.Moons.Except(jupiterMoonSystem.Moons).Any() 
                    && !initialSystem.MoonVelocities.Except(jupiterMoonSystem.MoonVelocities).Any())
                {
                    historyDoesntRepeat = false;
                }
            }

            return steps;
        }
    }

    public class Moon : IEqualityComparer<Moon>
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

        public bool Equals(Moon x, Moon y)
        {
            return x.X == y.X && x.Y == y.Y && x.Z == y.Z;
        }

        public int GetHashCode(Moon obj)
        {
            return obj.GetHashCode();
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
