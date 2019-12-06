using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    public class Program
    {
        static void Main(string[] args)
        {
            var inputLines = InputGetter.ReadInputAsLines(2019, 6).Result;

            var part1Solution = Part1Solution(inputLines);
            System.Console.WriteLine(part1Solution);

            var part2Solution = Part2Solution(inputLines);
            System.Console.WriteLine(part2Solution);
        }

        public static int Part1Solution(List<string> inputLines)
        {
            var planets = ProcessInput(inputLines);

            var centerPoint = planets.First(planet => planet.OrbitsAround == "COM");
            var planetsMapped = new List<Planet>();
            MapPlanetOrbits(centerPoint, planets, planetsMapped);

            var totalSteps = planetsMapped.Sum(planet => planet.OrbitNumber) + 1;
            return totalSteps;
        }

        public static int Part2Solution(List<string> inputLines)
        {
            var planets = ProcessInput(inputLines);

            var planetYou = planets.First(planet => planet.Name == "YOU");
            var planetSan = planets.First(planet => planet.Name == "SAN");
            var pathToYou = new List<Planet>();
            var pathToSan = new List<Planet>();

            GeneratePath(planetYou, planets, pathToYou);
            GeneratePath(planetSan, planets, pathToSan);

            var meanPath = pathToYou.Intersect(pathToSan).Count();
            var pathYouToSan = (pathToYou.Count() - meanPath) + (pathToSan.Count() - meanPath);

            return pathYouToSan;
        }

        private static void MapPlanetOrbits(Planet planet, List<Planet> planets, List<Planet> planetsMapped)
        {
            var orbittingPlanets = planets.Where(p => p.OrbitsAround == planet.Name).ToList();

            if (orbittingPlanets.Count() > 0)
            {
                foreach (var p in orbittingPlanets)
                {
                    var orbittingPlanet = planets.First(pl => pl.Equals(p));
                    orbittingPlanet.OrbitNumber = planet.OrbitNumber + 1;
                    planetsMapped.Add(orbittingPlanet);
                    MapPlanetOrbits(orbittingPlanet, planets, planetsMapped);
                }
            }
            else
            {
                return;
            }
        }

        private static void GeneratePath(Planet planet, List<Planet> planets, List<Planet> pathToPlanet) {
            var planetOrbittings = planets.Where(p => p.Name == planet.OrbitsAround);

            if (planetOrbittings.Any()) {
                var planetOrbitting = planetOrbittings.First();
                pathToPlanet.Add(planetOrbitting);
                GeneratePath(planetOrbitting, planets, pathToPlanet);
            }
            else {
                return;
            }
        }

        private static List<Planet> ProcessInput(List<string> inputLines) {
            return inputLines.Select(line =>
            {
                var data = line.Split(')');
                return new Planet(data[1], data[0]);
            }).ToList();
        }
    }
}
