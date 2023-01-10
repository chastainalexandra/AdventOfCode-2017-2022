using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day18;

class RegisterPart2 : Register<(bool running, int programSent)> {
    private int programSent = 0;
    private Queue<long> programIn;
    private Queue<long> programOut;

    public RegisterPart2(long p, Queue<long> programIn, Queue<long> programOut) {
        this["p"] = p;
        this.programIn = programIn;
        this.programOut = programOut;
    }

    protected override (bool running, int programSent) State() { 
        return (running: running, programSent: programSent); 
    }  

    // snd X sends the value of X to the other program. 
    // These values wait in a queue until that program is ready to receive them. Each program has its own message queue, so a program can never receive a message it sent.
    protected override void snd(string register) {
        programOut.Enqueue(this[register]);
        programSent++;
        ip++;
    }

    // rcv X receives the next value and stores it in register X.
    //  If no values are in the queue, the program waits for a value to be sent to it. 
    // Programs do not continue to the next instruction until they have received a value. Values are received in the order they are sent.
    protected override void rcv(string register) {
        if (programIn.Any()) {
            this[register] = programIn.Dequeue();
            ip++;
        } else {
            running = false;
        }
    }
}
