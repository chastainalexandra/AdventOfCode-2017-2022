using System;
using System.Linq;

namespace AdventOfCode.Y2018.Day19;

public class Helper {
    
    public int[] GoWithTheFlow(int[] register, string op, int[] stm) {
        register = register.ToArray();
        register[stm[2]] = op switch {
            "addr" => register[stm[0]] + register[stm[1]],
            "addi" => register[stm[0]] + stm[1],
            "mulr" => register[stm[0]] * register[stm[1]],
            "muli" => register[stm[0]] * stm[1],
            "banr" => register[stm[0]] & register[stm[1]],
            "bani" => register[stm[0]] & stm[1],
            "borr" => register[stm[0]] | register[stm[1]],
            "bori" => register[stm[0]] | stm[1],
            "setr" => register[stm[0]],
            "seti" => stm[0],
            "gtir" => stm[0] > register[stm[1]] ? 1 : 0,
            "gtri" => register[stm[0]] > stm[1] ? 1 : 0,
            "gtrr" => register[stm[0]] > register[stm[1]] ? 1 : 0,
            "eqir" => stm[0] == register[stm[1]] ? 1 : 0,
            "eqri" => register[stm[0]] == stm[1] ? 1 : 0,
            "eqrr" => register[stm[0]] == register[stm[1]] ? 1 : 0,
            _ => throw new ArgumentException()
        };
        return register;
    }
}