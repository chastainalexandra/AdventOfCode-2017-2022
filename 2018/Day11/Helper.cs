
namespace AdventOfCode.Y2018.Day11;

/* Find the fuel cell's rack ID, which is its X coordinate plus 10.
* Begin with a power level of the rack ID times the Y coordinate.
* Increase the power level by the value of the grid serial number (your puzzle input).
* Set the power level to itself multiplied by the rack ID.
* Keep only the hundreds digit of the power level (so 12345 becomes 3; numbers with no hundreds digit become 0).
* Subtract 5 from the power level.
*/

public class Helper {

    // For example, to find the power level of the fuel cell at 3,5 in a grid with serial number 8:
    // The rack ID is 3 + 10 = 13.
    // The power level starts at 13 * 5 = 65.
    // Adding the serial number produces 65 + 8 = 73.
    // Multiplying by the rack ID produces 73 * 13 = 949.
    // The hundreds digit of 949 is 9.
    // Subtracting 5 produces 9 - 5 = 4.

     public (int x, int y, int d) GetCoordinate(int gridSerialNumber, int D) {
        var fuelCell = new int[300, 300]; // Each fuel cell has a coordinate ranging from 1 to 300 in both the X (horizontal) and Y (vertical) direction. In X,Y notation, the top-left cell is 1,1, and the top-right cell is 300,1.
        for (var row = 0; row < 300; row++) {
            for (var column = 0; column < 300; column++) {
                var xx = column + 1;
                var yy = row + 1;
                var rackId = xx + 10;
                var powerLevel = rackId * yy;
                powerLevel += gridSerialNumber;
                powerLevel *= rackId;
                powerLevel = (powerLevel % 1000) / 100;
                powerLevel -= 5;

                fuelCell[row, column] = powerLevel;
            }
        }

        var totalPower = int.MinValue;
        var y = int.MinValue;
        var x = int.MinValue;
        var dd = int.MinValue;

        var cell = new int[300, 300];
        for (var d = 1; d <= D; d++) {
            for (var row = 0; row < 300 - d; row++) {
                for (var column = 0; column < 300; column++) {
                    cell[row, column] += fuelCell[row + d - 1, column];
                }
            }

            for (var row = 0; row < 300 - d; row++) {
                for (var column = 0; column < 300 - d; column++) {
                    var tp = 0;

                    for (var i = 0; i < d; i++) {
                        tp += cell[row, column + i];
                    }

                    if (tp > totalPower) {
                        tp = totalPower;
                        y = row + 1;
                        x = column + 1;
                        dd = d;
                    }
                }
            }
        }
        return (x, y, dd);
    }
    
}