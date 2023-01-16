using System.Linq;

namespace AdventOfCode.Y2018.Day02;

public class Helper 
{
    public bool Validate(string fileLine, int number) {
        return (from fl in fileLine
                group fl by fl into g
                select g.Count()).Any(cch => cch == number);
    }

    public string CommonIDs(string fileLine1, string fileLine2) {
        return string.Join("", fileLine1.Zip(fileLine2, (f1, f2) => f1 == f2 ? f1.ToString() : ""));
    }

    // the boxes will have IDs which differ by exactly one character at the same position in both strings. 
    public int FindDifferentIDs(string fileLine1, string fileLine2) {
        return fileLine1.Zip(fileLine2, 
            (f1, f2) => f1 == f2
        ).Count(x => x == false);
    }

}