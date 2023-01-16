using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace AdventOfCode.Y2018.Day18;

public class Helper {
    public int LumberCollection(string fileInput, int timeLimit) {
        var acres = new Dictionary<string, int>();
        var fl = fileInput.Split("\n");
        
        for (var t = 0; t < timeLimit; t++) {
            var hash = string.Join("", fl);
            if (acres.ContainsKey(hash)) {
                var time = t - acres[hash];
                var remainingResources = timeLimit - t - 1;
                var visitsLeft = remainingResources / time;
                t += visitsLeft * time;
            } else {
                acres[hash] = t;
            }
            fl = Resources(fl);
        }
        var res = string.Join("", fl);
        return Regex.Matches(res, @"\#").Count * Regex.Matches(res, @"\|").Count;
    }

    public string[] Resources(string[] resc) {
        var resources = new List<string>();
        var columnRow = resc.Length;
        var columnColumn = resc[0].Length;
       
        for (var row = 0; row < columnRow; row++) {
            var fl = "";
            for (var column = 0; column < columnColumn; column++) {
                var (tree, lumberyard, empty) = (0, 0, 0);
                foreach (var drow in new[] { -1, 0, 1 }) {
                    foreach (var dcol in new[] { -1, 0, 1 }) {
                        if (drow != 0 || dcol != 0) {
                            var (columnT, rowT) = (column + dcol, row + drow);
                            if (columnT >= 0 && columnT < columnColumn && rowT >= 0 && rowT < columnRow) {
                                switch (resc[rowT][columnT]) {
                                    case '#': 
                                        lumberyard++; // lumberyard (#)
                                    break; 
                                    case '|': 
                                        tree++; //  trees (|)
                                    break;
                                    case '.': // open ground (.)
                                        empty++; 
                                    break;
                                }
                            }
                        }
                    }
                }

                fl += resc[row][column] switch {
                    '#' when lumberyard >= 1 && tree >= 1 => '#',
                    '|' when lumberyard >= 3 => '#',
                    '.' when tree >= 3 => '|',
                    '#' => '.',
                    var c => c
                };
            }
            resources.Add(fl);
        }
        return resources.ToArray();
    }
}