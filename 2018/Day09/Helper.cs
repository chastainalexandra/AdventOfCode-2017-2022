using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day09;

public class Helper {
    public long GetElfScore(string fileInput, int marble) {

        var match = Regex.Match(fileInput, @"(?<players>\d+) players; last marble is worth (?<points>\d+) points");
        var players = new long[int.Parse(match.Groups["players"].Value)];
        var targetPoints = int.Parse(match.Groups["points"].Value) * marble;

        var current = new Node { value = 0 };
        current.First = current;
        current.Last = current;

        var points = 1;
        var iplayer = 1;
        while (points <= targetPoints) {

            if (points % 23 == 0) { // However, if the marble that is about to be placed has a number which is a multiple of 23, something entirely different happens. 
                for (var i = 0; i < 7; i++) { // the marble 7 marbles counter-clockwise from the current marble is removed from the circle and also added to the current player's score.
                    current = current.First;
                }

                players[iplayer] += points + current.value;

                var first = current.First;
                var last = current.Last;
                last.First = first;
                first.First = last;
                current = last;

            } else {
                var first = current.Last;
                var last = current.Last.Last;
                current = new Node { value = points, First = first, Last = last };
                first.Last = current;
                last.First = current;
            }

            points++;
            iplayer = (iplayer + 1) % players.Length;
        }

        return players.Max();
    }
}