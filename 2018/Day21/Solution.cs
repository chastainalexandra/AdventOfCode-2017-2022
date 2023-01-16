using System.Linq;

namespace AdventOfCode.Y2018.Day21;

[ProblemName("Chronal Conversion")]
class Solution : Solver {

    Helper help = new Helper();

    // What is the lowest non-negative integer value for register 0 that causes the program to halt after executing the most instructions?
    public object PartTwo(string fileInput) => help.Run("two", fileInput).Last();
    
    // What is the lowest non-negative integer value for register 0 
    // that causes the program to halt after executing the fewest instructions? 
    // (Executing the same instruction multiple times counts as multiple instructions executed.)
    public object PartOne(string fileInput) => help.Run("one", fileInput).First();
}
