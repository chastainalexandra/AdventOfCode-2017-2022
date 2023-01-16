using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day05;

public class Helper 
{
    public char ToLower(char ch) => ch <= 'Z' ? (char)(ch - 'A' + 'a') : ch;


     // In aA, a and A react, leaving nothing behind.
    // In abBA, bB destroys itself, leaving aA. As above, this then destroys itself, leaving nothing.
    // In abAB, no two adjacent units are of the same type, and so nothing happens.
    // In aabAAB, even though aa and AA are of the same type, their polarities match, and so nothing happens.
    public int Units(string fileInput, char? skip = null) {
        var polymers = new Stack<char>("⊥");
        
        foreach (var fl in fileInput) {
            var top = polymers.Peek();
            if (ToLower(fl) == skip) {
                continue;
            } else if (top != fl && ToLower(fl) == ToLower(top)) {
                polymers.Pop();
            } else {
                polymers.Push(fl);
            }
        }
        return polymers.Count() - 1;
    }
}