﻿using System;
using Shared;
using System.Linq;
using System.Collections.Generic;

namespace Day7
{
    public class Program
    {
        static void Main(string[] args)
        {
            // var inputLine = InputGetter.ReadInputAsString(2019, 7).Result;
            var inputLine = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            var input = inputLine.Split(',').Select(int.Parse).Select(slot => slot.ToString("00000")).ToList();
            var program = input.Select(int.Parse).ToList(); // als ik dit nou niet naar een int parse

            // var intprog = new IntCodeVM(inputLine);
            // intprog.ShowMemorySlots();


            // var part1Input = program.Select(ding => ding).ToList();

            // var part1Solution = Part1Solution(program);
            // System.Console.WriteLine(part1Solution);

            var part1Solution2 = Part1SolutionRefactor(inputLine);
            System.Console.WriteLine(part1Solution2);

            // var part2Input = program.Select(ding => ding).ToList();
            // var part2Solution = Part2Solution(part2Input);
            // System.Console.WriteLine(part2Solution);
        }

        public static int Part1SolutionRefactor(string input) {
            // Generate possible phases
            var allPhases = new List<List<int>>();

            // er is vast een slimmere manier om de permutaties te genereren
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    for (int k = 0; k < 5; k++)
                        for (int l = 0; l < 5; l++)
                            for (int m = 0; m < 5; m++)
                                allPhases.Add(new List<int>() { i, j, k, l, m });

            var filteredphases = allPhases.Where(allPhase => allPhase.Distinct().Count() == allPhase.Count());

            // generate the outputs
            var outputs = filteredphases.Select(phase =>
            {
                var inputProgram = input.Select(inp => inp).ToList();
                return GetThrusterSignal2(input, phase);
            });

            // input
            return outputs.Max();
        }

        public static int Part1Solution(List<int> input)
        {
            // Generate possible phases
            var allPhases = new List<List<int>>();

            // er is vast een slimmere manier om de permutaties te genereren
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    for (int k = 0; k < 5; k++)
                        for (int l = 0; l < 5; l++)
                            for (int m = 0; m < 5; m++)
                                allPhases.Add(new List<int>() { i, j, k, l, m });

            var filteredphases = allPhases.Where(allPhase => allPhase.Distinct().Count() == allPhase.Count());

            // generate the outputs
            var outputs = filteredphases.Select(phase =>
            {
                var inputProgram = input.Select(inp => inp).ToList();
                return GetThrusterSignal(inputProgram, phase);
            });

            // input
            return outputs.Max();
        }

        // public static int Part2Solution(List<int> input)
        // {
        //     // Generate possible phases
        //     var allPhases = new List<List<int>>();

        //     //er is vast een slimmere manier om de permutaties te genereren
        //     // var start = 5;
        //     // var end = 10;
        //     // for (int i = start; i < end; i++)
        //     //     for (int j = start; j < end; j++)
        //     //         for (int k = start; k < end; k++)
        //     //             for (int l = start; l < end; l++)
        //     //                 for (int m = start; m < end; m++)
        //     //                     allPhases.Add(new List<int>() { i, j, k, l, m});

        //     // var filteredphases = allPhases.Where(allPhase => allPhase.Distinct().Count() == allPhase.Count());

        //     // // generate the outputs
        //     // var outputs = filteredphases.Select(phase => {
        //     //     var inputProgram = input.Select(inp => inp).ToList();
        //     //     return GetThrusterSignalFromLoop(inputProgram, phase);
        //     // });

        //     var faasjes = new List<int>() { 9, 8, 7, 6, 5 };
        //     var bla = GetThrusterSignalFromLoop(input, faasjes);

        //     // input
        //     // return outputs.Max();
        //     return bla;
        // }

        public static int GetThrusterSignal(List<int> input, List<int> phases)
        {
            // input
            var thrusterSignal = 0;
            var isHalted = false;

            foreach (var phase in phases)
            {
                thrusterSignal = Day5SolutionHacked(input, thrusterSignal, ref isHalted, phase);
            }
            return thrusterSignal;
        }

        public static int GetThrusterSignal2(string input, List<int> phases)
        {
            // input
            var thrusterSignal = 0;
            var intcodeVM = new IntCodeVM(input);
            foreach (var phase in phases)
            {
                intcodeVM.Run(thrusterSignal, phase);
                thrusterSignal = intcodeVM.OutputValue;
            }
            return thrusterSignal;
        }

        // public static int GetThrusterSignalFromLoop(List<int> input, List<int> phases)
        // {
        //     // input
        //     var thrusterSignal = 0;
        //     var isHalted = false;
        //     var dummyBool = false;
        //     // deze zou dan niet op 0 weer moeten misschien, maar blijven op waar die was
        //     var iter = 0;

        //     var inputAmps = new List<List<int>>();

        //     for (int i = 0; i < 5; i++)
        //     {
        //         inputAmps.Add(input.Select(ding => ding).ToList());
        //         Day5SolutionHacked(inputAmps[i], thrusterSignal, ref dummyBool, phases[i]);
        //     }

        //     // verwerk de phases voor elk:

        //     // poging 1
        //     // while (!isHalted)
        //     // {
        //     //     dummyBool = false;
        //     //     // System.Console.WriteLine(iter % 5);
        //     //     // System.Console.WriteLine(thrusterSignal);
        //     //     if (iter == 0 || iter == 1 || iter == 2 || iter == 3 || iter == 4)
        //     //     {
        //     //         thrusterSignal = Day5SolutionHacked(inputAmps[iter % 5], thrusterSignal, ref dummyBool, phases[iter]);
        //     //         System.Console.WriteLine("gefaseerd");
        //     //     }
        //     //     else if (iter % 5 != 4 && iter > 4)
        //     //     {
        //     //         // System.Console.WriteLine("HOI");
        //     //         thrusterSignal = Day5SolutionHacked(inputAmps[iter % 5], thrusterSignal, ref dummyBool);
        //     //         // System.Console.WriteLine("UM");
        //     //     }
        //     //     else if (iter % 5 == 4)
        //     //     {
        //     //         // System.Console.WriteLine("HO2");
        //     //         thrusterSignal = Day5SolutionHacked(inputAmps[iter % 5], thrusterSignal, ref isHalted);
        //     //     }
        //     //     iter++;
        //     // }

        //     // poging 2
        //     while (!isHalted)
        //     {
        //         var dummyBool2 = false;
        //         // System.Console.WriteLine(iter % 5);
        //         // System.Console.WriteLine(thrusterSignal);
        //         if (iter == 0 || iter == 1 || iter == 2 || iter == 3 || iter == 4)
        //         {
        //             thrusterSignal = Day5SolutionHacked(input, thrusterSignal, ref dummyBool2, phases[iter]);
        //             // System.Console.WriteLine("gefaseerd");
        //         }
        //         else if (iter % 5 != 4 && iter > 4)
        //         {
        //             // System.Console.WriteLine("HOI");
        //             thrusterSignal = Day5SolutionHacked(input, thrusterSignal, ref dummyBool2);
        //             // System.Console.WriteLine("UM");
        //         }
        //         else if (iter % 5 == 4)
        //         {
        //             // System.Console.WriteLine("HO2");
        //             thrusterSignal = Day5SolutionHacked(input, thrusterSignal, ref isHalted);
        //         }
        //         iter++;
        //     }
        //     return thrusterSignal;
        // }

        // Hier kan je anders beter ff refactoren en classe van maken
        public static int Day5SolutionHacked(List<int> input, int initialInput, ref bool isHalted, int phaseInput = -1)
        {
            var instructions = input;
            var keepLooping = true;
            var iter = 0;
            var inputValue = initialInput;
            var maxLength = input.Count();

            // dit is iets met de phases
            if (phaseInput != -1)
            {
                var positie = instructions[iter + 1];
                instructions[positie] = phaseInput;
                iter = 2;
            } else {
                iter = 2;
            }

            //werkt dit wel, als je nei tphaseInputinvult

            // for (int i= 0; i<input.Count(); i++) {
            //     System.Console.WriteLine("index: {0}, value: {1}", i, input[i]);
            // }

            while (iter < maxLength && keepLooping)
            {
                if (iter + 2 >= maxLength)
                {
                    break;
                }
                var opcode = input[iter];
                int position1 = 0;


                // System.Console.WriteLine("loop vast opcode: {0}, iter: {1}", opcode, iter);
                // System.Console.WriteLine("opcode {0}", opcode);
                switch (opcode)
                {
                    case 1:
                        CalcOpcode1(instructions, iter);
                        iter += 4;
                        break;
                    case 2:
                        CalcOpcode2(instructions, iter);
                        iter += 4;
                        break;
                    case 3:
                        position1 = instructions[iter + 1];
                        instructions[position1] = inputValue;
                        iter += 2;
                        break;
                    case 4:
                        position1 = instructions[iter + 1];
                        inputValue = instructions[position1];
                        iter += 2;
                        // experimenteel:
                        keepLooping = false;
                        break;
                    case 5:
                        System.Console.WriteLine("CASE 5");
                        if (instructions[instructions[iter + 1]] != 0)
                        {
                            iter = instructions[iter + 2];
                        }
                        else
                        {
                            iter += 3;
                        }
                        break;
                    case 6:
                        System.Console.WriteLine("CASE 6");
                        if (instructions[instructions[iter + 1]] == 0)
                        {
                            iter = instructions[iter + 2];
                        }
                        else
                        {
                            iter += 3;
                        }
                        break;
                    case 7:
                        System.Console.WriteLine("CASE 7");
                        CalcOpcode7(instructions, iter);
                        iter += 4;
                        break;
                    case 8:
                        System.Console.WriteLine("CASE 8");
                        CalcOpcode8(instructions, iter);
                        iter += 4;
                        break;
                    case 99:
                    System.Console.WriteLine("WE ZIJN BIJ 99");
                        keepLooping = false;
                        isHalted = true;
                        break;
                    default:
                        // wtf arjan
                        var bla = opcode.ToString().ToCharArray();
                        Array.Reverse(bla);
                        var opInstruction = new String(bla);

                        var parameterLength = opInstruction.Length;
                        var opInstructionCode = opInstruction[0];
                        var opMode1 = parameterLength > 2 ? opInstruction[2] : '0';
                        var opMode2 = parameterLength > 3 ? opInstruction[3] : '0';
                        var opMode3 = parameterLength > 4 ? opInstruction[4] : '0';
                        switch (opInstructionCode)
                        {
                            case '1':
                                CalcOpcode1(instructions, iter, opMode1, opMode2, opMode3);
                                iter += 4;
                                break;
                            case '2':
                                CalcOpcode2(instructions, iter, opMode1, opMode2, opMode3);
                                iter += 4;
                                break;
                            case '3':
                                position1 = opMode1 == '0' ? instructions[iter + 1] : iter + 1;
                                instructions[position1] = inputValue;
                                iter += 2;
                                break;
                            case '4':
                                position1 = opMode1 == '0' ? instructions[iter + 1] : iter + 1;
                                inputValue = instructions[position1];
                                iter += 2;
                                // experimenteel:
                                keepLooping = false;
                                break;
                            case '5':
                                position1 = opMode1 == '0' ? instructions[iter + 1] : iter + 1;

                                if (instructions[position1] != 0)
                                {
                                    iter = opMode2 == '0' ? instructions[instructions[iter + 2]] : instructions[iter + 2];
                                    // System.Console.WriteLine(iter);
                                    // iter = instructions[iter + 2];
                                }
                                else
                                {
                                    iter += 3;
                                }
                                break;
                            case '6':
                                position1 = opMode1 == '0' ? instructions[iter + 1] : iter + 1;
                                // position1 = iter + 1;
                                if (instructions[position1] == 0)
                                {
                                    iter = opMode2 == '0' ? instructions[instructions[iter + 2]] : instructions[iter + 2];
                                }
                                else
                                {
                                    iter += 3;
                                }
                                break;
                            case '7':
                                CalcOpcode7(instructions, iter, opMode1, opMode2, opMode3);
                                iter += 4;
                                break;
                            case '8':
                                CalcOpcode8(instructions, iter, opMode1, opMode2, opMode3);
                                iter += 4;
                                break;
                            default:
                                System.Console.WriteLine("KAPOT");
                                // isHalted = true;
                                break;

                        }
                        break;
                }
            }

            return inputValue;
            //return input[0];
        }

        public static void CalcOpcode1(List<int> instructions, int iter, char opModeP1 = '0', char opModeP2 = '0', char opModeP3 = '0')
        {
            var value1 = opModeP1 == '0' ? instructions[instructions[iter + 1]] : instructions[iter + 1];
            var value2 = opModeP2 == '0' ? instructions[instructions[iter + 2]] : instructions[iter + 2];
            var position3 = opModeP3 == '0' ? instructions[iter + 3] : iter + 3;
            instructions[position3] = value1 + value2;
        }

        public static void CalcOpcode2(List<int> instructions, int iter, char opModeP1 = '0', char opModeP2 = '0', char opModeP3 = '0')
        {
            var value1 = opModeP1 == '0' ? instructions[instructions[iter + 1]] : instructions[iter + 1];
            var value2 = opModeP2 == '0' ? instructions[instructions[iter + 2]] : instructions[iter + 2];
            var position3 = opModeP3 == '0' ? instructions[iter + 3] : iter + 3;
            instructions[position3] = value1 * value2;
        }

        public static void CalcOpcode7(List<int> instructions, int iter, char opModeP1 = '0', char opModeP2 = '0', char opModeP3 = '0')
        {
            var value1 = opModeP1 == '0' ? instructions[instructions[iter + 1]] : instructions[iter + 1];
            var value2 = opModeP2 == '0' ? instructions[instructions[iter + 2]] : instructions[iter + 2];
            var position3 = opModeP3 == '0' ? instructions[iter + 3] : iter + 3;
            instructions[position3] = value1 < value2 ? 1 : 0;
        }

        public static void CalcOpcode8(List<int> instructions, int iter, char opModeP1 = '0', char opModeP2 = '0', char opModeP3 = '0')
        {
            var value1 = opModeP1 == '0' ? instructions[instructions[iter + 1]] : instructions[iter + 1];
            var value2 = opModeP2 == '0' ? instructions[instructions[iter + 2]] : instructions[iter + 2];
            var position3 = opModeP3 == '0' ? instructions[iter + 3] : iter + 3;
            instructions[position3] = value1 == value2 ? 1 : 0;
        }
    }
}
