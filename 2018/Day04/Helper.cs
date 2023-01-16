using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day04;

public class Helper 
{
    
    public IEnumerable<SleepTimes> GetSleepTimes(string fileInput) {

        bool Int(string line, out int r) {
            r = 0;
            return String(line, out string st) && int.TryParse(st, out r);
        }

        bool Date(string line, out DateTime r) {
            r = DateTime.MinValue;
            return String(line, out string st) && DateTime.TryParse(st, out r);
        }

        var fl = fileInput.Split("\n").ToList();
        fl.Sort((x, y) => DateTime.Parse(x.Substring(1, "1518-03-25 00:01".Length)).CompareTo(DateTime.Parse(y.Substring(1, "1518-03-25 00:01".Length))));
        var iline = 0;

        while (Int(@"Guard #(\d+) begins shift", out var guard)) {

            var sleep = new int[60]; // only the minute portion (00 - 59) is relevant for those events.
            while (Date(@"\[(.*)\] falls asleep", out var fallsAsleap)) {
                Date(@"\[(.*)\] wakes up", out var wakesUp);

                var from = fallsAsleap.Hour != 0 ? 0 : fallsAsleap.Minute;
                var to = wakesUp.Hour != 0 ? 0 : wakesUp.Minute;

                for (var min = from; min < to; min++) {
                    sleep[min] = 1;
                }
            }

            yield return new SleepTimes() { guardId = guard, asleepAwake = sleep };
        }

        if (iline != fl.Count) {
            throw new Exception();
        }

        bool String(string pattern, out string st) {
            st = null;
            if (iline >= fl.Count) {
                return false;
            }
            var m = Regex.Match(fl[iline], pattern);
            if (m.Success) {
                iline++;
                st = m.Groups[1].Value;
            }
            return m.Success;
        }
    }
}
