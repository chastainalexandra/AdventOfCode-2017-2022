using System;
using System.Linq;


namespace AdventOfCode.Y2018.Day23;

public class NanoBots {
    public readonly (int x, int y, int z) position;
    public readonly int r;
    public readonly Formation formation;
    public NanoBots((int x, int y, int z) position, int r) {
        this.position = position;
        this.r = r;
        formation = new Formation((position.x - r, position.y - r, position.z - r), (2 * r + 1, 2 * r + 1, 2 * r + 1));
    }

    public bool SignalRadius(Formation formation) {
        var dx = Math.Max(0, Math.Max(formation.min.x - position.x, position.x - formation.max.x));
        var dy = Math.Max(0, Math.Max(formation.min.y - position.y, position.y - formation.max.y));
        var dz = Math.Max(0, Math.Max(formation.min.z - position.z, position.z - formation.max.z));

        return Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz) <= r;
    }

    public bool Contains(Formation formation) {
        return formation
            .Coordinates()
            .All(pt => Math.Abs(pt.x - position.x) + Math.Abs(pt.y - position.y) + Math.Abs(pt.z - position.z) <= r);
    }
}