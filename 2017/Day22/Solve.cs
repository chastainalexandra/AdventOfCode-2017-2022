using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2017.Day22;

public class Solve {
     public static int FindInfections(string fileData, int iterations, Func<VirusStates, int, int, (VirusStates State, int row, int col)> update) {
        var fd = fileData.Split('\n');
        var columnRow = fd.Length;
        var columnCol = fd[0].Length;
        var cells = new Dictionary<(int row, int column), VirusStates>();
        for (int r = 0; r < columnRow; r++) {
            for (int c = 0; c < columnCol; c++) {
                if (fd[r][c] == '#') {
                    cells.Add((r, c), VirusStates.Infected);
                }
            }
        }
        var (row, col) = (columnRow / 2, columnCol/ 2);
        var (rr, cc) = (-1, 0);
        var virusInfections = 0;
        for (int i = 0; i < iterations; i++) {
            var state = cells.TryGetValue((row, col), out var s) ? s : VirusStates.Clean;
            
            (state, rr, cc) = update(state, rr, cc);
            
            if (state == VirusStates.Infected) {
                virusInfections++;
            }
            if (state == VirusStates.Clean) {
                cells.Remove((row, col));
            } else {
                cells[(row, col)] = state;
            }
            (row, col) = (row + rr, col + cc);
        }
        return virusInfections;
    }
}