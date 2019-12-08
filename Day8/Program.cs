using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Day8
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = InputGetter.ReadInputAsString(2019, 8).Result;
            // var input = "123456789012";

            var part1Solution = Part1Solution(input, 25, 6);
            System.Console.WriteLine(part1Solution);

            // var testInput = "0222112222120000";
            // Part2Solution(testInput, 2, 2);
            Part2Solution(input, 25, 6);
        }

        public static int Part1Solution(string input, int xSize, int ySize)
        {
            var layers = GetLayers(input, xSize, ySize);

            var lowest0Layers = layers.Select(layers => layers.Count(pixel => pixel == '0')).ToList();
            var listLeastZeros = layers[lowest0Layers.IndexOf(lowest0Layers.Min())];
            var factor1and2s = listLeastZeros.Where(pixel => pixel == '1').Count() * listLeastZeros.Where(pixel => pixel == '2').Count();
            return factor1and2s;
        }

        public static void Part2Solution(string input, int xSize, int ySize)
        {
            var layers = GetLayers(input, xSize, ySize);
            var picture = new List<char>();

            // liever had ik iets met zip gedaan;
            for (int i = 0; i < xSize * ySize; i++)
            {
                var j = 0;
                var pixel = '2';
                while (j < layers.Count()) {
                    var layerPixel = layers[j][i];
                    if (layerPixel == '0' || layerPixel == '1') {
                        pixel = layerPixel;
                        break;
                    }
                    j++;
                }
                picture.Add(pixel);
            }

            for (int y = 0; y < ySize; y++)
            {
                Console.WriteLine();
                for (int x = 0; x < xSize; x++)
                {
                    var output = picture[(y * xSize) + x];
                    if (output == '0')
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                    }
                    else if (output == '1')
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write(' ');
                    }
                }
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        private static List<List<char>> GetLayers(string input, int xSize, int ySize)
        {
            var layers = new List<List<char>>();
            for (int i = 0; i < input.Length; i++)
            {
                if (i % (xSize * ySize) == 0)
                {
                    layers.Add(new List<char>());
                }
                layers[layers.Count() - 1].Add(input[i]);
            }
            return layers;
        }
    }
}
