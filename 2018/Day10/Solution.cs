using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day10;

[ProblemName("The Stars Align")]
class Solution : Solver {

     Helper help = new Helper();

     //  exactly how many seconds would they have needed to wait for that message to appear?
    public object PartTwo(string fileInput) => help.GetMessage(fileInput).seconds;

    // What message will eventually appear in the sky?
     public object PartOne(string fileInput) => help.GetMessage(fileInput).st.Msg();

    
}
