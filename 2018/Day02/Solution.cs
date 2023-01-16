using System.Linq;

namespace AdventOfCode.Y2018.Day02;

[ProblemName("Inventory Management System")]
class Solution : Solver {

    Helper help = new Helper();

    // What letters are common between the two correct box IDs? 
    // (In the example above, this is found by removing the differing character from either ID, producing fgij.)
    public object PartTwo(string fileInput) {
        var fi = fileInput.Split("\n");
        return (from x in Enumerable.Range(0, fi.Length)
                from y in Enumerable.Range(x + 1, fi.Length - x - 1)
                let fl1 = fi[x]
                let fl2 = fi[y]
                where help.FindDifferentIDs(fl1, fl2) == 1
                select help.CommonIDs(fl1, fl2)
        ).Single();
    }

    // What is the checksum for your list of box IDs?
    public object PartOne(string fileInput) {
        var doubleIDs = (
            from fi in fileInput.Split("\n")
            where help.Validate(fi, 2) // // Of these box IDs, four of them contain a letter which appears exactly twice
            select fi
        ).Count();
        var thrippleIds = (
            from fi in fileInput.Split("\n")
            where help.Validate(fi, 3) //  and three of them contain a letter which appears exactly three times.
            select fi
        ).Count();
        return doubleIDs * thrippleIds; //   //  Multiplying these together produces a checksum of 4 * 3 = 12.
    }
}
