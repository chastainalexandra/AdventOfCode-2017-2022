using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day18;

class RegisterPart1 : Register<long?> {
    private long? instructionsSent = null;
    private long? instructionsReceived = null;

    // snd X plays a sound with a frequency equal to the value of X.
    protected override void snd(string reg) {
        instructionsSent = this[reg];
        ip++;
    }

    // rcv X recovers the frequency of the last sound played, but only when the value of X is not zero. (If it is zero, the command does nothing.)
    protected override void rcv(string reg) {
        if (this[reg] != 0) {
            instructionsReceived = instructionsSent;
        }
        ip++;
    }

    protected override long? State() { 
        return instructionsReceived; 
    }

}