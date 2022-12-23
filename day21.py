
from pprint import pprint

with open("day21.txt") as fin:
    lines = fin.read().strip().split("\n")


lookup = {}


def compute(name):
    if isinstance(lookup[name], int):
        return lookup[name]

    parts = lookup[name]

    left = compute(parts[0])
    right = compute(parts[2])

    return eval(f"{left}{parts[1]}{right}")


for line in lines:
    parts = line.split(" ")

    monkey = parts[0][:-1]

    if len(parts) == 2:
        lookup[monkey] = int(parts[1])
    else:
        lookup[monkey] = parts[1:]


pprint(lookup)

ans = compute("root")
print(ans)

# answers 
# Part 1 - 78342931359552
# Part 2 - 