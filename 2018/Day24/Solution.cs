using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day24;

[ProblemName("Immune System Simulator 20XX")]
class Solution : Solver {

    Helper help = new Helper();

    // How many units does the immune system have left after getting the smallest boost it needs to win?
    public object PartTwo(string fileInput) {
        var l = 0;
        var h = int.MaxValue / 2;
        while (h - l > 1) {
            var m = (h + l) / 2;
            if (help.DetermineUnitsNeeded(fileInput, m).immuneSystem) {
                h = m;
            } else {
                l = m;
            }
        }
        return help.DetermineUnitsNeeded(fileInput, h).units;
    }

    // As it stands now, how many units would the winning army have?
    public object PartOne(string fileInput) => help.DetermineUnitsNeeded(fileInput, 0).units;

    
}