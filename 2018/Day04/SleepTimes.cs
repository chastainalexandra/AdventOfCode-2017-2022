using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day04;
public class SleepTimes {
    public int guardId;
    public int[] asleepAwake;
    public int totalSleep => asleepAwake.Sum();
}
