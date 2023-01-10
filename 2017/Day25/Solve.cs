using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day25;
public class Solve {
    public TurningMachine Parse(string fileData) {
        var fd = fileData.Split('\n').Where(fd => !string.IsNullOrEmpty(fd)).ToArray();
        int line = 0;

        TurningMachine turningMachine = new TurningMachine();

        String(@"Begin in state (\w).", out turningMachine.state);
        Int(@"Perform a diagnostic checksum after (\d+) steps.", out turningMachine.iterations);

        while (String(@"In state (\w):", out var state)) {
            while (Int(@"If the current value is (\d):", out var read)) {
                Int(@"- Write the value (\d).", out var write);
                String(@"- Move one slot to the (left|right).", out var dir);
                String(@" - Continue with state (\w).", out string newState);
                turningMachine.prg[(state, read)] = (write, dir == "left" ? -1 : 1, newState);
            }
        }

        bool Int(string pattern, out int r) {
            r = 0;
            return String(pattern, out string st) && int.TryParse(st, out r);
        }

        bool String(string pattern, out string st) {
            st = null;
            if (line >= fd.Length) {
                return false;
            }
            var m = Regex.Match(fd[line], pattern);
            if (m.Success) {
                line++;
                st = m.Groups[1].Value;
            }
            return m.Success;
        }

        return turningMachine;
    }
}