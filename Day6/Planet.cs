using System;

namespace Day6
{
    struct Planet : IEquatable<Planet>
    {
        public string Name { get; set; }

        public string OrbitsAround { get; set; }

        public int OrbitNumber { get; set;}

        public Planet(string name, string orbitsAround)
        {
            Name = name;
            OrbitsAround = orbitsAround;
            OrbitNumber = 1;
        }

        public override string ToString()
        {
            return $"Planet {Name} orbits around {OrbitsAround} at orbitnumber {OrbitNumber}";
        }

        public bool Equals(Planet otherPlanet)
        {
            return this.Name == otherPlanet.Name;
        }
    }
}
