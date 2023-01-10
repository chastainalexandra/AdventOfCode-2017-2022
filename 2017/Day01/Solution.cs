using System.Linq;

namespace AdventOfCode.Y2017.Day01;

[ProblemName("Inverse Captcha")]
class Solution : Solver {
    // public string GetName() => "Inverse Captcha"; have to move this to above with upgrade 
    public object PartOne(string input) => FirstDay(input, 1);

    public object PartTwo(string input) => FirstDay(input, input.Length / 2);

    int FirstDay(string fileData, int x) {
        return (
            from fd in Enumerable.Range(0, fileData.Length)
            where fileData[fd] == fileData[(fd + x) % fileData.Length]
            select int.Parse(fileData[fd].ToString())
        ).Sum();
    }
}
