using System.Linq;

namespace AdventOfCode.Y2017.Day02;

[ProblemName("Corruption Checksum")]
class Solution : Solver {
  // public string GetName() => "Corruption Checksum"; have to move this to above with upgrade 
    
    public object PartTwo(string fileData) {
        return (
            from fd in fileData.Split('\n')
            let spreadsheet = fd.Split('\t').Select(int.Parse)
            from s in spreadsheet
            from sh in spreadsheet
            where s > sh && s % sh == 0
            select s / sh
        ).Sum(); // What is the sum of each row's result in your puzzle input?
    }

    public object PartOne(string fileData) {
        return (
            from fd in fileData.Split('\n')
            let spreadsheet = fd.Split('\t').Select(int.Parse)
            select spreadsheet.Max() - spreadsheet.Min()
        ).Sum(); // What is the checksum for the spreadsheet in your puzzle input?
    }
}
