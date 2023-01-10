using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day24;

public class Solve {

    public HashSet<Component> Parse(string fileData) {
        var components = new HashSet<Component>();
        foreach (var fd in fileData.Split('\n')) {
            var lines = fd.Split('/');
            components.Add(new Component { Port1 = int.Parse(lines[0]), Port2 = int.Parse(lines[1]) });
        }
        return components;
    }
    public int Bridge(string fileData, Func<(int length, int strength), (int length, int strength), int> compare) {

        (int length, int strength) fold(int portIn, HashSet<Component> components) {
            var strongest = (0, 0);
            foreach (var component in components.ToList()) {
                var portOut =
                    portIn == component.Port1 ? component.Port2 :
                    portIn == component.Port2 ? component.Port1 :
                     -1;

                if (portOut != -1) {
                    components.Remove(component);
                    var curr = fold(portOut, components);
                    (curr.length, curr.strength) = (curr.length + 1, curr.strength + component.Port1 + component.Port2);
                    strongest = compare(curr, strongest) > 0 ? curr : strongest;
                    components.Add(component);
                }
            }
            return strongest;
        }
        return fold(0, Parse(fileData)).strength;
    }

  
}