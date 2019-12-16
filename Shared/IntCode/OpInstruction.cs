using System;

public class OpInstruction {
    public int OpCode { get; set; }

    public string Instruction { get; set; }

    public OpMode OpMode1 { get; set; }
    public OpMode OpMode2 {get; set; }
    public OpMode OpMode3 {get; set;}

    public OpInstruction(string memorySlot) {
        Instruction = memorySlot;
        Instruction = Instruction.Replace("-", string.Empty);
        OpCode = Int32.Parse(Instruction.Substring(3, 2));
        OpMode1 = (OpMode)Int32.Parse(Instruction[2].ToString());
        OpMode2 = (OpMode)Int32.Parse(Instruction[1].ToString());
        OpMode3 = (OpMode)Int32.Parse(Instruction[0].ToString());
    }
}