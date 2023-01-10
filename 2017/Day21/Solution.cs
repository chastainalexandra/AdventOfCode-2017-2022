using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day21;

[ProblemName("Fractal Art")]
class Solution : Solver {

    // public string GetName() => "Fractal Art"; have to move this to above with upgrade 

    // How many pixels stay on after 18 iterations?
    public object PartTwo(string fileData) => FindPixelIterationsCount(fileData, 18);

    // How many pixels stay on after 5 iterations?
    public object PartOne(string fileData) => FindPixelIterationsCount(fileData, 5);

    int FindPixelIterationsCount(string fileData, int iterations) {
        var pixels = Pixels.FromString(".#./..#/###"); // The image consists of a two-dimensional square grid of pixels that are either on (#) or off (.). The program always begins with this pattern:
        var rules = new PixelRules(fileData);
        for (var i = 0; i < iterations; i++) {
            pixels = rules.Apply(pixels);
        }
        return pixels.PixelCount();
    }
}


