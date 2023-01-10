using System.Collections.Generic;

namespace AdventOfCode.Y2017.Day13;

public class FirewallLayers : System.Collections.Generic.List<(int depth, int range)>
{
    // You need to cross a vast firewall. 
    // The firewall consists of several layers, each with a security scanner that moves back and forth across the layer.
    //  To succeed, you must not be detected by a scanner.
    public FirewallLayers(IEnumerable<(int depth, int range)> layers) : base(layers) {
        // By studying the firewall briefly, you are able to record (in your puzzle input) 
        // the depth of each layer and the range of the scanning area for the scanner within it, written as depth: range.
    }
}