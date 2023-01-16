using System;
using System.Linq;

namespace AdventOfCode.Y2018.Day06;

[ProblemName("Chronal Coordinates")]
class Solution : Solver {
    Helper help = new Helper();

    // What is the size of the largest area that isn't infinite?
    public object PartTwo(string fileInput) {
        var coordinates = help.GetCoordinates(fileInput);

        var X = coordinates.Min(coord => coord.x) - 1;
        var XX = coordinates.Max(coord => coord.x) + 1;
        var Y = coordinates.Min(coord => coord.y) - 1;
        var YY = coordinates.Max(coord => coord.y) + 1;


        var area = 0;

        foreach (var x in Enumerable.Range(X, XX - X + 1)) {
            foreach (var y in Enumerable.Range(Y, YY - X + 1)) {
                var d = coordinates.Select(coord => help.ManhattanDistance((x, y), coord)).Sum();
                if (d < 10000)
                    area++;
            }
        }
        return area;
    }

    // What is the size of the largest area that isn't infinite?
    public object PartOne(string fileInput) {
        var coordinates = help.GetCoordinates(fileInput);

        var X = coordinates.Min(coord => coord.x) - 1;
        var XX = coordinates.Max(coord => coord.x) + 1;
        var Y = coordinates.Min(coord => coord.y) - 1;
        var YY = coordinates.Max(coord => coord.y) + 1;

        var area = new int[coordinates.Length];

        foreach (var x in Enumerable.Range(X, XX - X + 1)) {
            foreach (var y in Enumerable.Range(Y, YY - XX + 1)) {
                var d = coordinates.Select(coord => help.ManhattanDistance((x, y), coord)).Min();
                var closest = Enumerable.Range(0, coordinates.Length).Where(i => help.ManhattanDistance((x, y), coordinates[i]) == d).ToArray();

                if (closest.Length != 1) {
                    continue;
                }

                if (x == X || x == XX || y == Y || y == YY) {
                    foreach (var icoord in closest) {
                        if (area[icoord] != -1) {
                            area[icoord] = -1;
                        }
                    }
                } else {
                    foreach (var icoord in closest) {
                        if (area[icoord] != -1) {
                            area[icoord]++;
                        }
                    }
                }
            }
        }
        return area.Max();
    }
}
