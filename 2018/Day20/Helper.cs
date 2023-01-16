using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day20;

public class Helper {
    public (int max, int distantRooms) GetDoors(string fileInput) {
        var roomMap = Rooms(fileInput)
            .ToList()
            .GroupBy(x => x.positionFrom)
            .ToDictionary(x=>x.Key, x=> x.Select(y => y.positionTo).ToList());
                   
        var queue = new Queue<((int x, int y) pos, int d)>();
        queue.Enqueue(((0, 0), 0));

        var visited = new HashSet<(int x, int y)>();
        var (max, distantRooms) = (int.MinValue, 0);

        while (queue.Any()) {
            var (pos, d) = queue.Dequeue();
            if (visited.Contains(pos)) {
                continue;
            }

            max = Math.Max(max, d);
            if (d >= 1000) {
                distantRooms++;
            }

            visited.Add(pos);
            foreach (var nextPos in roomMap[pos]) {
                queue.Enqueue((nextPos, d + 1));
            }
        }

        return (max, distantRooms);
    }

    public IEnumerable<((int x, int y) positionFrom, (int x, int y) positionTo)> Rooms(string fileInput) {
        var s = new Stack<(int x, int y)>();
        (int x, int y) position = (0, 0);
        foreach (var fi in fileInput) {
            var prev = position;
            switch (fi) {
                case 'N': // (north)
                    position = (position.x, position.y - 1);
                    break;
                case 'S': // (south),
                     position = (position.x, position.y + 1); 
                     break;
                case 'E':  // (east),
                    position = (position.x + 1, position.y); 
                    break;
                case 'W':  // (west)
                    position = (position.x - 1, position.y); 
                    break;
                case '(': // A branch is given by a list of options separated by pipes (|) and wrapped in parentheses.
                    s.Push(position); 
                    break;
                case '|':
                     position = s.Peek();
                     break;
                case ')':
                     position = s.Pop();
                    break;
            }

            if ("NSEW".IndexOf(fi) >= 0) {
                yield return (prev, position);
                yield return (position, prev);
            }
        }
    }
}