
namespace AdventOfCode.Y2018.Day09;

[ProblemName("Marble Mania")]
class Solution : Solver {

     Helper help = new Helper();

     // What would the new winning Elf's score be if the number of the last marble were 100 times larger?
    public object PartTwo(string fileInput) => help.GetElfScore(fileInput, 100);

    // What is the winning Elf's score?
    public object PartOne(string fileInput) => help.GetElfScore(fileInput, 1);
}

