using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day13;

public class Helper {
    public string CartPostion(Cart cart) => $"{cart.position.column},{cart.position.row}";

   public (List<Cart> crashed, List<Cart> carts) Step(string[] mat, List<Cart> carts) {
        var crashed = new List<Cart>();

        foreach (var cart in carts.OrderBy((cartT) => cartT.position)) {
            cart.position = (row: cart.position.row + cart.directionRow, column: cart.position.column + cart.directionColumn);
            
            foreach (var cart2 in carts.ToArray()) {
                if (cart != cart2 && cart.position.row == cart2.position.row && cart.position.column == cart2.position.column) {
                    crashed.Add(cart);
                    crashed.Add(cart2);

                }
            }
            switch (mat[cart.position.row][cart.position.column]) {
                case '\\':
                    if (cart.directionColumn == 1 || cart.directionColumn == -1) {
                        cart.Move(Direction.Right);
                    } else if (cart.directionRow == -1 || cart.directionRow == 1) {
                        cart.Move(Direction.Left);
                    } else {
                        throw new Exception();
                    }
                    break;
                case '/':
                    if (cart.directionColumn == 1 || cart.directionColumn == -1) {
                        cart.Move(Direction.Left);
                    } else if (cart.directionRow == 1 || cart.directionRow == -1) {
                        cart.Move(Direction.Right);
                    }
                    break;
                case '+':
                    cart.Turn();
                    break;
            }
        }
        return (crashed, carts.Where(cart => !crashed.Contains(cart)).ToList());
    }

    public (string[] dir, List<Cart> carts) ReadInput(string fileInput){
        var dir = fileInput.Split("\n");
        var crow = dir.Length;
        var ccol = dir[0].Length;

        var carts = new List<Cart>();
        for (var row = 0; row < crow; row++) {
            for (var col = 0; col < ccol; col++) {
                var ch = dir[row][col];
                switch (ch) { // Several carts are also on the tracks. Carts always face either up (^), down (v), left (<), or right (>). (On your initial map, the track under each cart is a straight path matching the direction the cart is facing.)
                    case '^':
                        carts.Add(new Cart { position = (row: row, column: col), directionColumn = 0, directionRow = -1 });
                        break;
                    case 'v':
                        carts.Add(new Cart { position = (row: row, column: col), directionColumn = 0, directionRow = 1 });
                        break;
                    case '<':
                        carts.Add(new Cart { position = (row: row, column: col), directionColumn = -1, directionRow = 0 });
                        break;
                    case '>':
                        carts.Add(new Cart { position = (row: row, column: col), directionColumn = 1, directionRow = 0 });
                        break;
                }
            }
        }
        return (dir, carts);
    }
}