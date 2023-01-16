using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day23;

[ProblemName("Experimental Emergency Teleportation")]
class Solution : Solver {

     Helper help = new Helper();

    // Find the coordinates that are in range of the largest number of nanobots.
    // What is the shortest manhattan distance between any of those points and 0,0,0?
    public object PartTwo(string fileInput) {
        var nanoBots = help.ExperimentalEmergencyTeleportation(fileInput);
        var x = nanoBots.Select(nano => nano.position.x).Min();
        var y = nanoBots.Select(nano => nano.position.y).Min();
        var z = nanoBots.Select(nano => nano.position.z).Min();

        var maxX = nanoBots.Select(nano => nano.position.x).Max();
        var maxY = nanoBots.Select(nano => nano.position.y).Max();
        var maxZ = nanoBots.Select(nano => nano.position.z).Max();

        return help.FindDistance(new Formation((x, y, z), (maxX - x + 1, maxY - y + 1, maxZ - z + 1)), nanoBots).pt;
    }


    // Find the nanobot with the largest signal radius. How many nanobots are in range of its signals?
    public object PartOne(string fileInput) {
        var nanoBots = help.ExperimentalEmergencyTeleportation(fileInput);
        var signalRange = nanoBots.Select(drone => drone.r).Max();
        var maxNanoRange = nanoBots.Single(drone => drone.r == signalRange);
        return nanoBots.Count(bots => help.Distance(bots.position, maxNanoRange.position) <= signalRange);
    }

}

