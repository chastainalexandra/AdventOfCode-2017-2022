using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day17;

[ProblemName("Reservoir Research")]
class Solution : Solver {
    Helper help = new Helper();
    
    // How many water tiles are left after the water spring stops producing water and all remaining water not at rest has drained?
    public object PartTwo(string fileInput) => Regex.Matches(help.FillSpring(fileInput), "[~]").Count;

    // How many tiles can the water reach within the range of y values in your scan?
    public object PartOne(string fileInput) => Regex.Matches(help.FillSpring(fileInput), "[~|]").Count;
}
