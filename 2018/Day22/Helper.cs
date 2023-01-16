using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day22;

public class Helper {
   public (int targCoordinateX, int targCoordinateY, Func<int, int, RegionType> regionType) RiskAndCoordinates(string fileInput) {
        var fl = fileInput.Split("\n");
        var depth = Regex.Matches(fl[0], @"\d+").Select(x => int.Parse(x.Value)).Single();
        var coordinatesForTarget = Regex.Matches(fl[1], @"\d+").Select(x => int.Parse(x.Value)).ToArray();
        var (targCoordinateX, targCoordinateY) = (coordinatesForTarget[0], coordinatesForTarget[1]);

        var m = 20183; //  region's erosion level is its geologic index plus the cave system's depth, all modulo 20183.

        var erosionLevel = new Dictionary<(int, int), int>();
        int geologicIndex(int x, int y) {
            var key = (x, y);
            if (!erosionLevel.ContainsKey(key)) {
                if (x == targCoordinateX && y == targCoordinateY) {
                    erosionLevel[key] = depth;
                } else if (x == 0 && y == 0) { // he region at 0,0 (the mouth of the cave) has a geologic index of 0
                    erosionLevel[key] = depth;
                } else if (x == 0) {
                    erosionLevel[key] = ((y * 48271) + depth) % m; // If the region's X coordinate is 0, the geologic index is its Y coordinate times 48271.
                } else if (y == 0) {
                    erosionLevel[key] = ((x * 16807) + depth) % m; // If the region's Y coordinate is 0, the geologic index is its X coordinate times 16807.
                } else {
                    erosionLevel[key] = ((geologicIndex(x, y - 1) * geologicIndex(x - 1, y)) + depth) % m;
                }
            }
            return erosionLevel[key];
        }


        //  A region's erosion level is its geologic index plus the cave system's depth, all modulo 20183. Then:
        // If the erosion level modulo 3 is 0, the region's type is rocky.
        // If the erosion level modulo 3 is 1, the region's type is wet.
        // If the erosion level modulo 3 is 2, the region's type is narrow.
        RegionType regionType(int x, int y) {
        // At 0,0, the geologic index is 0. The erosion level is (0 + 510) % 20183 = 510. The type is 510 % 3 = 0, rocky.
        // At 1,0, because the Y coordinate is 0, the geologic index is 1 * 16807 = 16807. The erosion level is (16807 + 510) % 20183 = 17317. The type is 17317 % 3 = 1, wet.
        // At 0,1, because the X coordinate is 0, the geologic index is 1 * 48271 = 48271. The erosion level is (48271 + 510) % 20183 = 8415. The type is 8415 % 3 = 0, rocky.
        // At 1,1, neither coordinate is 0 and it is not the coordinate of the target, so the geologic index is the erosion level of 0,1 (8415) times the erosion level of 1,0 (17317), 8415 * 17317 = 145722555. The erosion level is (145722555 + 510) % 20183 = 1805. The type is 1805 % 3 = 2, narrow.
        // At 10,10, because they are the target's coordinates, the geologic index is 0. The erosion level is (0 + 510) % 20183 = 510. The type is 510 % 3 = 0, rocky.
            return (RegionType)(geologicIndex(x, y) % 3);
        }

        return (targCoordinateX, targCoordinateY, regionType);
    }
}