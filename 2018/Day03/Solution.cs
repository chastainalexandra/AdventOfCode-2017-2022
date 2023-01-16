using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day03;

[ProblemName("No Matter How You Slice It")]
class Solution : Solver {

    Helper help = new Helper();

    // What is the ID of the only claim that doesn't overlap?
    public object PartTwo(string fileInput) => help.FabricDecorating(fileInput).intactId;

    // If the Elves all proceed with their own plans, none of them will have enough fabric. How many square inches of fabric are within two or more claims?
     public object PartOne(string fileInput) => help.FabricDecorating(fileInput).overlapArea;
}
