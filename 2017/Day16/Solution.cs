using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day16;

[ProblemName("Permutation Promenade")]
class Solution : Solver {

    // public string GetName() => "Permutation Promenade"; have to move this to above with upgrade 

    int Move(Func<string, string> step, string startingProgram) {
        var move = startingProgram;
        for (int i = 0; ; i++) {
            move = step(move);
            if (startingProgram == move) {
                return i + 1;
            }
        }
    }

    Func<string, string> DanceParse(string fileData) {
        var moves = (
            from fd in fileData.Split(',')
            select
                DanceMoves(fd, "p([a-z])/([a-z])", m => {
                    var (c1, c2) = (m[0].Single(), m[1].Single());
                    return order => {
                        var (idx1, idx2) = (order.IndexOf(c1), order.IndexOf(c2));
                        order[idx1] = c2;
                        order[idx2] = c1;
                        return order;
                    };
                }) ??
                DanceMoves(fd, "x([0-9]+)/([0-9]+)", m => {
                    int idx1 = int.Parse(m[0]);
                    int idx2 = int.Parse(m[1]);
                    return (order) => {
                        (order[idx1], order[idx2]) = (order[idx2], order[idx1]);
                        return order;
                    };
                }) ??
                DanceMoves(fd, "s([0-9]+)", m => {
                    int n = int.Parse(m[0]);
                    return (order) => {
                        return order.Skip(order.Count - n).Concat(order.Take(order.Count - n)).ToList();
                    };
                }) ??
                throw new Exception("Cannot parse " + fd)
        ).ToArray();

        return startOrder => {
            var order = startOrder.ToList();
            foreach (var move in moves) {
                order = move(order);
            }
            return string.Join("", order);
        };
    }

    Func<List<char>, List<char>> DanceMoves(string stm, string pattern, Func<string[], Func<List<char>, List<char>>> a) {
        var match = Regex.Match(stm , pattern);
        if (match.Success) {
            return a(match.Groups.Cast<Group>().Skip(1).Select(g => g.Value).ToArray());
        } else {
            return null;
        }
    }

    // In what order are the programs standing after their billion dances?
    public object PartTwo(string fileData) {
        var step = DanceParse(fileData);
        var startingProgram = "abcdefghijklmnop";

        var move = Move(step, startingProgram);

        var startingDance = startingProgram;
        for (int i = 0; i < 1000000000 % move; i++) {
            startingDance = step(startingDance);
        }
        return startingDance;
    }

    //You watch the dance for a while and record their dance moves (your puzzle input). In what order are the programs standing after their dance?
    public object PartOne(string fileData) {
        return DanceParse(fileData)("abcdefghijklmnop"); 
        // There are sixteen programs in total, named a through p. 
        // They start by standing in a line: a stands in position 0, b stands in position 1, and so on until p, which stands in position 15.
    }
}
