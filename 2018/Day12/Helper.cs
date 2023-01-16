using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day12;

public class Helper {
    public (Pots pots, Dictionary<string, string> rules) Parse(string fileInput) {
        var fl = fileInput.Split("\n");
        var p = new Pots { potsToTheLeft = 0, pot = fl[0].Substring("initial state: ".Length) };
        var rules = (from f in fl.Skip(2) let parts = f.Split(" => ") select new { key = parts[0], value = parts[1] }).ToDictionary(x => x.key, x => x.value);
        return (p, rules);
    }

    public long SubterraneanSustainability(string fileInput, long iterations) {
        var (p, rules) = Parse(fileInput);

        var dLeftPos = 0L;

        while (iterations > 0) {
            var prevState = p;
            p = Spread(p, rules);
            iterations--;
            dLeftPos = p.potsToTheLeft - prevState.potsToTheLeft;
            if (p.pot == prevState.pot) {
                p = new Pots { potsToTheLeft = p.potsToTheLeft + iterations * dLeftPos, pot = p.pot };
                break;
            }
        }

        return Enumerable.Range(0, p.pot.Length).Select(i => p.pot[i] == '#' ? i + p.potsToTheLeft : 0).Sum();
    }

     Pots Spread(Pots p, Dictionary<string, string> rules) {
        var pots = "....." + p.pot + ".....";
        var newP = "";
        for (var i = 2; i < pots.Length - 2; i++) {
            var x = pots.Substring(i - 2, 5);
            newP += rules.TryGetValue(x, out var ch) ? ch : ".";
        }

        var firstFlower = newP.IndexOf("#");
        var newLeft = firstFlower + p.potsToTheLeft - 3;

        newP = newP.Substring(firstFlower);
        newP = newP.Substring(0, newP.LastIndexOf("#") + 1);
        var res = new Pots { potsToTheLeft = newLeft, pot = newP };

        return res;
    }

}
    