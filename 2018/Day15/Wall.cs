using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day15;


class Wall : Square {
    public static readonly Wall Square = new Wall();
    private Wall() { }
}