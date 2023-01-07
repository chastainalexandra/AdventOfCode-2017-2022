# --- Day 9: Smoke Basin ---
import numpy as np

def part01(input_map):
    r = len(input_map)
    c = len(input_map[0])

    ans = 0
   
    for row in range(r):
        for col in range(c):
            low = True
            for d in [(0, 1), (0, -1), (-1, 0), (1, 0)]:
                rr = row + d[0]
                cc = col + d[1]

                if not ((0 <= rr and rr < r) and (0 <= cc and cc < c)):
                    continue

                if input_map[rr][cc] <= input_map[row][col]:
                    low = False
                    break

            #  Find all of the low points on your heightmap. 
            # What is the sum of the risk levels of all low points on your heightmap?
            if low:
                ans += input_map[row][col] + 1
    print(ans)

def part02(input_map):
    r = len(input_map)
    c = len(input_map[0])

    l = []
    id = 1
    ids = np.zeros((r, c), dtype=int)

    for row in range(r):
        for col in range(c):
            low = True
            for d in [(0, 1), (0, -1), (-1, 0), (1, 0)]:
                rr = row + d[0]
                cc = col + d[1]

                if not ((0 <= rr and rr < r) and (0 <= cc and cc < c)):
                    continue

                if input_map[rr][cc] <= input_map[row][col]:
                    low = False
                    break

            if low:
                l.append((row, col))

    for row, col in l:
        stack = [(row, col)]
        mark = set()
        while len(stack) > 0:
            row, col = stack.pop()

            if (row, col) in mark:
                continue
            mark.add((row, col))

            ids[row, col] = id

            for d in [(0, 1), (0, -1), (-1, 0), (1, 0)]:
                rr = row + d[0]
                cc = col + d[1]

                if not ((0 <= rr and rr < r) and (0 <= cc and cc < c)):
                    continue

                if input_map[rr][cc] == 9:
                    continue

                stack.append((rr, cc))

        id += 1

    # Find basins sizes
    basins = [0] * id

    for x in ids.flatten():
        basins[x] += 1
    basins = basins[1:]

    basins.sort()
    # What do you get if you multiply together the sizes of the three largest basins?
    print(basins[-1] * basins[-2] * basins[-3])

if __name__ == "__main__":
   with open("day09.txt") as fin:
    input = fin.read().strip().split("\n")
    input_map = [[int(i) for i in list(line)] for line in input]

part01(input_map) #Answer  491
part02(input_map) #Answer 1075536