using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day07;

[ProblemName("Recursive Circus")]
class Solution : Solver {

    // public string GetName() => "Recursive Circus"; have to move this to above with upgrade 

    Tree Parse(string fileData) {
        var tree = new Tree();
        foreach (var line in fileData.Split('\n')) {
            var parts = Regex.Match(line, @"(?<id>[a-z]+) \((?<weight>[0-9]+)\)( -> (?<child>.*))?");

            tree.Add(
                parts.Groups["id"].Value,
                new Node {
                    Id = parts.Groups["id"].Value,
                    Weight = int.Parse(parts.Groups["weight"].Value),
                    Child = string.IsNullOrEmpty(parts.Groups["child"].Value)
                        ? new string[0]
                        : Regex.Split(parts.Groups["child"].Value, ", "),
                });
        }
        return tree;
    }

    Node Root(Tree tree) =>
        tree.Values.First(node => !tree.Values.Any(nodeParent => nodeParent.Child.Contains(node.Id)));

    int GetWeight(Node node, Tree tree) {
        node.WeightOfTree = node.Weight + node.Child.Select(childId => GetWeight(tree[childId], tree)).Sum();
        return node.WeightOfTree;
    }

    Node Child(Node node, Tree tree) {
        var w =
            (from childId in node.Child
             let child = tree[childId]
             group child by child.WeightOfTree into childernWeight
             orderby childernWeight.Count()
             select childernWeight).ToArray();

        return w.Length == 1 ? null : w[0].Single();
    }

    int Calc(Node node, int desiredWeight, Tree tree) {
        if (node.Child.Length < 2) {
            throw new NotImplementedException();
        } 

        var kido = Child(node, tree);

        if (kido == null) {
            return desiredWeight - node.WeightOfTree + node.Weight;
        } else {
            desiredWeight = desiredWeight - node.WeightOfTree + kido.WeightOfTree;
            return Calc(kido, desiredWeight, tree);
        }
    }

    public object PartOne(string fileData) => Root(Parse(fileData)).Id;

    public object PartTwo(string fileData) {
        var tree = Parse(fileData);
        var root = Root(tree);
        GetWeight(root, tree);
        var kido = Child(root, tree);
        var desiredWeight = tree[root.Child.First(childId => childId != kido.Id)].WeightOfTree;
        return Calc(kido, desiredWeight, tree);
    }

}
