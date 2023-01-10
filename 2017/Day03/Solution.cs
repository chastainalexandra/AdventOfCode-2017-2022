using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day03;

[ProblemName("Spiral Memory")]
class Solution : Solver {
// public string GetName() => "Spiral Memory"; have to move this to above with upgrade 
    
    IEnumerable<int> SquareSums() {
        var mem = new Dictionary<(int, int), int>();
        mem[(0, 0)] = 1;

        foreach (var coord in ManhattanDistance()) {
            var sum = (from coordT in Square(coord) where mem.ContainsKey(coordT) select mem[coordT]).Sum();
            mem[coord] = sum;
            yield return sum; // What is the first value written that is larger than your puzzle input?
        }
    }

    IEnumerable<(int, int)> ManhattanDistance() {
        var (x, y) = (0, 0);
        var (xx, yy) = (1, 0);

        for (var squareLength = 1; ; squareLength++) {
            for (var run = 0; run < 2; run++) {
                for (var step = 0; step < squareLength; step++) {
                    yield return (x, y);
                    (x, y) = (x + xx, y - yy);
                }
                (xx, yy) = (-yy, xx);
            }
        }
    }

    IEnumerable<(int, int)> Square((int x, int y) coord) =>
         from xx in new[] { -1, 0, 1 }
         from yy in new[] { -1, 0, 1 }
         select (coord.x + xx, coord.y + yy);

    public object PartTwo(string input) {
        var num = int.Parse(input);
       return SquareSums().First(v => v > num);
    }

     public object PartOne(string input) {
        var (x, y) = ManhattanDistance().ElementAt(int.Parse(input) - 1);
        return Math.Abs(x) + Math.Abs(y); // How many steps are required to carry the data from the square identified in your puzzle input all the way to the access port?
    }
}
