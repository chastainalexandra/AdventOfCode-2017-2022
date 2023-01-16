using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day23;

public class Helper {
    public int Distance((int x, int y, int z) a, (int x, int y, int z) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z);

    IEnumerable<(int x, int y, int z)> Coordinates(NanoBots[] nanoBot) => (
        from nano in nanoBot
        from dx in new[] { -1, 0, 1 }
        from dy in new[] { -1, 0, 1 }
        from dz in new[] { -1, 0, 1 }
        where dx * dx + dy * dy + dz * dz == 1
        select (nano.position.x + dx * nano.r, nano.position.y + dy * nano.r, nano.position.z + dz * nano.r)
    ).ToArray();

    public NanoBots[] ExperimentalEmergencyTeleportation(string fileInput) => (
        from fi in fileInput.Split("\n")
        let lines = Regex.Matches(fi, @"-?\d+").Select(x => int.Parse(x.Value)).ToArray()
        select new NanoBots((lines[0], lines[1], lines[2]), lines[3])
    ).ToArray();

    public (int nanos, int pt) FindDistance(Formation formation, NanoBots[] nanoBots) {

        var queue = new TheQueue<(int, int), (Formation formation, NanoBots[] nanos)>();
        queue.Enqueue((0, 0), (formation, nanoBots));

        while (queue.Any()) {
            (formation, nanoBots) = queue.Dequeue();

            if (formation.Size() == 1) {
                return (nanoBots.Count(bot => bot.Contains(formation)), formation.Distance());
            } else {
                foreach (var form in formation.Divide()) {
                    var botsRange = nanoBots.Where(drone => drone.SignalRadius(form)).ToArray();
                    queue.Enqueue((-botsRange.Count(), form.Distance()), (form, botsRange));
                }
            }
        }
        throw new Exception();
    }
}