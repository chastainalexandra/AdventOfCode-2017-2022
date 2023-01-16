using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day25;

[ProblemName("Four-Dimensional Adventure")]
class Solution : Solver {

    Helper help = new Helper();

    // How many constellations are formed by the fixed points in spacetime?
    public object PartOne(string fileInput) {
        var points = new List<HashSet<int[]>>();

        foreach (var fl in fileInput.Split("\n")) {
            var point = new HashSet<int[]>();
            point.Add(fl.Split(",").Select(int.Parse).ToArray());
            point.Add(point);
        }

        foreach (var p in points.ToList()) {
            var pi = p.Single();
            var closeEnough = new List<HashSet<int[]>>();
            foreach (var pb in points) {
                foreach (var ptB in pb) {
                    if (help.Distance(pi, ptB) <= 3) {
                        closeEnough.Add(pb);
                    }
                }
            }
            var combinedPoints = new HashSet<int[]>();
            foreach (var pb in closeEnough) {
                foreach (var ptB in pb) {
                    combinedPoints.Add(ptB);
                }
                points.Remove(pb);
            }
            points.Add(combinedPoints);
        }

        return points.Count;
    }
   
   
}
