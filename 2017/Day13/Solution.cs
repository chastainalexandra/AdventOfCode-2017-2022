using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day13;

[ProblemName("Packet Scanners")]
class Solution : Solver {

    // public string GetName() => "Packet Scanners"; have to move this to above with upgrade 

    IEnumerable<int> FirewallSeverity(FirewallLayers firewallLayers, int layer) {
        var packet = 0;
        foreach (var fl in firewallLayers) {
            layer += fl.depth - packet;
            packet = fl.depth;
            var securityScanner = layer % (2 * fl.range - 2);
            if (securityScanner == 0) {
                yield return fl.depth * fl.range;
            }
        }
    }

    FirewallLayers Parse(string fileData) =>
        new FirewallLayers(
            from fd in fileData.Split('\n')
            let lines = Regex.Split(fd, ": ").Select(int.Parse).ToArray()
            select (lines[0], lines[1])
        );

    // What is the fewest number of picoseconds that you need to delay the packet to pass through the firewall without being caught?
    public object PartTwo(string fileData) {
        var layers = Parse(fileData);
        return Enumerable
            .Range(0, int.MaxValue)
            .First(n => !FirewallSeverity(layers, n).Any());
    }

    //Given the details of the firewall you've recorded, if you leave immediately, what is the severity of your whole trip?
     public object PartOne(string fileData) => FirewallSeverity(Parse(fileData), 0).Sum();
}
