using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day03;

public class Helper 
{
    public (int overlapArea, int intactId) FabricDecorating(string fileInput) {
        var reg = new Regex(@"(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<width>\d+)x(?<height>\d+)");
        var fabricPiece = new int[1000, 1000]; // The whole piece of fabric they're working on is a very large square - at least 1000 inches on each side.



        var claimOverlapArea = 0;

        var claims = new HashSet<int>();
        foreach (var fl in fileInput.Split("\n")) {
            var squares = reg.Match(fl);
            var right = int.Parse(squares.Groups["x"].Value);
            var left = int.Parse(squares.Groups["y"].Value);
            var rectangleWidth = int.Parse(squares.Groups["width"].Value);
            var rectagleHeight = int.Parse(squares.Groups["height"].Value);
            var claimId = int.Parse(squares.Groups["id"].Value);

            claims.Add(claimId);

            for (var i = 0; i < rectangleWidth; i++) {
                for (var j = 0; j < rectagleHeight; j++) {
                    if (fabricPiece[right + i, left + j] == 0) {
                        fabricPiece[right + i, left + j] = claimId;
                    } else if (fabricPiece[right + i, left + j] == -1) {
                        claims.Remove(claimId);
                    } else {
                        claims.Remove(fabricPiece[right + i, left + j]);
                        claims.Remove(claimId);
                        claimOverlapArea++;

                        fabricPiece[right + i, left + j] = -1;
                    }
                }
            }
        }

        return (claimOverlapArea, claims.Single());
    }
}