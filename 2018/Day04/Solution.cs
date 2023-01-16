using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day04;

[ProblemName("Repose Record")]
class Solution : Solver 
{
    Helper help = new Helper();

    // What is the ID of the guard you chose multiplied by the minute you chose?
    public object PartTwo(string fileInput) {
        var data = from schedule in help.GetSleepTimes(fileInput)
                group schedule by schedule.guardId into g
                select new { 
                    guard = g.Key, 
                    totalSleeps = g.Select(day => day.totalSleep).Sum(), 
                    sleepByMin = Enumerable.Range(0, 60).Select(minT => g.Sum(day => day.asleepAwake[minT])).ToArray()
                };

        var maxMaxSleep = data.Max(x => x.sleepByMin.Max());
        var fooT = data.Single(x => x.sleepByMin.Max() == maxMaxSleep);
        var min = Enumerable.Range(0, 60).Single(minT => fooT.sleepByMin[minT] == maxMaxSleep);

        return fooT.guard * min;
    }

    // What is the ID of the guard you chose multiplied by the minute you chose?
    public object PartOne(string fileInput) {
        var data = from schedule in help.GetSleepTimes(fileInput)
                group schedule by schedule.guardId into g
                select new { 
                    guard = g.Key, 
                    totalSleeps = g.Select(day => day.totalSleep).Sum(), 
                    sleepByMin = Enumerable.Range(0, 60).Select(minT => g.Sum(day => day.asleepAwake[minT])).ToArray()
                };
        var maxSleep = data.Max(x => x.totalSleeps);
        var sleep = data.Single(g => g.totalSleeps == maxSleep);
        var guardSleepByMin = Enumerable.Range(0, 60).Max(minT => sleep.sleepByMin[minT]);
        var min = Enumerable.Range(0, 60).Single(minT => sleep.sleepByMin[minT] == guardSleepByMin);
        return sleep.guard * min;
    }
}
