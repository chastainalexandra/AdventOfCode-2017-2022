using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day09;

[ProblemName("Stream Processing")]
class Solution : Solver {

    // public string GetName() => "Stream Processing"; have to move this to above with upgrade 

    IEnumerable<int> Score(string fileData) {
        var score = 0;
        foreach (var fd in IdentifyGarbage(fileData).Where((x) => !x.garbage).Select(x => x.fd)) {
            if (fd == '}') {
                score--;
            } else if (fd == '{') {
                score++;
                yield return score;
            }
        }
    }

    // Garbage begins with < and ends with >
    // Between those angle brackets, almost any character can appear, including { and }. Within garbage, < has no special meaning.

    /*
        Here are some self-contained pieces of garbage:

        <>, empty garbage.
        <random characters>, garbage containing random characters.
        <<<<>, because the extra < are ignored.
        <{!>}>, because the first > is canceled.
        <!!>, because the second ! is canceled, allowing the > to terminate the garbage.
        <!!!>>, because the second ! and the first > are canceled.
        <{o"i!a,<{i<a>, which ends at the first >.
    */
    IEnumerable<(char fd, bool garbage)> IdentifyGarbage(string fileData) {
        var notGarbage = false;
        var isGarbage = false;
        foreach (var fd in fileData) {
            if (isGarbage) {
                if (notGarbage) {
                    notGarbage = false;
                } else {
                    if (fd == '>') {
                        isGarbage = false;
                    } else if (fd == '!') { //  any character that comes after ! should be ignored, including <, >, and even another !.
                        notGarbage = true;
                    } else {
                        yield return (fd, isGarbage);
                    }
                }
            } else {
                if (fd == '<') {
                    isGarbage = true;
                } else {
                    yield return (fd, isGarbage);
                }
            }
        }
    }

    public object PartTwo(string fileData) => IdentifyGarbage(fileData).Where((x) => x.garbage).Count();
    public object PartOne(string fileData) => Score(fileData).Sum();
}
