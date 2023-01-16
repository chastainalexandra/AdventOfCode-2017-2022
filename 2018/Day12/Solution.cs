using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day12;

[ProblemName("Subterranean Sustainability")]
class Solution : Solver {

     Helper help = new Helper();

    // After fifty billion (50000000000) generations, what is the sum of the numbers of all pots which contain a plant?
    public object PartTwo(string fileInput) => help.SubterraneanSustainability(fileInput, 50000000000);

    // After 20 generations, what is the sum of the numbers of all pots which contain a plant?
    public object PartOne(string fileInput) => help.SubterraneanSustainability(fileInput, 20);

}


