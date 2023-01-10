// borrowed from https://github.com/encse/adventofcode
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using AngleSharp.Dom;

namespace AdventOfCode.Model;

class CalendarToken {
    public string Text { get; set; }
    public string RgbaColor { get; set; }
    public int ConsoleColor { get; set; }
    public bool Bold { get; set; }
}