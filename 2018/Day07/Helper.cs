using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day07;

public class Helper {
    
    public Dictionary<char, List<char>> GetSteps(string fileInput) {
        var dict = (
            from fl in fileInput.Split("\n")
            let steps = fl.Split(" ")
            let step = steps[7][0]
            let stepsOn = steps[1][0]
            group stepsOn by step into g
            select g
        ).ToDictionary(g => g.Key, g => g.ToList());

        foreach (var key in new List<char>(dict.Keys)) {
            foreach (var d in dict[key]) {
                if (!dict.ContainsKey(d)) {
                    dict.Add(d, new List<char>());
                }
            }
        }
        return dict;
    }
}