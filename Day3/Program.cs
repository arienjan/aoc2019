using System;
using Shared;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Day3
{
    public class Program
    {
        static void Main(string[] args)
        {
            var wire1Instructions = File.ReadAllText("wire1.txt");
            var wire2Instructions = File.ReadAllText("wire2.txt");

            var solution = Part1Solution(wire1Instructions, wire2Instructions);
            System.Console.WriteLine("part1 solution: {0}", solution.Item1);
            System.Console.WriteLine("part2 solution: {0}", solution.Item2);
        }

        public static Tuple<int, int> Part1Solution(string wire1Instructions, string wire2Instructions)
        {
            var startingPoint = new Point(0, 0, 0);
            var wire1Points = new List<Point>();
            var wire2Points = new List<Point>();

            // Do instructions
            FindPoints(ref wire1Points, wire1Instructions);
            FindPoints(ref wire2Points, wire2Instructions);

            // find intersections
            var intersections = new List<Point>();
            var intersectionStepsCount = new List<int>();

            foreach (var point1 in wire1Points)
            {
                foreach (var point2 in wire2Points)
                {
                    if (point1.Equals(point2))
                    {
                        intersections.Add(point1);
                        intersectionStepsCount.Add(point1.StepsTaken + point2.StepsTaken);
                    }
                }
            }

            var minDistance = intersections.Select(intersection => Math.Abs(intersection.X) + Math.Abs(intersection.Y)).Min();
            System.Console.WriteLine("minsteps {0}", intersectionStepsCount.Min());
            return new Tuple<int, int>(minDistance, intersectionStepsCount.Min());
        }

        public static void FindPoints(ref List<Point> wirePoints, string wireInstructions)
        {
            var currentX = 0;
            var currentY = 0;
            var stepsTaken = 0;
            foreach (var instruction in wireInstructions.Split(','))
            {
                var direction = instruction.Substring(0, 1);
                var distance = int.Parse(instruction.Substring(1));

                switch (direction)
                {
                    case "U":
                        for (int i = 1; i < distance + 1; i++) {
                            currentY += 1;
                            stepsTaken += 1;
                            wirePoints.Add(new Point(currentX, currentY, stepsTaken));
                        }
                        break;
                    case "D":
                        
                        for (int i = 1; i < distance + 1; i++) {
                            currentY -= 1;
                            stepsTaken += 1;
                            wirePoints.Add(new Point(currentX, currentY, stepsTaken));
                        }
                        break;
                    case "L":
                        
                        for (int i = 1; i < distance + 1; i++) {
                            currentX -= 1;
                            stepsTaken += 1;
                            wirePoints.Add(new Point(currentX, currentY, stepsTaken));
                        }
                        break;
                    case "R":
                        
                        for (int i = 1; i < distance + 1; i++) {
                            currentX += 1;
                            stepsTaken += 1;
                            wirePoints.Add(new Point(currentX, currentY, stepsTaken));
                        }
                        break;
                    default:
                        Console.WriteLine("Faulty Instruction");
                        break;
                }
            }
        }

        public static void AddPointsForDistance(ref List<Point> wirePoints, ref int currentX, ref int currentY, int distance, int direction) {
            System.Console.WriteLine("nog ff ipmlementeren");
        }

        public struct Point : IEquatable<Point>
        {
            public int X { get; set; }
            public int Y { get; set; }

            public int StepsTaken {get; set;}

            public Point(int x, int y, int stepsTaken)
            {
                X = x;
                Y = y;
                StepsTaken = stepsTaken;
            }

            public bool Equals(Point point)
            {
                return (X == point.X && Y == point.Y);
            }
        }
    }
}
