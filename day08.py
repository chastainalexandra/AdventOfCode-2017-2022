import numpy as np

def part01(n,m,grid,part1): 
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
    print("part 1 - ", part1)

def part02(n,m,grid,part1): 
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

    print("part 2 - ", max(part1, score))


if __name__ == "__main__":
    with open("day08.txt", "r") as fin:
        dataFile = fin.read().strip().split()

    grid = [list(map(int, list(line))) for line in dataFile]

    n = len(grid)
    m = len(grid[0])

    grid = np.array(grid)

    part1 = 0
    part2 = 0

    part01(n,m,grid,part1)
    part02(n,m,grid,part1)



# answers 
# Part 1 - 1679
# Part 2 - 536625