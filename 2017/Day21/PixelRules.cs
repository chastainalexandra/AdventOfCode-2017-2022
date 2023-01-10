using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day21;

class PixelRules {
    private Dictionary<int, Pixels> rules2;
    private Dictionary<int, Pixels> rules3;

    public PixelRules(string fileData) {
        rules2 = new Dictionary<int, Pixels>();
        rules3 = new Dictionary<int, Pixels>();

        foreach (var fd in fileData.Split('\n')) {
            var parts = Regex.Split(fd, " => ");
            var left = parts[0];
            var right = parts[1];
            var rules =
                left.Length == 5 ? rules2 :
                left.Length == 11 ? rules3 :
                throw new Exception();
            foreach (var mtx in Variations(Pixels.FromString(left))) {
                rules[mtx.CodeNumber] = Pixels.FromString(right);
            }
        }
    }

    public Pixels Apply(Pixels mtx) {
        return Pixels.Join((
            from child in mtx.Split()
            select
                child.PatternSize == 2 ? rules2[child.CodeNumber] :
                child.PatternSize == 3 ? rules3[child.CodeNumber] :
                null
        ).ToArray());
    }

    IEnumerable<Pixels> Variations(Pixels mtx) {
        for (int j = 0; j < 2; j++) {
            for (int i = 0; i < 4; i++) {
                yield return mtx;
                mtx = mtx.Rotate();
            }
            mtx = mtx.Flip();
        }
    }
}