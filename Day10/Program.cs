using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Shared;

// wat kun je doen, maak sowieso extension method op vector voor de lengte, x^2 + y^2
// bereken vanaf elk punt de vector naar elk van de # punten
// als er vectoren zijn met gelijke angle between, kijk welke dichtstebij is
// dan ben je er, eerst dus zaak om alle # punten te bepalen

namespace Day10
{
    public class Program
    {
        static void Main(string[] args)
        {
            var inputLines = InputGetter.ReadInputAsLines(2019, 10).Result;

            var part2Solution = Solution(inputLines);
            System.Console.WriteLine(part2Solution);
        }

        public static Tuple<int, int> Solution(List<string> inputLines, bool onlyPart1 = false)
        {
            var vectorDict = new Dictionary<int, List<Vector2D>>();
            foreach (var meetPunt in GetGridPoints(inputLines))
            {
                if (inputLines[meetPunt.X][meetPunt.Y] == '#')
                {
                    var vectors = new List<Vector2D>();
                    foreach (var asteroidePunt in GetGridPoints(inputLines))
                    {
                        if ((asteroidePunt.X != meetPunt.X || asteroidePunt.Y != meetPunt.Y) && inputLines[asteroidePunt.X][asteroidePunt.Y] == '#')
                        {
                            vectors.Add(new Vector2D(asteroidePunt.X - meetPunt.X, asteroidePunt.Y - meetPunt.Y));
                        }
                    }
                    var numberOfAsertoid = vectors.Distinct().ToList().Count();
                    vectors.Sort();
                    vectorDict[numberOfAsertoid] = vectors;
                }
            }

            var bestPositionVectors = vectorDict[vectorDict.Keys.Max()];

            if (onlyPart1) {
                return new Tuple<int, int>(vectorDict.Keys.Max(), 0);
            }

            // loopen over die vectoren, misschien kan in de sortering ook nog de lengte worden meegenomen (x^2 + y^2), dan kunnen we gewoon over de verschillende angles loopen, steeds een pakken en dan wegblazen
            var uniqueAngles = bestPositionVectors.Select(v => v.Angle).Distinct().ToList();
            Vector2D asteroid200 = new Vector2D(-1, -1);

            var shotAsteroids = 0;
            var iter = 1;
            var keepLooping = true;

            while (keepLooping)
            {
                // System.Console.WriteLine(bestPositionVectors.Count());
                var currentAngle = uniqueAngles[iter % uniqueAngles.Count()];
                if (bestPositionVectors.Any(bpv => bpv.Angle == currentAngle))
                {
                    var closestVector = bestPositionVectors.Where(bpv => bpv.Angle == currentAngle).OrderBy(bpv => bpv.Length).First();
                    bestPositionVectors.Remove(closestVector);
                    shotAsteroids++;
                    System.Console.WriteLine($"{closestVector.realX}, {closestVector.realY}");
                    System.Console.WriteLine($"{closestVector.Angle}, {shotAsteroids}");
                    if (shotAsteroids == 200)
                    {
                        asteroid200 = closestVector;
                        keepLooping = false;
                    }
                }
                iter++;
            }

            System.Console.WriteLine(asteroid200.realX * 100 + asteroid200.realY);

            return new Tuple<int, int>(vectorDict.Keys.Max(), asteroid200.realX * 100 + asteroid200.realY);
        }

        // deze kan je wel vaker gebruiken toch, ook in gbuild grid
        private static IEnumerable<Point> GetGridPoints(List<string> inputLines)
        {
            for (int x = 0; x < inputLines[0].Count(); x++)
            {
                for (int y = 0; y < inputLines.Count(); y++)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}
