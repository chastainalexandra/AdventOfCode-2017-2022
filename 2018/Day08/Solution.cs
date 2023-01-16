using System;
using System.Linq;

namespace AdventOfCode.Y2018.Day08;

[ProblemName("Memory Maneuver")]
class Solution : Solver {

     Helper help = new Helper();    

    public object PartTwo(string fileInput) {
        return help.GetMetadatEntries(fileInput).value();
    }
    // What is the sum of all metadata entries
    public object PartOne(string fileInput) =>
        help.GetMetadatEntries(fileInput).fold(0, (cur, node) => cur + node.metadataEntries.Sum()); // The first check done on the license file is to simply add up all of the metadata entries.

}
