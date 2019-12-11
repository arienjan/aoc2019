using System.Collections.Generic;

public class IntCodeVM {
    private List<int> Memory { get; set; }
    public int InputValue { get; set; }
    public int OutputValue { get; set; }
    public bool IsHalted {get; set;}

    private int iter;

    public IntCodeVM(List<int> Memory) {
        this.Memory = Memory;
    }

    public void Run(int inputValue) {
        iter = 0;
    }

    public void Run(int inputValue, int phase) {
        iter = 0;
    }
}