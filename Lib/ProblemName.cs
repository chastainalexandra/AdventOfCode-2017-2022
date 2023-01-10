// borrowed from https://github.com/encse/adventofcode
using System;

namespace AdventOfCode;

class ProblemName : Attribute {
    public readonly string Name;
    public ProblemName(string name) {
        this.Name = name;
    }
}