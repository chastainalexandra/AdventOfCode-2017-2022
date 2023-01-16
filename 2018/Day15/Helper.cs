using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day15;

public class Helper {
    public (bool didElfDie, int score) GetGameOutcome(string fileInput, int goblinAp, int elfAp, bool tsto) {
        var game = GetGameData(fileInput, goblinAp, elfAp);
        var elfCount = game.elves.Count(elves => elves.elf);
        
        return (game.elves.Count(p => p.elf) == elfCount, game.rounds * game.elves.Select(elf => elf.hitPower).Sum());
    }


    Game GetGameData(string fileInput, int goblinAp, int elfAp) {
        var elves = new List<Elf>();
        var fl = fileInput.Split("\n");
        var sq = new Square[fl.Length, fl[0].Length];

        var game = new Game { sq = sq, elves = elves };

        for (var row = 0; row < fl.Length; row++) {
            for (var column = 0; column < fl[0].Length; column++) {
                switch (fl[row][column]) {
                    case '#': // generating a map of the walls (#)
                        sq[row, column] = Wall.Square;
                        break;
                    case '.': // open cavern (.)
                        sq[row, column] = Empty.square;
                        break;
                    case var character when character == 'G' || character == 'E': //  Goblin (G) and Elf (E)
                        var el = new Elf {
                            elf = character == 'E',
                            attackPower = character == 'E' ? elfAp : goblinAp,
                            position = (row, column),
                            game = game
                        };
                        elves.Add(el);
                        sq[row, column] = el;
                        break;
                }
            }
        }
        return game;
    }
    
}