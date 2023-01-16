using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day15;

[ProblemName("Beverage Bandits")]
class Solution : Solver {

    Helper help = new Helper();

    // After increasing the Elves' attack power until it is just barely enough for them to win without any Elves dying, 
    // what is the outcome of the combat described in your puzzle input?
    public object PartTwo(string fileInput) {
        // you need to find the outcome of the battle in which the Elves 
        // have the lowest integer attack power (at least 4) 
        // that allows them to win without a single death. 
        var elfAttachPower = 4;
        while (true) {
            var outcome = help.GetGameOutcome(fileInput, 3, elfAttachPower, false);
            if (outcome.didElfDie) {
                return outcome.score;
            }
            elfAttachPower++;
        }
    }

    // What is the outcome of the combat described in your puzzle input?
    public object PartOne(string fileInput) {
        return help.GetGameOutcome(fileInput, 3, 3, false).score;
    }
}