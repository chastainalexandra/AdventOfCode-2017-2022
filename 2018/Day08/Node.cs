using System;
using System.Linq;

namespace AdventOfCode.Y2018.Day08;
public class Node 
{
    // Specifically, a node consists of:
    // A header, which is always exactly two numbers:
    // The quantity of child nodes.
    // The quantity of metadata entries.
    // Zero or more child nodes (as specified in the header).
    // One or more metadata entries (as specified in the header).
    public Node[] childNode;
    public int[] metadataEntries;
    public T fold<T>(T seed, Func<T, Node, T> aggregate) {
        return childNode.Aggregate(aggregate(seed, this), (cur, child) => child.fold(cur, aggregate));
    }

    public int value() {
        if(childNode.Length == 0){
            return metadataEntries.Sum();
        }

        var res = 0;
        foreach(var i in metadataEntries){
            if(i >= 1 && i <= childNode.Length){
                res += childNode[i-1].value();
            }
        }
        return res;
    }
}