using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day18;

abstract class Register<TState> {
    private Dictionary<string, long> register = new Dictionary<string, long>();

    protected bool running;
    protected int ip = 0;
    protected long this[string registry] {
        get {
            return long.TryParse(registry, out var n) ? n
                : register.ContainsKey(registry) ? register[registry]
                : 0;
        }
        set {
            register[registry] = value;
        }
    }


    /*
     * There aren't that many instructions, so it shouldn't be hard to figure out what they do. Here's what you determine:

        snd X plays a sound with a frequency equal to the value of X.
        set X Y sets register X to the value of Y.
        add X Y increases register X by the value of Y.
        mul X Y sets register X to the result of multiplying the value contained in register X by the value of Y.
        mod X Y sets register X to the remainder of dividing the value contained in register X by the value of Y (that is, it sets X to the result of X modulo Y).
        rcv X recovers the frequency of the last sound played, but only when the value of X is not zero. (If it is zero, the command does nothing.)
        jgz X Y jumps with an offset of the value of Y, but only if the value of X is greater than zero. (An offset of 2 skips the next instruction, an offset of -1 jumps to the previous instruction, and so on.)

     */
    public IEnumerable<TState> Instructions(string fileData) {
        var fd = fileData.Split('\n').ToArray();

        while (ip >= 0 && ip < fd.Length) {
            running = true;
            var line = fd[ip];
            var programs = line.Split(' ');
            switch (programs[0]) {
                case "snd": {
                    snd(programs[1]); 
                    break;
                } 
                case "set": {
                    set(programs[1], programs[2]);
                     break;
                }
                case "add": {
                    add(programs[1], programs[2]);
                     break;
                }
                case "mul": {
                    mul(programs[1], programs[2]);
                     break;
                }
                case "mod": {
                    mod(programs[1], programs[2]);
                    break;
                }
                case "rcv": {
                     rcv(programs[1]); 
                     break;
                }
                case "jgz": {
                     jgz(programs[1], programs[2]); 
                     break;
                }
                default: throw new Exception("Cannot parse " + line);
            }
            yield return State();
        }

        running = false;
        yield return State();
    }

    protected abstract TState State();

    // snd X plays a sound with a frequency equal to the value of X.
    protected abstract void snd(string reg);

     // set X Y sets register X to the value of Y.
    protected void set(string reg0, string reg1) {
        this[reg0] = this[reg1];
        ip++;
    }

    // add X Y increases register X by the value of Y.
    protected void add(string reg0, string reg1) {
        this[reg0] += this[reg1];
        ip++;
    }

    // mul X Y sets register X to the result of multiplying the value contained in register X by the value of Y.
    protected void mul(string reg0, string reg1) {
        this[reg0] *= this[reg1];
        ip++;
    }

     // mod X Y sets register X to the remainder of dividing the value contained in register X by the value of Y (that is, it sets X to the result of X modulo Y).
    protected void mod(string reg0, string reg1) {
        this[reg0] %= this[reg1];
        ip++;
    }
    
    // rcv X recovers the frequency of the last sound played, but only when the value of X is not zero. (If it is zero, the command does nothing.)
    protected abstract void rcv(string reg);

    // jgz X Y jumps with an offset of the value of Y, 
    // but only if the value of X is greater than zero. (An offset of 2 skips the next instruction, an offset of -1 jumps to the previous instruction, and so on.)
    protected void jgz(string reg0, string reg1) {
        ip += this[reg0] > 0 ? (int)this[reg1] : 1;
    }
}