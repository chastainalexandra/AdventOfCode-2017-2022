using System.Linq;

namespace AdventOfCode.Y2017.Day19;

public class Helper {
     public (string msg, int steps) NetworkPacketPath(string fileData){
        var fd = fileData.Split('\n');
        var (columnCol, columnRow) = (fd[0].Length, fd.Length);
        var (intersectionCol, intersectionRow) = (fd[0].IndexOf('|'), 0);
        var (directionCol, directionRow) = (0, 1);

        var msg = "";
        var steps = 0;

        while (true) {
            intersectionRow += directionRow;
            intersectionCol += directionCol;
            steps++;

            if (intersectionCol < 0 || intersectionCol >= columnCol || intersectionRow < 0 || intersectionRow >= columnRow || fd[intersectionRow][intersectionCol] == ' ') {
                break;
            }

            switch (fd[intersectionRow][intersectionCol]) {
                case '+':
                    (directionCol, directionRow) = (
                            from packet in new[] { (drow: directionCol, directionCol: -directionRow), (drow: -directionCol, directionCol: directionRow)}
                            let icolT = intersectionCol + packet.directionCol
                            let irowT = intersectionRow + packet.drow
                            where icolT >= 0 && icolT < columnCol && irowT >= 0 && irowT < columnRow && fd[irowT][icolT] != ' '
                            select (packet.directionCol, packet.drow)
                        ).Single();
                    break;
                case char ch when (ch >= 'A' && ch <= 'Z'):
                    msg += ch;
                    break;
            }
        }
        return (msg, steps);
    }
}