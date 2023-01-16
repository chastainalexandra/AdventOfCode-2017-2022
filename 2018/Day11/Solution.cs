namespace AdventOfCode.Y2018.Day11;

[ProblemName("Chronal Charge")]
class Solution : Solver {

     Helper help = new Helper();

    public object PartTwo(string fileInput) {
        var res =  help.GetCoordinate(int.Parse(fileInput), 300);
        return $"{res.x},{res.y},{res.d}";
    }

    // What is the X,Y coordinate of the top-left fuel cell of the 3x3 square with the largest total power?
    public object PartOne(string fileInput) {
        var res = help.GetCoordinate(int.Parse(fileInput), 3);
        return $"{res.x},{res.y}";
    }

}
