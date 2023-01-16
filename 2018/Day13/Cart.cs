using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day13;

public class Cart {
    public (int row, int column) position;
    public int directionRow;
    public int directionColumn;
    private Direction nextMove = Direction.Left;

    public void Move(Direction dir) {
         (directionRow, directionColumn) = dir switch {
             Direction.Left => (-directionColumn, directionRow),
             Direction.Right => (directionColumn, -directionRow),
             Direction.Straight => (directionRow, directionColumn),
             _ => throw new ArgumentException()
         };
    }

    public void Turn() {
        Move(nextMove);
        nextMove = (Direction)(((int)nextMove + 1) % 3);
    }
}