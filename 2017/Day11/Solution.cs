using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day11;

[ProblemName("Hex Ed")]
class Solution : Solver {

    // public string GetName() => "Hex Ed"; have to move this to above with upgrade 

    IEnumerable<int> Directions(string fileData) => 
        from dir in Direction(fileData) select (Math.Abs(dir.x) + Math.Abs(dir.y) + Math.Abs(dir.z))/2;

    IEnumerable<(int x, int y, int z)> Direction(string fileData) {
        var (x, y, z) = (0, 0, 0);
        foreach (var dir in fileData.Split(',')) {
            switch (dir) {
                case "n": 
                    (x, y, z) = (x + 0, y + 1, z - 1); 
                    break;
                case "s":  
                    (x, y, z) = (x + 0, y - 1, z + 1); 
                    break;
                case "nw": 
                    (x, y, z) = (x - 1, y + 1, z + 0); 
                    break;
                 case "ne": 
                    (x, y, z) = (x + 1, y + 0, z - 1); 
                    break;
                case "se": 
                    (x, y, z) = (x + 1, y - 1, z + 0);
                     break;
                 case "sw": 
                    (x, y, z) = (x - 1, y + 0, z + 1);
                    break;
                default: throw new ArgumentException(dir);
            }
            yield return (x, y, z);
        }
    }


    /*How many steps away is the furthest he ever got from his starting position?*/
    public object PartTwo(string fileData) => Directions(fileData).Max();

    /*
    * You have the path the child process took. 
    * Starting where he started, you need to determine the fewest number of steps required to reach him. 
    * (A "step" means to move from the hex you are in to any adjacent hex.)
    */
    public object PartOne(string fileData) => Directions(fileData).Last();

   
}
