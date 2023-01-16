using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day10;

public record MessageString(string st)
{
    public override string ToString() {
        var fl = st.Split("\n")
            .SkipWhile(x => string.IsNullOrWhiteSpace(x))
            .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        while (fl.All(line => line.StartsWith(" "))) {
            fl = GetTextRectangle(fl, 1, 0, fl[0].Length - 1, fl.Length).Split("\n");
        }

        while (fl.All(line => line.EndsWith(" "))) {
            fl = GetTextRectangle(fl, 0, 0, fl[0].Length - 1, fl.Length).Split("\n");
        }

        var width = fl[0].Length;
        var height = fl.Length;

        var smallAlphabet = StripMargin(@"
        | A    B    C    E    F    G    H    I    J    K    L    O    P    R    S    U    Y    Z    
        |  ##  ###   ##  #### ####  ##  #  #  ###   ## #  # #     ##  ###  ###   ### #  # #   ##### 
        | #  # #  # #  # #    #    #  # #  #   #     # # #  #    #  # #  # #  # #    #  # #   #   # 
        | #  # ###  #    ###  ###  #    ####   #     # ##   #    #  # #  # #  # #    #  #  # #   #  
        | #### #  # #    #    #    # ## #  #   #     # # #  #    #  # ###  ###   ##  #  #   #   #   
        | #  # #  # #  # #    #    #  # #  #   #  #  # # #  #    #  # #    # #     # #  #   #  #    
        | #  # ###   ##  #### #     ### #  #  ###  ##  #  # ####  ##  #    #  # ###   ##    #  #### 
        ");

        var largeAlphabet = StripMargin(@"
        | A       B       C       E       F       G       H       J       K       L       N       P       R       X       Z
        |   ##    #####    ####   ######  ######   ####   #    #     ###  #    #  #       #    #  #####   #####   #    #  ######  
        |  #  #   #    #  #    #  #       #       #    #  #    #      #   #   #   #       ##   #  #    #  #    #  #    #       #  
        | #    #  #    #  #       #       #       #       #    #      #   #  #    #       ##   #  #    #  #    #   #  #        #  
        | #    #  #    #  #       #       #       #       #    #      #   # #     #       # #  #  #    #  #    #   #  #       #   
        | #    #  #####   #       #####   #####   #       ######      #   ##      #       # #  #  #####   #####     ##       #    
        | ######  #    #  #       #       #       #  ###  #    #      #   ##      #       #  # #  #       #  #      ##      #     
        | #    #  #    #  #       #       #       #    #  #    #      #   # #     #       #  # #  #       #   #    #  #    #      
        | #    #  #    #  #       #       #       #    #  #    #  #   #   #  #    #       #   ##  #       #   #    #  #   #       
        | #    #  #    #  #    #  #       #       #   ##  #    #  #   #   #   #   #       #   ##  #       #    #  #    #  #       
        | #    #  #####    ####   ######  #        ### #  #    #   ###    #    #  ######  #    #  #       #    #  #    #  ######  
        ");

        var charMap =
            fl.Length == smallAlphabet.Length - 1 ? smallAlphabet :
            fl.Length == largeAlphabet.Length - 1 ? largeAlphabet :
            throw new Exception("Could not find alphabet");

        var charWidth = charMap == smallAlphabet ? 5 : 8;
        var charHeight = charMap == smallAlphabet ? 6 : 10;
        var res = "";
        for (var i = 0; i < width; i += charWidth) {
            res += FindText(fl, i, charWidth, charHeight, charMap);
        }
        return res;
    }

    string[] StripMargin(string st) => (
        from line in Regex.Split(st, "\r?\n")
        where Regex.IsMatch(line, @"^\s*\| ")
        select Regex.Replace(line, @"^\s* \| ", "")
    ).ToArray();

    public string FindText(string[] text, int columnLetter, int charWidth, int charHeight, string[] charMap) {
        var textRect = GetTextRectangle(text, columnLetter, 0, charWidth, charHeight);

        for (var column = 0; column < charMap[0].Length; column += charWidth) {
            var ch = charMap[0][column].ToString();
            var charPattern = GetTextRectangle(charMap, column, 1, charWidth, charHeight);
            var found = Enumerable.Range(0, charPattern.Length).All(i => {
                var textWhiteSpace = " .".Contains(textRect[i]);
                var charWhiteSpace = " .".Contains(charPattern[i]);
                return textWhiteSpace == charWhiteSpace;
            });

            if (found) {
                return ch;
            }
        }

        throw new Exception($"Unrecognized letter: \n{textRect}\n");
    }

    string GetTextRectangle(string[] st, int column, int row, int column1, int columnRow) {
        var res = "";
        for (var r = row; r < row + columnRow; r++) {
            for (var c = column; c < column + column1; c++) {
                var ch = r < st.Length && c < st[r].Length ? st[r][c] : ' ';
                res += ch;
            }
            if (r + 1 != row + columnRow) {
                res += "\n";
            }
        }
        return res;
    }
}