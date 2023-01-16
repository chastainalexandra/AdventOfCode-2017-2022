using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day01;

[ProblemName("Chronal Calibration")]
class Solution : Solver {

    Solve solve = new Solve();

    // What is the first frequency your device reaches twice?
    public object PartTwo(string fileInput) {
        var seen = new HashSet<int>();
        foreach (var fd in solve.Frequencies(fileInput)) {
            if (seen.Contains(fd)) {
                return fd;
            }
            seen.Add(fd);
        }
        throw new Exception();
    }

    // Starting with a frequency of zero, what is the resulting frequency after all of the changes in frequency have been applied?
     public object PartOne(string fileInput) {
        return solve.Frequencies(fileInput).ElementAt(fileInput.Split("\n").Count() - 1);
    }
}
