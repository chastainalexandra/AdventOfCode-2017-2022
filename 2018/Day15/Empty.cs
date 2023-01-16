using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day15;

class Empty : Square {
    public static readonly Empty square = new Empty();
    private Empty() { }
}