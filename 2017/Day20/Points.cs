using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day20;

class Points {
    public int x;
    public int y;
    public int z;

    public int Len() => Math.Abs(x) + Math.Abs(y) + Math.Abs(z);

    public Points (int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}