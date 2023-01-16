using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Y2018.Day07;

[ProblemName("The Sum of Its Parts")]
class Solution : Solver {

    Helper help = new Helper();

    public object PartTwo(string fileInput) {
        var time = 0;
        var steps = help.GetSteps(fileInput);

        var works = new int[5];
        var items = new char[works.Length];

        while (steps.Any() || works.Any(work => work > 0)) {
            for (var i = 0; i < works.Length && steps.Any(); i++) {
                // start working
                if (works[i] == 0) {
                    char minKey = char.MaxValue;
                    foreach (var key in steps.Keys) {
                        if (steps[key].Count == 0) {
                            if (key < minKey) {
                                minKey = key;
                            }
                        }
                    }
                    if (minKey != char.MaxValue) {
                        works[i] = 60 + minKey - 'A' + 1;
                        items[i] = minKey;
                        steps.Remove(items[i]);
                    }
                }
            }

            time++;

            for (var i = 0; i < works.Length; i++) {
                if (works[i] == 0) {
                    // wait
                    continue;
                } else if (works[i] == 1) {
                    // finish
                    works[i]--;
                    foreach (var key in steps.Keys) {
                        steps[key].Remove(items[i]);
                    }

                } else if (works[i] > 0) {
                    // step
                    works[i]--;
                }
            }
        }

        return time;
    }

    // In what order should the steps in your instructions be completed?
    public object PartOne(string fileInput) {

        var sb = new StringBuilder();
        var steps = help.GetSteps(fileInput);
        while (steps.Any()) {
            char minKey = char.MaxValue;
            foreach (var key in steps.Keys) {
                if (steps[key].Count == 0) {
                    if (key < minKey) {
                        minKey = key;
                    }
                }
            }
            sb.Append(minKey);
            steps.Remove(minKey);
            foreach (var key in steps.Keys) {
                steps[key].Remove(minKey);
            }
        }
        return sb.ToString();
    }
}
