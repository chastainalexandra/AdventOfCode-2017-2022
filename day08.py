import numpy as np

with open("day08.txt", "r") as fin:
    dataFile = fin.read().strip().split()

grid = [list(map(int, list(line))) for line in dataFile]

n = len(grid)
m = len(grid[0])

grid = np.array(grid)

part1 = 0
part2 = 0
for i in range(n):
    for j in range(m):
        h = grid[i, j]

        if j == 0 or np.amax(grid[i, :j]) < h:
            part1 += 1
        elif j == m - 1 or np.amax(grid[i, (j+1):]) < h:
            part1 += 1
        elif i == 0 or np.amax(grid[:i, j]) < h:
            part1 += 1
        elif i == n - 1 or np.amax(grid[(i+1):, j]) < h:
            part1 += 1

dd = [[0, 1], [0, -1], [1, 0], [-1, 0]]
for i in range(n):
    for j in range(m):
        h = grid[i, j]
        score = 1

        # Scan in 4 directions
        for di, dj in dd:
            ii, jj = i + di, j + dj
            dist = 0

            while (0 <= ii < n and 0 <= jj < m) and grid[ii, jj] < h:
                dist += 1
                ii += di
                jj += dj

                if (0 <= ii < n and 0 <= jj < m) and grid[ii, jj] >= h:
                    dist += 1

            score *= dist

        part1 = max(part2, score)


print("part 1 - ", part1)
print("part 2 - ", part2)

# answers 
# Part 1 
# Part 2