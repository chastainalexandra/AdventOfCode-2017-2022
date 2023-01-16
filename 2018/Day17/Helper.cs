using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace AdventOfCode.Y2018.Day17;

public class Helper {
    
     public string FillSpring(string fileInput) {
        var (width, height) = (2000, 2000);
        var coordinates = new char[width, height];

        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                coordinates[x, y] = '.';
            }
        }

        foreach (var fl in fileInput.Split("\n")) {
            var nums = Regex.Matches(fl, @"\d+").Select(g => int.Parse(g.Value)).ToArray();
            for (var i = nums[1]; i <= nums[2]; i++) {
                if (fl.StartsWith("x")) {
                    coordinates[nums[0], i] = '#';
                } else {
                    coordinates[i, nums[0]] = '#';
                }
            }
        }
        FillSpringRecursive(coordinates, 500, 0);

        var (minY, maxY) = (int.MaxValue, int.MinValue);
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                if (coordinates[x, y] == '#') {
                    minY = Math.Min(minY, y);
                    maxY = Math.Max(maxY, y);
                }
            }
        }
        var sb = new StringBuilder();
        for (var y = minY; y <= maxY; y++) {
            for (var x = 0; x < width; x++) {
                sb.Append(coordinates[x, y]);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public void FillSpringRecursive(char[,] coordinates, int x, int y) {
        var width = coordinates.GetLength(0);
        var height = coordinates.GetLength(1);
        if (coordinates[x, y] != '.') {
            return;
        }
        coordinates[x, y] = '|';
        if (y == height - 1) {
            return ;
        }
        FillSpringRecursive(coordinates, x, y + 1);

        if (coordinates[x, y + 1] == '#' || coordinates[x, y + 1] == '~') {
            if (x > 0) {
                FillSpringRecursive(coordinates, x - 1, y);
            }
            if (x < width - 1) {
                FillSpringRecursive(coordinates, x + 1, y);
            }
        }

        if (WaterAtRest(coordinates, x, y)) {
            foreach (var dx in new[] { -1, 1 }) {
                for (var xT = x; xT >= 0 && xT < width && coordinates [xT, y] == '|'; xT += dx) {
                    coordinates[xT, y] = '~'; //  counting both water at rest (~)   
                }
            }
        }
    }

    public bool WaterAtRest(char[,] mtx, int x, int y) {
        var width = mtx.GetLength(0);
        foreach (var dx in new[] { -1, 1 }) {
            for (var xT = x; xT >= 0 && xT < width && mtx[xT, y] != '#'; xT += dx) {
                if (mtx[xT, y] == '.' || mtx[xT, y + 1] == '|') { // ther sand tiles the water can hypothetically reach (|)
                    return false;
                }
            }
        }
        return true;
    }
}