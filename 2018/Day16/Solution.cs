using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day16;

[ProblemName("Chronal Classification")]
class Solution : Solver {

    Helper help = new Helper();

    // What value is contained in register 0 after executing the test program?
    public object PartTwo(string fileInput) {

        var constraints = Enumerable.Range(0, 16).ToDictionary(i => i, i => Enumerable.Range(0, 16).ToList());
        var (register, prg) = help.Parse(fileInput);
        foreach (var reg in register) {
            var op = reg.statement[0];
            var oldMapping = constraints[op];
            var newMapping = new List<int>();
            foreach (var i in oldMapping) {
                reg.statement[0] = i;
                var regsActual = help.Instruction(reg.registerB, reg.statement);
                if (Enumerable.Range(0, 4).All(register => regsActual[register] == reg.registerA[register])) {
                    newMapping.Add(i);
                }
            }
            constraints[op] = newMapping;
        }

        var mapping = help.Mapping(constraints, new bool[16], new Dictionary<int, int>());
        var regs = new int[4];
        foreach (var stm in prg) {
            stm[0] = mapping[stm[0]];
            regs = help.Instruction(regs, stm);
        }
        return regs[0];
    }

    // Ignoring the opcode numbers, how many samples in your puzzle input behave like three or more opcodes?

    public object PartOne(string fileInput) {
        var res = 0;
        var (register, prg) = help.Parse(fileInput);
        foreach (var reg in register) {
            var match = 0;
            for (var i = 0; i < 16; i++) { // an be manipulated by instructions containing one of 16 opcodes.
                reg.statement[0] = i;
                var regsActual = help.Instruction(reg.registerB, reg.statement);
                if (Enumerable.Range(0, 4).All(re => regsActual[re] == reg.registerA[re])) {
                    match++;
                }
            }
            if (match >= 3) {
                res++;
            }
        }
        return res;
    }
   
}

