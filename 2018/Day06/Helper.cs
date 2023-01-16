using System;
using System.Linq;

namespace AdventOfCode.Y2018.Day06;

public class Helper {

    public (int x, int y)[] GetCoordinates(string fileInput) => (
            from fl in fileInput.Split("\n")
            let coordinates = fl.Split(", ").Select(int.Parse).ToArray()
            select (coordinates[0], coordinates[1])
        ).ToArray();
    
    // Using only the Manhattan distance, determine the area around each 
    // coordinate by counting the number of integer X,Y locations that are closest to that coordinate 
    // (and aren't tied in distance to any other coordinate).
    public int ManhattanDistance((int x, int y) c1, (int x, int y) c2) {
        return Math.Abs(c1.x - c2.x) + Math.Abs(c1.y - c2.y);
    }
}