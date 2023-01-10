using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day25;

[ProblemName("The Halting Problem")]
class Solution : Solver {

    // public string GetName() => "The Halting Problem"; have to move this to above with upgrade 

    Solve solve = new Solve();

    // Recreate the Turing machine and save the computer! What is the diagnostic checksum it produces once it's working again?
    public object PartOne(string fileData) {
        var fd = solve.Parse(fileData);
        var tape = new Dictionary<int, int>();
        var pos = 0;
        while (fd.iterations > 0) {
            var read = tape.TryGetValue(pos, out var t) ? t : 0;
            var (write, dir, newState) = fd.prg[(fd.state, read)];
            fd.state = newState;
            tape[pos] = write;
            pos += dir;
            fd.iterations--;
        }
        return tape.Select(kvp => kvp.Value).Sum();
    }

}



