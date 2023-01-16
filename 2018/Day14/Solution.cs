using System.Linq;

namespace AdventOfCode.Y2018.Day14;

[ProblemName("Chocolate Charts")]
class Solution : Solver {

     Helper help = new Helper();

    //How many recipes appear on the scoreboard to the left of the score sequence in your puzzle input?

    public object PartTwo(string fileInput) => help.GetRecipes(fileInput.Length).First(item => item.st == fileInput).i;

    // What are the scores of the ten recipes immediately after the number of recipes in your puzzle input?
    public object PartOne(string fileInput) => help.GetRecipes(10).ElementAt(int.Parse(fileInput)).st;

    
}
