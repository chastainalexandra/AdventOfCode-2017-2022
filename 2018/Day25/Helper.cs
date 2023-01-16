using System;
using System.Linq;


namespace AdventOfCode.Y2018.Day25;

public class Helper {
     public int Distance(int[] x, int[] y) => Enumerable.Range(0, x.Length).Select(i => Math.Abs(x[i] - y[i])).Sum();

    
}