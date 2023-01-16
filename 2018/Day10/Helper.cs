using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day10;

public class Helper {
    public (string st, int seconds) GetMessage(string fileInput) {
        var rx = new Regex(@"position=\<\s*(?<x>-?\d+),\s*(?<y>-?\d+)\> velocity=\<\s*(?<vx>-?\d+),\s*(?<vy>-?\d+)\>");
        var points = (
            from fl in fileInput.Split("\n")
            let l = rx.Match(fl)
            select new Point {
                positionX = int.Parse(l.Groups["x"].Value),
                positionY = int.Parse(l.Groups["y"].Value),
                velocityX = int.Parse(l.Groups["vx"].Value),
                velocityY = int.Parse(l.Groups["vy"].Value)
            }
        ).ToArray();

        var seconds = 0;
        Func<bool, (int left, int top, long width, long height)> step = (bool forward) => {
            foreach (var point in points) {
                if (forward) {
                    point.positionX += point.velocityX;
                    point.positionY += point.velocityY;
                } else {
                    point.positionX -= point.velocityX;
                    point.positionY -= point.velocityY;
                }
            }
            seconds += forward ? 1 : -1;

            var minX = points.Min(pt => pt.positionX);
            var maxX = points.Max(pt => pt.positionX);
            var minY = points.Min(pt => pt.positionY);
            var maxY = points.Max(pt => pt.positionY);
            return (minX, minY, maxX - minX + 1, maxY - minY + 1);
        };

        var area = long.MaxValue;
        while (true) {

            var rect = step(true);
            var areaNew = (rect.width) * (rect.height);

            if (areaNew > area) {
                rect = step(false);
                var st = "";
                for(var row=0;row<rect.height;row++){

                    for(var column=0;column<rect.width;column++){
                        st += points.Any(p => p.positionX - rect.left == column && p.positionY-rect.top == row) ? '#': ' ';
                    }
                    st+= "\n";
                }
                return (st, seconds);
            }
            area = areaNew;
        }
    }
}