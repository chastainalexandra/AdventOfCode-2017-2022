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
import sys

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

def step(currentPos, nextStep, steps):
    global heightMapSize, heightMap, numSteps
    # Invalid step
    if (
        currentPos[0] + nextStep[0] < 0
        or currentPos[1] + nextStep[1] < 0
        or currentPos[0] + nextStep[0] >= heightMapSize[0]
        or currentPos[1] + nextStep[1] >= heightMapSize[1]
        or (
            ord(heightMap[currentPos[0] + nextStep[0]][currentPos[1] + nextStep[1]])
            - ord(heightMap[currentPos[0]][currentPos[1]])
            > 1
            and heightMap[currentPos[0]][currentPos[1]] != "S"
        )
        or numSteps[currentPos[0] + nextStep[0]][currentPos[1] + nextStep[1]]
        <= steps + 1
        or (
            heightMap[currentPos[0] + nextStep[0]][currentPos[1] + nextStep[1]] == "E"
            and ord("z") - ord(heightMap[currentPos[0]][currentPos[1]]) > 1
        )
    ):
        return
    newPos = [currentPos[0] + nextStep[0], currentPos[1] + nextStep[1]]
    steps += 1
    numSteps[newPos[0]][newPos[1]] = steps
    step(newPos, (0, 1), steps)
    step(newPos, (1, 0), steps)
    step(newPos, (0, -1), steps)
    step(newPos, (-1, 0), steps)

def findChar(c):
    global heightMapSize, heightMap
    for i in range(heightMapSize[0]):
        for j in range(heightMapSize[1]):
            if heightMap[i][j] == c:
                return [i, j]

def part02():
    endPos = findChar("E")
    minSteps = sys.maxsize
    for i in range(heightMapSize[0]):
        for j in range(heightMapSize[1]):
            if heightMap[i][j] == "S" or heightMap[i][j] == "a":
                initialPos = [i, j]
                numSteps[initialPos[0]][initialPos[1]] = 0
                step(initialPos, (0, 1), 0)
                step(initialPos, (1, 0), 0)
                step(initialPos, (0, -1), 0)
                step(initialPos, (-1, 0), 0)
                minSteps = min(minSteps, numSteps[endPos[0]][endPos[1]])
    print(minSteps)

if __name__ == "__main__":
   with open("day12.txt") as fin:
    lines = fin.read().strip().split("\n")
   part01(lines)

   file = open("day12.txt")

   data = file.readlines()

   heightMapSize = (len(data), len(data[0]) - 1)
   heightMap = [["" for i in range(heightMapSize[1])] for j in range(heightMapSize[0])]
   part02()

# answers 
# Part 1 - 497
# Part 2 - 492