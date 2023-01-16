using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day22;

[ProblemName("Mode Maze")]
class Solution : Solver {
    Helper help = new Helper();

    public object PartTwo(string fileInput) {
        var (coordinateTargetX, coordinateTargetY, regionType) = help.RiskAndCoordinates(fileInput);
        var queue = new TheQueue<((int x, int y) position, Tool tool, int t)>();
        var visited = new HashSet<((int x, int y), Tool tool)>();

        IEnumerable<((int x, int y) position, Tool tool, int dt)> AdjacentRegion((int x, int y) position, Tool tool) {
            yield return regionType(position.x, position.y) switch {
                RegionType.Rocky => (position, tool == Tool.ClimbingGear ? Tool.Torch : Tool.ClimbingGear, 7),
                RegionType.Narrow => (position, tool == Tool.Torch ? Tool.Nothing : Tool.Torch, 7),
                RegionType.Wet => (position, tool == Tool.ClimbingGear ? Tool.Nothing : Tool.ClimbingGear, 7),
                _ => throw new ArgumentException()
            };

            foreach (var dx in new[] { -1, 0, 1 }) {
                foreach (var dy in new[] { -1, 0, 1 }) {
                    if (Math.Abs(dx) + Math.Abs(dy) != 1) {
                        continue;
                    }

                    var positionNew = (x: position.x + dx, y: position.y + dy);
                    if (positionNew.x < 0 || positionNew.y < 0) {
                        continue;
                    }

                    switch (regionType(positionNew.x, positionNew.y)) {
                        case RegionType.Rocky when tool == Tool.ClimbingGear || tool == Tool.Torch:
                        case RegionType.Narrow when tool == Tool.Torch || tool == Tool.Nothing:
                        case RegionType.Wet when tool == Tool.ClimbingGear || tool == Tool.Nothing:
                            yield return (positionNew, tool, 1);
                            break;
                    }
                }
            }
        }

        queue.Enqueue(0, ((0, 0), Tool.Torch, 0));

        while (queue.Any()) {
            var state = queue.Dequeue();
            var (position, tool, t) = state;

            if (position.x == coordinateTargetX && position.y == coordinateTargetY && tool == Tool.Torch) {
                return t;
            }

            var hash = (position, tool);
            if (visited.Contains(hash)) {
                continue;
            }

            visited.Add(hash);

            foreach( var (newPosition, newTool, dt) in AdjacentRegion(position, tool)) {
                queue.Enqueue(
                    t + dt + Math.Abs(newPosition.x - coordinateTargetX) + Math.Abs(newPosition.y - coordinateTargetY), 
                    (newPosition, newTool, t + dt)
                );
            }

        }

        throw new Exception();
    }

    // What is the total risk level for the smallest rectangle that includes 0,0 and the target's coordinates?
     public object PartOne(string fileInput) {
        var (coordinateTargetX, coordinateTargetY, regionType) = help.RiskAndCoordinates(fileInput);
        var riskLevel = 0;
        for (var y = 0; y <= coordinateTargetY; y++) {
            for (var x = 0; x <= coordinateTargetX; x++) {
                riskLevel += (int)regionType(x, y);
            }
        }
        return riskLevel;
    }   
}
