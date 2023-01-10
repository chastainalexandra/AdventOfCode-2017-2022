using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day08;

[ProblemName("I Heard You Like Registers")]
class Solution : Solver {

    // public string GetName() => "I Heard You Like Registers"; have to move this to above with upgrade 
 
    (int highest, int largest) Run(string fileData) {
        var register = new Dictionary<string, int>();
        var highest = 0;
        foreach (var fd in fileData.Split('\n')) {
            
            var words = fd.Split(' ');
            var (registerDst, op, num, registerConditions, condition, conditionNumber) = (words[0], words[1], int.Parse(words[2]), words[4], words[5], int.Parse(words[6]));
            if (!register.ContainsKey(registerDst)) {
                register[registerDst] = 0;
            }
            if (!register.ContainsKey(registerConditions)) {
                register[registerConditions] = 0;
            }

            var conditions = condition switch {
                ">"  => register[registerConditions] > conditionNumber,
                "<"  => register[registerConditions] < conditionNumber,
                "==" => register[registerConditions] == conditionNumber,
                "!=" => register[registerConditions] != conditionNumber,
                ">=" => register[registerConditions] >= conditionNumber,
                "<=" => register[registerConditions] <= conditionNumber,
                _ => throw new NotImplementedException(condition)
            };
            if (conditions) {
                register[registerDst] += 
                    op == "inc" ? num :
                    op == "dec" ? -num :
                    throw new NotImplementedException(op);
            }
            highest = Math.Max(highest, register[registerDst]);
        }
        return (highest, register.Values.Max());
    }

    public object PartTwo(string fileData) => Run(fileData).highest;

    public object PartOne(string fileData) => Run(fileData).largest;

}
