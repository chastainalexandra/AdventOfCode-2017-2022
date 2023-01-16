using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day05;

[ProblemName("Alchemical Reduction")]
class Solution : Solver {

    Helper help = new Helper();

    // What is the length of the shortest polymer you can produce by removing all units of exactly one type and fully reacting the result?
    public object PartTwo(string fileInput) => (from ch in "abcdefghijklmnopqrstuvwxyz" select help.Units(fileInput, ch)).Min();

    // How many units remain after fully reacting the polymer you scanned?
    public object PartOne(string fileInput) => help.Units(fileInput);
}
