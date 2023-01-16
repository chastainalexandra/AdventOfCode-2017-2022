using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day13;

[ProblemName("Mine Cart Madness")]
class Solution : Solver {

     Helper help = new Helper();

   
    // What is the location of the last cart at the end of the first tick where it is the only cart left?
    public object PartTwo(string fileInput) {
        var (dir, carts) = help.ReadInput(fileInput);
        while (carts.Count > 1) {
            var newDirection = help.Step(dir, carts);
            carts = newDirection.carts;
        }
        return help.CartPostion(carts[0]);
    }

    // After following their respective paths for a while, the carts eventually crash. To help prevent crashes, you'd like to know the location of the first crash.
    public object PartOne(string fileInput) {
        var (dir, carts) = help.ReadInput(fileInput);
        while (true) {
            var newDirection = help.Step(dir, carts);
            if (newDirection.crashed.Any()) {
                return help.CartPostion(newDirection.crashed[0]);
            }
        }
    }
}

