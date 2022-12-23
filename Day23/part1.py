from collections import defaultdict

with open("puzzleFile.txt") as fin:
    dataFile = fin.read().strip().split("\n")


checks = [
    [[1, 2, 3], 2],
    [[7, 6, 5], 6],
    [[5, 4, 3], 4],
    [[1, 0, 7], 0]
]


n = len(dataFile)
m = len(dataFile[0])

elves = set()

for i in range(n):
    for j in range(m):
        if dataFile[i][j] == "#":
            elves.add((i, j))


dirs = [
    [0, 1],
    [-1, 1],
    [-1, 0],
    [-1, -1],
    [0, -1],
    [1, -1],
    [1, 0],
    [1, 1]
]


def get_bounds(elves):
    min_row = 1 << 60
    max_row = -(1 << 60)
    min_col = 1 << 60
    max_col = -(1 << 60)

    for row, col in elves:
        min_row = min(min_row, row)
        max_row = max(max_row, row)

        min_col = min(min_col, col)
        max_col = max(max_col, col)

    return min_row, max_row, min_col, max_col


def print_elves(elves):
    min_row, max_row, min_col, max_col = get_bounds(elves)
    for row in range(min_row, max_row + 1):
        for col in range(min_col, max_col + 1):
            if (row, col) in elves:
                print("#", end="")
            else:
                print(".", end="")

        print()


for round in range(10):
    # Stage 1: make proposals
    propose = {}
    proposed = defaultdict(int)
    for elf in elves:
        row, col = elf

        # If elf has no neighbors, continue
        good = False
        for drow, dcol in dirs:
            if (row + drow, col + dcol) in elves:
                good = True
                break
        if not good:
            continue

        for check_dirs, propose_dir in checks:
            good = True
            for d in check_dirs:
                drow, dcol = dirs[d]
                if (row + drow, col + dcol) in elves:
                    good = False
                    break

            if not good:
                continue

            drow, dcol = dirs[propose_dir]
            propose[elf] = (row + drow, col + dcol)
            proposed[propose[elf]] += 1
            break

    # Stage 2: do the proposals
    new_elves = set()
    for elf in elves:
        if elf in propose:
            new_loc = propose[elf]
            if proposed[new_loc] > 1 or proposed[new_loc] == 0:
                new_elves.add(elf)
            else:
                new_elves.add(new_loc)
        else:
            new_elves.add(elf)

    # Rotate stuff
    checks = checks[1:] + [checks[0]]
    elves = new_elves


# Find bounding box
min_row, max_row, min_col, max_col = get_bounds(elves)

print(min_row, max_row)
print(min_col, max_col)

print((max_row - min_row + 1) * (max_col - min_col + 1) - len(elves))