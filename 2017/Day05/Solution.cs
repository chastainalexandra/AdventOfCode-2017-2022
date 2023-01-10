using System;
using System.Linq;

namespace AdventOfCode.Y2017.Day05;

[ProblemName("A Maze of Twisty Trampolines, All Alike")]
class Solution : Solver {
    // public string GetName() => "A Maze of Twisty Trampolines, All Alike"; have to move this to above with upgrade 
    
    int GetStepsToExit(string fileData, Func<int, int> update) {
        var steps = fileData.Split('\n').Select(int.Parse).ToArray();
        var i = 0;
        var count = 0;
        while (i < steps.Length && i >= 0) {
            var jmp = steps[i];
            steps[i] = update(steps[i]);
            i += jmp;
            count++;
        }
        return count;
    }

    public object PartTwo(string fileData) => GetStepsToExit(fileData, fd => fd < 3 ? fd + 1 : fd - 1); // How many steps does it now take to reach the exit?

    public object PartOne(string fileData) => GetStepsToExit(fileData, fd => fd + 1); // How many steps does it take to reach the exit?
}
