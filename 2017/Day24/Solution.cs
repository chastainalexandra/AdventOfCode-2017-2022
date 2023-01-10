using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day24;

[ProblemName("Electromagnetic Moat")]
class Solution : Solver {

    // public string GetName() => "Electromagnetic Moat"; have to move this to above with upgrade 

    public Solve solve = new Solve();

    // What is the strength of the longest bridge you can make? If you can make multiple bridges of the longest length, pick the strongest one.
    public object PartTwo(string fileData) => solve.Bridge(fileData, (a, b) => a.CompareTo(b));

    // What is the strength of the strongest bridge you can make with the components you have available?
    public object PartOne(string fileData) => solve.Bridge(fileData, (a, b) => a.strength - b.strength);
}

