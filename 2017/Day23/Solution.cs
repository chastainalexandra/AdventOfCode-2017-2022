using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2017.Day23;

[ProblemName("Coprocessor Conflagration")]
class Solution : Solver {

    // public string GetName() => "Coprocessor Conflagration"; have to move this to above with upgrade 

   
    public object PartTwo(string fileData) {
        //TODO: Find solution to this one where did i put it
       return fileData;
    }

    // If you run the program (your puzzle input), how many times is the mul instruction invoked?
    public object PartOne(string fileData) {
        var register = new Dictionary<string, int>();
        int program = 0;
       
        void setRegister(string reg, int value) {
            register[reg] = value;
        }

         int getRegister(string registry) {
            return int.TryParse(registry, out var n) ? n
                : register.ContainsKey(registry) ? register[registry]
                : 0;
        }

        var fd = fileData.Split('\n');
        var mulInstructionCount = 0;
        while (program >= 0 && program < fd.Length) {
            var programLine = fd[program];
            var programs = programLine.Split(' ');
            switch (programs[0]) {
                case "set":
                    setRegister(programs[1], getRegister(programs[2])); // set X Y sets register X to the value of Y.
                    program++;
                    break;
                case "sub":
                    setRegister(programs[1], getRegister(programs[1]) - getRegister(programs[2])); // sub X Y decreases register X by the value of Y.
                    program++;
                    break;
                case "mul":
                    mulInstructionCount++;
                    setRegister(programs[1], getRegister(programs[1]) * getRegister(programs[2]));  // mul X Y sets register X to the result of multiplying the value contained in register X by the value of Y.
                    program++;
                    break;
                case "jnz":
                    program += getRegister(programs[1]) != 0 ? getRegister(programs[2]) : 1; // jnz X Y jumps with an offset of the value of Y, but only if the value of X is not zero. (An offset of 2 skips the next instruction, an offset of -1 jumps to the previous instruction, and so on.)
                    break;
                default: throw new Exception("Cannot parse " + programLine);
            }
        }
        return mulInstructionCount;
    }
}
