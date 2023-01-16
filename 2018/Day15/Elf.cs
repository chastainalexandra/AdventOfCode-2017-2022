using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day15;

class Elf : Square {
    public (int row, int column) position;
    public bool elf;
    public int attackPower = 3; // Goblin or Elf, has 3 attack power
    public int hitPower = 200; // 200 hit points.
    public Game game;

    public bool Step() {
        if (hitPower <= 0) {
            return false;
        } else if (Attack()) {
            return true;
        } else if (Move()) {
            Attack();
            return true;
        } else {
            return false;
        }
    }

    private bool Move() {
        var targets = FindTargets();
        if (!targets.Any()) {
            return false;
        }
        var opponent = targets.OrderBy(a => a.target).First();
        var nextPos = targets.Where(a => a.elf == opponent.elf).Select(a => a.firstStep).OrderBy(_ => _).First();
        (game.sq[nextPos.row, nextPos.column], game.sq[position.row, position.column]) =
            (game.sq[position.row, position.column], game.sq[nextPos.row, nextPos.column]);
        position = nextPos;
        return true;
    }

    private IEnumerable<(Elf elf, (int row, int column) firstStep, (int row, int column) target)> FindTargets() {

        var minDist = int.MaxValue;
        foreach (var (otherPlayer, firstStep, target, dist) in BlocksNextToOpponentsByDistance()) {
            if (dist > minDist) {
                break;
            } else {
                minDist = dist;
                yield return (otherPlayer, firstStep, target);
            }
        }
    }

    private IEnumerable<(Elf elf, (int row, int column) firstStep, (int row, int column) target, int dist)> BlocksNextToOpponentsByDistance() {
        var seen = new HashSet<(int row, int column)>();
        seen.Add(position);
        var q = new Queue<((int row, int column) pos, (int drow, int dcol) origDir, int dist)>();

        foreach (var (drow, dcol) in new[] { (-1, 0), (0, -1), (0, 1), (1, 0) }) {
            var posT = (position.row + drow, position.column + dcol);
            q.Enqueue((posT, posT, 1));
        }

        while (q.Any()) {
            var (pos, firstStep, dist) = q.Dequeue();

            if (game.GetSquare(pos) is Empty) {
                foreach (var (drow, dcol) in new[] { (-1, 0), (0, -1), (0, 1), (1, 0) }) {
                    var posT = (pos.row + drow, pos.column + dcol);
                    if (!seen.Contains(posT)) {
                        seen.Add(posT);
                        q.Enqueue((posT, firstStep, dist + 1));

                        var nextBlock = game.GetSquare(posT);
                        if (nextBlock is Elf) {
                            var elf = nextBlock as Elf;
                            if (elf.elf != this.elf) {
                                yield return (elf, firstStep, pos, dist);
                            }
                        }
                    }
                }
            }
        }
    }

    private bool Attack() {
        var opponents = new List<Elf>();

        foreach (var (drow, dcol) in new[] { (-1, 0), (0, -1), (0, 1), (1, 0) }) {
            var posT = (this.position.row + drow, this.position.column + dcol);
            var block = game.GetSquare(posT);
            switch (block) {
                case Elf otherPlayer when otherPlayer.elf != this.elf:
                    opponents.Add(otherPlayer);
                    break;
            }
        }

        if (!opponents.Any()) {
            return false;
        }
        var minHitPower = opponents.Select(a => a.hitPower).Min();
        var opponent = opponents.First(a => a.hitPower == minHitPower);
        opponent.hitPower -= this.attackPower;
        if (opponent.hitPower <= 0) {
            game.elves.Remove(opponent);
            game.sq[opponent.position.row, opponent.position.column] = Empty.square;
        }
        return true;
    }

}
