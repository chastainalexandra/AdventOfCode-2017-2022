using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day12;

[ProblemName("Digital Plumber")]
class Solution : Solver {

    // public string GetName() => "Digital Plumber"; have to move this to above with upgrade 

    IEnumerable<HashSet<string>> GetPrograms(string fileData) {
        var nodes = Parse(fileData);
        var n = new Dictionary<string, string>();

        string getProgram(string id) {
            var root = id;
            while (n.ContainsKey(root)) {
                root = n[root];
            }
            return root;
        }

        foreach (var node in nodes) {
            var rootA = getProgram(node.Id);
            foreach (var no in node.Pipes) {
                var rootB = getProgram(no);
                if (rootB != rootA) {
                    n[rootB] = rootA;
                }
            }
        }

        return
            from node in nodes
            let root = getProgram(node.Id)
            group node.Id by root into partitions
            select new HashSet<string>(partitions.ToArray());
        
    }

    List<Node> Parse(string fileData) {
        return (
            from fd in fileData.Split('\n')
            let programs = Regex.Split(fd, " <-> ")
            select new Node() {
                    Id = programs[0],
                    Pipes = new List<string>(Regex.Split(programs[1], ", "))
                }
        ).ToList();
    }

    // How many groups are there in total?
    public object PartTwo(string fileData) => GetPrograms(fileData).Count();

    // How many programs are in the group that contains program ID 0?
    public object PartOne(string fileData) => GetPrograms(fileData).Single(x => x.Contains("0")).Count();
}
