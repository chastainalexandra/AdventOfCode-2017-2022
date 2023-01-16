using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Y2018.Day23;

public class Formation {
    public readonly (int x, int y, int z) min;
    public readonly (int x, int y, int z) max;
    private readonly (int sx, int sy, int sz) size;
    public Formation((int x, int y, int z) min, (int sx, int sy, int sz) size) {
        this.min = min;
        this.max = (min.x + size.sx - 1, min.y + size.sy - 1, min.z + size.sz - 1);
        this.size = size;
    }

    public IEnumerable<(int x, int y, int z)> Coordinates() {
        yield return (min.x, min.y, min.z);
        yield return (max.x, min.y, min.z);
        yield return (min.x, max.y, min.z);
        yield return (max.x, max.y, min.z);

        yield return (min.x, min.y, max.z);
        yield return (max.x, min.y, max.z);
        yield return (min.x, max.y, max.z);
        yield return (max.x, max.y, max.z);
    }
    
    public IEnumerable<Formation> Divide() {
        var sx = size.sx / 2;
        var tx = size.sx - sx; 
        var sy = size.sy / 2;
        var ty = size.sy - sy;
        var sz = size.sz / 2;
        var tz = size.sz - sz;

        return new[]{
            new Formation((min.x,      min.y,       min.z     ), (sx, sy, sz)),
            new Formation((min.x + sx, min.y,       min.z     ), (tx, sy, sz)),
            new Formation((min.x,      min.y + sy,  min.z     ), (sx, ty, sz)),
            new Formation((min.x + sx, min.y + sy,  min.z     ), (tx, ty, sz)),

            new Formation((min.x,      min.y,       min.z + sz), (sx, sy, tz)),
            new Formation((min.x + sx, min.y,       min.z + sz), (tx, sy, tz)),
            new Formation((min.x,      min.y + sy,  min.z + sz), (sx, ty, tz)),
            new Formation((min.x + sx, min.y + sy,  min.z + sz), (tx, ty, tz)),

        }.Where(formation => formation.size.sx > 0 && formation.size.sy > 0 && formation.size.sz > 0);
    }

    public int Distance() {
        return Coordinates().Select(pt => Math.Abs(pt.x) + Math.Abs(pt.y) + Math.Abs(pt.z)).Min();
    }

    public BigInteger Size() {
        return (BigInteger)size.sx * (BigInteger)size.sy * (BigInteger)size.sz;
    }
}