using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day15;

class Game {
    public Square[,] sq;
    public List<Elf> elves;
    public int rounds;

    private bool ValidPos((int row, int column) position) =>
        position.row >= 0 && position.row < this.sq.GetLength(0) && position.column >= 0 && position.column < this.sq.GetLength(1);

    public Square GetSquare((int row, int column) position) =>
        ValidPos(position) ? sq[position.row, position.column] : Wall.Square;

    public void Step() {
        var finishedBeforeEndOfRound = false;
        foreach (var elf in elves.OrderBy(elf => elf.position).ToArray()) {
            if (elf.hitPower > 0) {
                finishedBeforeEndOfRound |= Finished();
                elf.Step();
            }
        }

        if (!finishedBeforeEndOfRound) {
            rounds++;
        }
    }

    public bool Finished() =>
        elves.Where(e => e.elf).All(e => e.hitPower == 0) ||
        elves.Where(e => !e.elf).All(e => e.hitPower == 0);

}
