using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day06;

[ProblemName("Memory Reallocation")]
class Solution : Solver {
    // public string GetName() => "Memory Reallocation"; have to move this to above with upgrade 

    List<int> Parse(string input) => input.Split('\t').Select(int.Parse).ToList();

    int GetBlockCount(List<int> numbers) {
        var blockCount = 0;
        var seen = new HashSet<string>();
        while (true) {
            var key = string.Join(";", numbers.Select(x => x.ToString()));
            if (seen.Contains(key)) {
                return blockCount;
            }
            seen.Add(key);
            RedistributeCycles(numbers);
            blockCount++;
        }
    }

    void RedistributeCycles(List<int> numbers) {
        var max = numbers.Max();
        var i = numbers.IndexOf(max);
        numbers[i] = 0;
        while (max > 0) {
            i++;
            numbers[i % numbers.Count]++;
            max--;
        }
    }


    public object PartTwo(string fileData) {
        var numbers = Parse(fileData);
        GetBlockCount(numbers);
        return GetBlockCount(numbers); // How many cycles are in the infinite loop that arises from the configuration in your puzzle input?
    }
    public object PartOne(string fileData) => GetBlockCount(Parse(fileData)); 
    // Given the initial block counts in your puzzle input, how many redistribution cycles must be completed before a configuration is produced that has been seen before?
}
