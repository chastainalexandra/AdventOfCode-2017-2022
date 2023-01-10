using System.Collections.Generic;

namespace AdventOfCode.Y2017.Day25;

public class TurningMachine {
        public string state;
        public int iterations;
        public Dictionary<(string state, int read), (int write, int dir, string state)> prg =
            new Dictionary<(string, int), (int, int, string)>();   
    }  