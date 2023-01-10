using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day18;

[ProblemName("Duet")]
class Solution : Solver {

    // public string GetName() => "Duet"; have to move this to above with upgrade 

    // Once both of your programs have terminated (regardless of what caused them to do so), how many times did program 1 send a value?
    public object PartTwo(string fileData) {
        var p0Input = new Queue<long>();
        var p1Input = new Queue<long>();

        return Enumerable
            .Zip(
                new RegisterPart2(0, p0Input, p1Input).Instructions(fileData), 
                new RegisterPart2(1, p1Input, p0Input).Instructions(fileData), 
                (state0, state1) => (state0: state0, state1: state1))
            .First(x => !x.state0.running && !x.state1.running)
            .state1.programSent;
    }

    /*
     * What is the value of the recovered frequency (the value of the most recently played sound) the first time a rcv instruction is executed with a non-zero value?*/
     public object PartOne(string fileData) =>
        new RegisterPart1()
            .Instructions(fileData)
            .First(received => received != null).Value;
}
