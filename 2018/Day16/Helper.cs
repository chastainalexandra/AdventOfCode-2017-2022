using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace AdventOfCode.Y2018.Day16;

public class Helper {
    public Dictionary<int, int> Mapping(Dictionary<int, List<int>> constaints, bool[] used, Dictionary<int, int> res) {
        var op = res.Count;
        if (op == 16) {
            return res;
        }
        foreach (var i in constaints[op]) {
            if (!used[i]) {
                used[i] = true;
                res[op] = i;
                var x = Mapping(constaints, used, res);
                if (x != null) {
                    return x;
                }
                res.Remove(op);
                used[i] = false;
            }
        }
        return null;
    }

    public (List<Registers> register, List<int[]> prg) Parse(string fileInput) {
        var fl = fileInput.Split("\n").ToList();
        var iline = 0;

        var regs = new List<Registers>();
        while (Ints(@"Before: \[(\d+), (\d+), (\d+), (\d+)\]", out var regB)) {
            Ints(@"(\d+) (\d+) (\d+) (\d+)", out var statement);
            Ints(@"After:  \[(\d+), (\d+), (\d+), (\d+)\]", out var regA);
            iline++;
            regs.Add(new Registers() { registerB = regB, registerA = regA, statement = regB });
        }
        iline++;
        iline++;
        var prg = new List<int[]>();
        while (Ints(@"(\d+) (\d+) (\d+) (\d+)", out var stm)) {
            prg.Add(stm);
        }

        bool Ints(string pattern, out int[] r) {
            r = null;
            if (iline >= fl.Count) {
                return false;
            }
            var m = Regex.Match(fl[iline], pattern);
            if (m.Success) {
                iline++;
                r = m.Groups.Values.Skip(1).Select(x => int.Parse(x.Value)).ToArray();
            }
            return m.Success;
        }
        return (regs, prg);
    }

    public int[] Instruction(int[] register, int[] stm) {
        register = register.ToArray();
        register[stm[3]] = stm[0] switch {
            0 => register[stm[1]] + register[stm[2]],
            1 => register[stm[1]] + stm[2],
            2 => register[stm[1]] * register[stm[2]],
            3 => register[stm[1]] * stm[2],
            4 => register[stm[1]] & register[stm[2]],
            5 => register[stm[1]] & stm[2],
            6 => register[stm[1]] | register[stm[2]],
            7 => register[stm[1]] | stm[2],
            8 => register[stm[1]],
            9 => stm[1],
            10 => stm[1] > register[stm[2]] ? 1 : 0,
            11 => register[stm[1]] > stm[2] ? 1 : 0,
            12 => register[stm[1]] > register[stm[2]] ? 1 : 0,
            13 => stm[1] == register[stm[2]] ? 1 : 0,
            14 => register[stm[1]] == stm[2] ? 1 : 0,
            15 => register[stm[1]] == register[stm[2]] ? 1 : 0,
            _ => throw new ArgumentException()
        };
        return register;
    }
}