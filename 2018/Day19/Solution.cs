using System;
using System.Linq;

namespace AdventOfCode.Y2018.Day19;

[ProblemName("Go With The Flow")]
class Solution : Solver {

     Helper help = new Helper();
    
    // What value is left in register 0 when this new background process halts?

    public object PartTwo(string fileInput) {
        var time = 10551292;
        var registerZero = 0;
        for (var x = 1; x <= time; x++) {
            if (time % x == 0)
                registerZero += x;
        }
        return registerZero;
    }


    // What value is left in register 0 when the background process halts?
    public object PartOne(string fileInput) {
        var ip = 0;
        var register = int.Parse(fileInput.Split("\n").First().Substring("#ip ".Length));
        var fi = fileInput.Split("\n").Skip(1).ToArray();
        var instructionPointer = new int[6]; // The instruction pointer is 6, so the instruction seti 9 0 5 stores 9 into register 5. The instruction pointer is incremented, causing it to point outside the program, and so the program ends.
        while (ip >= 0 && ip < fi.Length) {
            var args = fi[ip].Split(" ");
            instructionPointer[register] = ip;
            instructionPointer = help.GoWithTheFlow(instructionPointer, args[0], args.Skip(1).Select(int.Parse).ToArray());
            ip = instructionPointer[register];
            ip++;
        }
        return instructionPointer[0];
    }


    
}
