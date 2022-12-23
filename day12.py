# You try contacting the Elves using your handheld device, but the river you're following must be too low to get a decent signal.
# You ask the device for a heightmap of the surrounding area (your puzzle input).
# The heightmap shows the local area from above broken into a grid; the elevation of each square of the grid is given by a single lowercase letter, 
# where a is the lowest elevation, b is the next-lowest, and so on up to the highest elevation, z.
# Also included on the heightmap are marks for your current position (S) and the location that should get the best signal (E). 
# Your current position (S) has elevation a, and the location that should get the best signal (E) has elevation z.
# You'd like to reach E, but to save energy, you should do it in as few steps as possible. 
# During each step, you can move exactly one square up, down, left, or right. 
# To avoid needing to get out your climbing gear, the elevation of the destination square can be at most one higher 
# than the elevation of your current square; that is, if your current elevation is m, you could step to elevation n, but not to elevation o. 
# (This also means that the elevation of the destination square can be much lower than the elevation of your current square.)

from string import ascii_lowercase
from heapq import heappop, heappush
import string
import collections

def part01(lines): 
    grid = [list(line) for line in lines]
    n = len(grid)
    m = len(grid[0])

    for i in range(n):
        for j in range(m):
            char = grid[i][j]
            if char == "S": # heightmap are marks for your current position (S)
                start = i, j
            if char == "E": # location that should get the best signal (E)
                end = i, j

    def height(s):
        if s in ascii_lowercase:
            return ascii_lowercase.index(s)
        if s == "S":
            return 0
        if s == "E":
            return 25

    def neighbors(i, j):
        for di, dj in [[1, 0], [-1, 0], [0, 1], [0, -1]]:
            ii = i + di
            jj = j + dj

            if not (0 <= ii < n and 0 <= jj < m):
                continue

            if height(grid[ii][jj]) <= height(grid[i][j]) + 1:
                yield ii, jj

    visited = [[False] * m for _ in range(n)]
    heap = [(0, start[0], start[1])]

    while True:
        steps, i, j = heappop(heap)

        if visited[i][j]:
            continue
        visited[i][j] = True

        if (i, j) == end:
            print(steps)
            break

        for ii, jj in neighbors(i, j):
            heappush(heap, (steps + 1, ii, jj))

# As you walk up the hill, you suspect that the Elves will want to turn this into a hiking trail. 
# The beginning isn't very scenic, though; perhaps you can find a better starting point.
# To maximize exercise while hiking, the trail should start as low as possible: 
# elevation a. The goal is still the square marked E. 
# However, the trail should still be direct, taking the fewest steps to reach its goal. 
# So, you'll need to find the shortest path from any square at elevation a to the square marked E.


def part02():
    possible_origins = []
    dest = None
    with open('day12.txt') as f:
        heightmap = []
        for i, line in enumerate(f):
            line = line.strip()
            for j, char in enumerate(line):
                if char == 'a':
                    possible_origins.append((j, i))
            if line.find('E') != -1:
                dest = (line.index('E'), i)
                line = line.replace('E', 'z')
            heightmap.append([string.ascii_lowercase.index(x) for x in line])


    def next_steps(heightmap, pos):
        options = []
        for (x, y) in [(0, -1), (0, 1), (1, 0), (-1, 0)]:
            new_pos = (pos[0] + x, pos[1] + y)
            if new_pos[0] < 0 or new_pos[1] < 0 or new_pos[0] >= len(heightmap[0]) or new_pos[1] >= len(heightmap):
                continue
            if heightmap[pos[1] + y][pos[0] + x] <= heightmap[pos[1]][pos[0]] + 1:
                options.append(new_pos)
        return options


    def find_route(origin):
        queue = collections.deque()
        queue.extend([[origin, x] for x in next_steps(heightmap, origin)])
        visited = set()
        while len(queue):
            path = queue.popleft()
            steps = next_steps(heightmap, path[-1])
            for step in steps:
                if step == dest:
                    return len(path)
                if step not in visited:
                    visited.add(step)
                    queue.append(path + [step])
        return None


    shortest_route = None
    for o in possible_origins:
        new_len = find_route(o)
        if new_len is not None and (shortest_route is None or new_len < shortest_route):
            print(f"new best: {new_len}")
            shortest_route = new_len

        print(shortest_route)

if __name__ == "__main__":
   with open("day12.txt") as fin:
    lines = fin.read().strip().split("\n")
   part01(lines)
   part02()

# answers 
# Part 1 - 497
# Part 2 - 492