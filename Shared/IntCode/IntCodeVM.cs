using System;
using System.Collections.Generic;
using System.Linq;

public class IntCodeVM
{
    private List<string> Memory { get; set; }
    private int InputValue { get; set; }
    public int OutputValue { get; set; }
    public bool IsHalted { get; set; }

    private int MaxLength { get; set; }
    private int iter = 0;
    private bool keepLooping;

    public IntCodeVM(string MemoryString)
    {
        this.Memory = MemoryString.Split(',').Select(int.Parse).Select(slot => slot.ToString("00000")).ToList();
        MaxLength = this.Memory.Count();
    }

    public void ShowMemorySlots()
    {
        foreach (var slot in Memory)
        {
            System.Console.WriteLine(slot);
        }
    }

    public void Run(int InputValue)
    {
        this.InputValue = InputValue;
        keepLooping = true;
        while (iter < MaxLength && keepLooping)
        {
            if (iter + 2 >= MaxLength)
            {
                break;
            }
            var opInstruction = new OpInstruction(Memory[iter]);
            var opCode = opInstruction.OpCode;
            System.Console.WriteLine($"opInstruction: {Memory[iter]}");
            System.Console.WriteLine($"opCode: {opCode}");
            switch (opCode)
            {
                // misschien zouden ints toch mooier zijn
                case (int)OpCode.Adds:
                    CalcOpCode1(opInstruction);
                    break;
                case (int)OpCode.Multiplies:
                    CalcOpCode2(opInstruction);
                    break;
                case (int)OpCode.Input:
                    CalcOpCode3(opInstruction);
                    break;
                case (int)OpCode.Output:
                    CalcOpCode4(opInstruction);
                    // experimenteel:
                    IsHalted = true;
                    keepLooping = false;
                    break;
                case (int)OpCode.JumpIfTrue:
                    CalcOpCode5(opInstruction);
                    break;
                case (int)OpCode.JumpIfFalse:
                    CalcOpCode6(opInstruction);
                    break;
                case (int)OpCode.LessThan:
                    CalcOpCode7(opInstruction);
                    break;
                case (int)OpCode.Equals:
                    CalcOpCode8(opInstruction);
                    break;
                case (int)OpCode.Halts:
                    System.Console.WriteLine("WE ZIJN BIJ 99");
                    IsHalted = true;
                    keepLooping = false;
                    break;
            }
        }
    }

    public void Run(int inputValue, int phase)
    {
        this.InputValue = inputValue;
        Memory[Int32.Parse(Memory[iter + 1])] = phase.ToString("00000");
        iter += 2;
        Run(inputValue);
    }

    private void CalcOpCode1(OpInstruction opInstruction)
    {
        // of moet dit in de OpInstruction komen?
        var value1 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode1, 1)]);
        var value2 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode2, 2)]);
        var position3 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode3, 3)]);
        Memory[position3] = (value1 + value2).ToString("00000");
        iter += 4;
    }

    private void CalcOpCode2(OpInstruction opInstruction)
    {
        var value1 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode1, 1)]);
        var value2 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode2, 2)]);
        var position3 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode3, 3)]);
        Memory[position3] = (value1 * value2).ToString("00000");
        iter += 4;
    }

    private void CalcOpCode3(OpInstruction opInstruction)
    {
        var position1 = GetLocation(opInstruction.OpMode1, 1);
        Memory[position1] = InputValue.ToString("00000");
        iter += 2;
    }

    private void CalcOpCode4(OpInstruction opInstruction)
    {
        var position1 = GetLocation(opInstruction.OpMode1, 1);
        OutputValue = Int32.Parse(Memory[position1]);
        iter += 2;
    }

    private void CalcOpCode5(OpInstruction opInstruction)
    {
        var position1 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode1, 1)]);

        if (Int32.Parse(Memory[position1]) != 0)
        {
            iter = Int32.Parse(Memory[GetLocation(opInstruction.OpMode2, 2)]);
        }
        else
        {
            iter += 3;
        }
    }

    private void CalcOpCode6(OpInstruction opInstruction)
    {
        var position1 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode1, 1)]);

        if (Int32.Parse(Memory[position1]) == 0)
        {
            iter = Int32.Parse(Memory[GetLocation(opInstruction.OpMode2, 2)]);
        }
        else
        {
            iter += 3;
        }
    }

    private void CalcOpCode7(OpInstruction opInstruction)
    {
        var value1 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode1, 1)]);
        var value2 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode2, 2)]);
        var position3 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode3, 3)]);
        Memory[position3] = value1 < value2 ? "00001" : "00000";
        iter += 4;
    }

    private void CalcOpCode8(OpInstruction opInstruction)
    {
        var value1 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode1, 1)]);
        var value2 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode2, 2)]);
        var position3 = Int32.Parse(Memory[GetLocation(opInstruction.OpMode3, 3)]);
        Memory[position3] = value1 == value2 ? "00001" : "00000";
        iter += 4;
    }

    private int GetLocation(OpMode opMode, int offset)
    {
        switch (opMode)
        {
            case OpMode.Position:
                return Int32.Parse(Memory[iter + offset]);
            case OpMode.Immediate:
                return iter + offset;
        }
        return 0;
    }
}