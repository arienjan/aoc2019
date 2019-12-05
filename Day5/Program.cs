using System;
using Shared;
using System.Linq;
using System.Collections.Generic;

namespace Day5
{
    public class Program
    {
        static void Main(string[] args)
        {
            // InputGetter.ReadInputAsString(2019, 2).Wait();
            var inputLine = InputGetter.ReadInputAsString(2019, 5).Result;
            var input = inputLine.Split(',').ToList();
            var program = input.Select(int.Parse).ToList();

            // arrange part 1
            var initialInput = 1;
            var part1Input = program.Select(ding => ding).ToList();

            var part1Solution = Part1Solution(part1Input, initialInput);
            System.Console.WriteLine(part1Solution);

            // arrange part 2
            initialInput = 5;
            var part2Input = program.Select(ding => ding).ToList();
            var part2Solution = Part2Solution(part2Input, initialInput);
            System.Console.WriteLine(part2Solution);
        }

        public static int Part1Solution(List<int> input, int initialInput)
        {
            var instructions = input;
            var keepLooping = true;
            var iter = 0;
            var inputValue = initialInput;
            var maxLength = input.Count();
            while (iter < maxLength && keepLooping)
            {
                if (iter + 2 >= maxLength)
                {
                    break;
                }
                var opcode = input[iter];
                int position1 = 0;
                // if (position1 >= maxLength || position2 >= maxLength || position3 >= maxLength)
                // {
                //     break;
                // }

                // System.Console.WriteLine(opcode);

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
                        break;
                    case 99:
                        keepLooping = false;
                        break;
                    default:
                        // wtf arjan
                        var bla = opcode.ToString().ToCharArray();
                        Array.Reverse(bla);
                        var opInstruction = new String(bla);

                        var parameterLength = opInstruction.Length;
                        // als het alleen 1, 2, 3 of 4 is:
                        var opInstructionCode = opInstruction[0];
                        // dit is wel heel lelijk
                        var opMode1 = parameterLength > 2 ? opInstruction[2] : '0';
                        var opMode2 = parameterLength > 3 ? opInstruction[3] : '0';
                        // System.Console.WriteLine("opInstruction {0}", opInstruction);
                        // deze zit er nog niet in
                        // var instructionSet3 = parameterLength > 4 ? new Tuple<int, char>(input[iter+3], opInstruction[4]) : null;
                        switch (opInstructionCode)
                        {
                            case '1':
                                //System.Console.WriteLine("Case 1");
                                CalcOpcode1(instructions, iter, opMode1, opMode2);
                                iter += 4;
                                break;
                            case '2':
                                //System.Console.WriteLine("Case 2");
                                CalcOpcode2(instructions, iter, opMode1, opMode2);
                                iter += 4;
                                break;
                            case '3':
                                //System.Console.WriteLine("Case 3");
                                position1 = opMode1 == '0' ? instructions[iter + 1] : iter + 1;
                                instructions[position1] = inputValue;
                                iter += 2;
                                break;
                            case '4':
                                //System.Console.WriteLine("Case 4");
                                position1 = opMode1 == '0' ? instructions[iter + 1] : iter + 1;
                                inputValue = instructions[position1];
                                iter += 2;
                                break;
                            default:
                                System.Console.WriteLine("KAPOT");
                                break;

                        }
                        break;
                }
            }

            return inputValue;
            //return input[0];
        }

        public static int Part2Solution(List<int> input, int initialInput)
        {
            var instructions = input;
            var keepLooping = true;
            var iter = 0;
            var inputValue = initialInput;
            var maxLength = input.Count();
            while (iter < maxLength && keepLooping)
            {
                if (iter + 2 >= maxLength)
                {
                    break;
                }
                var opcode = input[iter];
                int position1 = 0;

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
                        System.Console.WriteLine("CASE 5");
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
                        System.Console.WriteLine("CASE 5");
                        CalcOpcode7(instructions, iter);
                        iter += 4;
                        break;
                    case 8:
                        System.Console.WriteLine("CASE 5");
                        CalcOpcode8(instructions, iter);
                        iter += 4;
                        break;
                    case 99:
                        keepLooping = false;
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
                                break;
                            case '5':
                                position1 = opMode1 == '0' ? instructions[iter + 1] : iter + 1;
                                // position1 = instructions[iter + 1];
                                // position1 = iter + 1;
                                if (instructions[position1] != 0)
                                {
                                    iter = opMode2 == '0' ? instructions[instructions[iter + 2]] : instructions[iter + 2];
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
            var position3 = opModeP3 == '0' ? instructions[iter + 3] : iter+3;
            instructions[position3] = value1 + value2;
        }

        public static void CalcOpcode2(List<int> instructions, int iter, char opModeP1 = '0', char opModeP2 = '0', char opModeP3 = '0')
        {
            var value1 = opModeP1 == '0' ? instructions[instructions[iter + 1]] : instructions[iter + 1];
            var value2 = opModeP2 == '0' ? instructions[instructions[iter + 2]] : instructions[iter + 2];
            var position3 = opModeP3 == '0' ? instructions[iter + 3] : iter+3;
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
