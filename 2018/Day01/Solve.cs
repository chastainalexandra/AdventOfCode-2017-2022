using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day01;

public class Solve {
    public IEnumerable<int> Frequencies(string fileInput) {
        var fd = 0;
        while (true) {
            foreach (var frequency in fileInput.Split("\n").Select(int.Parse)) {
                fd += frequency;
                yield return fd;
            }
        }
    }
}