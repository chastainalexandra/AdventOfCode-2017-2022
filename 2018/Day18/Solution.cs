using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day18;

[ProblemName("Settlers of The North Pole")]
class Solution : Solver {

    Helper help = new Helper();

    // What will the total resource value of the lumber collection area be after 1000000000 minutes?
    public object PartTwo(string fileInput) => help.LumberCollection(fileInput, 1000000000);

    // What will the total resource value of the lumber collection area be after 10 minutes?
    public object PartOne(string fileInput) => help.LumberCollection(fileInput, 10);
}
