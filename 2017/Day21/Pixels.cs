using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day21;

class Pixels {
    private bool[] flags;

    public int PatternSize {
        get;
        private set;
    }

    public Pixels(int PatternSize) {
        this.flags = new bool[PatternSize * PatternSize];
        this.PatternSize = PatternSize;
    }

    public override string ToString() {
        var sb = new StringBuilder();
        for (int row = 0; row < PatternSize; row++) {
            for (int col = 0; col < PatternSize; col++) {
                sb.Append(this[row, col] ? "#" : ".");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static Pixels FromString(string st) {
        st = st.Replace("/", "");
        var pixelSize = (int)Math.Sqrt(st.Length);
        var res = new Pixels(pixelSize);
        for (int i = 0; i < st.Length; i++) {
            res[i / pixelSize, i % pixelSize] = st[i] == '#';
        }
        return res;
    }


    private bool this[int row, int col] {
        get {
            return flags[(PatternSize * row) + col];
        }
        set {
            flags[(PatternSize * row) + col] = value;
        }
    }

    // Then, the program repeats the following process:
    // If the size is evenly divisible by 2, break the pixels up into 2x2 squares, 
    //  and convert each 2x2 square into a 3x3 square by following the corresponding enhancement rule.
    //  Otherwise, the size is evenly divisible by 3; break the pixels up into 3x3 squares, 
    // and convert each 3x3 square into a 4x4 square by following the corresponding enhancement rule.
    public int CodeNumber {
        get {
            if (PatternSize != 2 && PatternSize != 3) {
                throw new ArgumentException();
            }
            var x = 0;
            for (int row = 0; row < PatternSize; row++) {
                for (int col = 0; col < PatternSize; col++) {
                    if (this[row, col]) {
                        x |= (1 << (row * PatternSize + col));
                    }
                }
            }
            return x;
        }
    }

    public static Pixels Join(Pixels[] pix) {
        var pixelPerRow = (int)Math.Sqrt(pix.Length);
        var res = new Pixels(pixelPerRow * pix[0].PatternSize);
        for (int pi = 0; pi < pix.Length; pi++) {
            var pixel = pix[pi];
            for (int row = 0; row < pixel.PatternSize; row++) {
                for (int col = 0; col < pixel.PatternSize; col++) {
                    var rowRes = (pi / pixelPerRow) * pixel.PatternSize + row;
                    var colRes = (pi % pixelPerRow) * pixel.PatternSize + col;
                    res[rowRes, colRes] = pixel[row, col];
                }
            }
        }

        return res;
    }

    public int PixelCount() {
        var pixelCount = 0;
        for (int row = 0; row < PatternSize; row++) {
            for (int col = 0; col < PatternSize; col++) {
                if (this[row, col]) {
                    pixelCount++;
                }
            }
        }
        return pixelCount;
    }

    // The size of the grid (3) is not divisible by 2, but it is divisible by 3.
    //  It divides evenly into a single square; the square matches the second rule,
    public IEnumerable<Pixels> Split() {

        var girdSize =
            PatternSize % 2 == 0 ? 2 :
            PatternSize % 3 == 0 ? 3 :
            throw new Exception();

        for (int row = 0; row < PatternSize; row += girdSize) {
            for (int col = 0; col < PatternSize; col += girdSize) {
                var pixel = new Pixels(girdSize);
                for (int drow = 0; drow < girdSize; drow++) {
                    for (int dcol = 0; dcol < girdSize; dcol++) {
                        pixel[drow, dcol] = this[row + drow, col + dcol];
                    }
                }
                yield return pixel;
            }
        }
    }
    
    public Pixels Flip() {
        var res = new Pixels(this.PatternSize);
        for (int row = 0; row < PatternSize; row++) {
            for (int col = 0; col < PatternSize; col++) {
                res[row, PatternSize - col - 1] = this[row, col];
            }
        }
        return res;
    }

    public Pixels Rotate() {
        var res = new Pixels(this.PatternSize);
        for (int i = 0; i < PatternSize; i++) {
            for (int x = 0; x < PatternSize; x++) {
                res[i, x] = this[x, PatternSize - i - 1];
            }
        }
        return res;
    }

    
}
