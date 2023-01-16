using System;
using System.Linq;

namespace AdventOfCode.Y2018.Day08;

public class Helper {
     public Node GetMetadatEntries(string fileInput) {
        var fl = fileInput.Split(" ").Select(int.Parse).GetEnumerator();
        Func<int> next = () => {
            fl.MoveNext();
            return fl.Current;
        };

        Func<Node> read = null;
        read = () => {
            var node = new Node() {
                childNode = new Node[next()],
                metadataEntries = new int[next()]
            };
            for (var i = 0; i < node.childNode.Length; i++) {
                node.childNode[i] = read();
            }
            for (var i = 0; i < node.metadataEntries.Length; i++) {
                node.metadataEntries[i] = next();
            }
            return node;
        };
        return read();
    }
    
}