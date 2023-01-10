using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day15;

[ProblemName("Dueling Generators")]
class Solution : Solver {

    // public string GetName() => "Dueling Generators"; have to move this to above with upgrade 


    (IEnumerable<long> a, IEnumerable<long> b) ParseGenerators(string fileData) {
        var fd = fileData.Split('\n');
        var generatorA = int.Parse(fd[0].Substring("Generator A starts with ".Length));
        var generatorB = int.Parse(fd[1].Substring("Generator B starts with ".Length));
        // (generator A uses 16807; generator B uses 48271),
        return (Generators(generatorA, 16807), Generators(generatorB, 48271));
    }

    IEnumerable<(long, long)> Add((IEnumerable<long> a, IEnumerable<long> b) items) =>
        Enumerable.Zip(items.a, items.b, (a, b) => (a, b));

    int Pairs(IEnumerable<(long a, long b)> items) =>
        items.Count(item => (item.a & 0xffff) == (item.b & 0xffff));

    /*The generators both work on the same principle. 
      To create its next value, a generator will take the previous value it produced, multiply it by a factor (generator A uses 16807; generator B uses 48271), 
      and then keep the remainder of dividing that resulting product by 2147483647. 
      That final remainder is the value it produces next.*/
    IEnumerable<long> Generators(int start, int mul) {
        var mod = 2147483647;

        long state = start;
        while (true) {
            state = (state * mul) % mod;
            yield return state;
        }
    }


    // After 5 million pairs, but using this new generator logic, what is the judge's final count?
    public object PartTwo(string fileData) {
        var generators = ParseGenerators(fileData);
        return Pairs(Add((generators.a.Where(a => (a & 3) == 0), generators.b.Where(a => (a & 7) == 0))).Take(5000000));
    }

    //After 40 million pairs, what is the judge's final count?
    public object PartOne(string fileData) =>
        Pairs(Add(ParseGenerators(fileData)).Take(40000000));
}
