using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Y2018.Day23;

public class TheQueue<K, T> where K : IComparable {
    SortedDictionary<K, Queue<T>> d = new SortedDictionary<K, Queue<T>>();
    int c = 0;
    public bool Any() {
        return d.Any();
    }

    public void Enqueue(K p, T t) {
        if (!d.ContainsKey(p)) {
            d[p] = new Queue<T>();
        }
        d[p].Enqueue(t);
        c++;
    }

    public T Dequeue() {
        c--;
        var p = d.Keys.First();
        var items = d[p];
        var t = items.Dequeue();
        if (!items.Any()) {
            d.Remove(p);
        }
        return t;
    }

    public int Count() {
        return c;
    }
}
