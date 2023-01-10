using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017.Day10;

[ProblemName("Knot Hash")]
class Solution : Solver {

    // public string GetName() => "Knot Hash"; have to move this to above with upgrade 

    int[] Calculate(IEnumerable<int> fileData, int rounds) {
        var output = Enumerable.Range(0, 256).ToArray(); // To achieve this, begin with a list of numbers from 0 to 255 However, you should instead use the standard list size of 256

        var current = 0;
        var skipSize = 0;
        for (var round = 0; round < rounds; round++) {
            foreach (var len in fileData) {
                for (int i = 0; i < len / 2; i++) {
                    var from = (current + i) % output.Length;
                    var to = (current + len - 1 - i) % output.Length;
                    (output[from], output[to]) = (output[to], output[from]);
                }
                current += len + skipSize;
                skipSize++;
            }
        }
        return output;
    }

    /*
     * Treating your puzzle input as a string of ASCII characters, what is the Knot Hash of your puzzle input? 
     * Ignore any leading or trailing whitespace you might encounter.
     **/ 
    public object PartTwo(string fileData) {
        var sequence = new [] { 17, 31, 73, 47, 23 }; // Once you have determined the sequence of lengths to use, add the following lengths to the end of the sequence: 17, 31, 73, 47, 23.
        var fd = fileData.ToCharArray().Select(b => (int)b).Concat(sequence);

        var rounds = Calculate(fd, 64); // Second, instead of merely running one round like you did above, run a total of 64 rounds, using the same length sequence in each round.

        /* 
        * Once the rounds are complete, you will be left with the numbers from 0 to 255 in some order, called the sparse hash. 
        * Your next task is to reduce these to a list of only 16 numbers called the dense hash.
        */
        return string.Join("", 
            from blockIdx in Enumerable.Range(0, 16)
            let block = rounds.Skip(16 * blockIdx).Take(16)
            select block.Aggregate(0, (acc, ch) => acc ^ ch).ToString("x2"));
    }

     // what is the result of multiplying the first two numbers in the list?
    public object PartOne(string fileData) {
        var chars = fileData.Split(',').Select(int.Parse);
        var hash = Calculate(chars, 1);
        return hash[0] * hash[1];
    }

}
