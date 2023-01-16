using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Y2018.Day22;

public class TheQueue<T> {
    SortedDictionary<int, Queue<T>> d = new SortedDictionary<int, Queue<T>>();
    public bool Any() {
        return d.Any();
    }

    public void Enqueue(int p, T t) {
        if (!d.ContainsKey(p)) {
            d[p] = new Queue<T>();
        }
        d[p].Enqueue(t);
    }

    public T Dequeue() {
        var p = d.Keys.First();
        var items = d[p];
        var t = items.Dequeue();
        if (!items.Any()) {
            d.Remove(p);
        }
        return t;
    }
}
