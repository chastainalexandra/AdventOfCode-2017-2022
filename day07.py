from collections import defaultdict
from functools import lru_cache
from pprint import pprint

with open("day07.txt") as fin:
    blocks = ("\n" + fin.read().strip()).split("\n$ ")[1:]


path = []

dir_sizes = defaultdict(int)
children = defaultdict(list)
visited = set()

def part1(block):
    lines = block.split("\n")
    command = lines[0]
    outputs = lines[1:]

    parts = command.split(" ")
    op = parts[0]
    if op == "cd":
        if parts[1] == "..":
            path.pop()
        else:
            path.append(parts[1])

        return

    abspath = "/".join(path)
    assert op == "ls"

    sizes = []
    for line in outputs:
        if not line.startswith("dir"):
            sizes.append(int(line.split(" ")[0]))
        else:
            dir_name = line.split(" ")[1]
            children[abspath].append(f"{abspath}/{dir_name}")

    dir_sizes[abspath] = sum(sizes)


for block in blocks:
    part1(block)


# Do DFS
@lru_cache(None)  # Cache may not be strictly necessary
def dfs(abspath):
    size = dir_sizes[abspath]
    for child in children[abspath]:
        size += dfs(child)
    return size


part01Ans = 0
for abspath in dir_sizes:
    if dfs(abspath) <= 100000:
        part01Ans += dfs(abspath)

print("part 1 - ", part01Ans)

def part2(block):
    lines = block.split("\n")
    command = lines[0]
    outputs = lines[1:]

    parts = command.split(" ")
    op = parts[0]
    if op == "cd":
        if parts[1] == "..":
            path.pop()
        else:
            path.append(parts[1])

        return

    abspath = "/".join(path)

    assert op == "ls"

    sizes = []
    for line in outputs:
        if not line.startswith("dir"):
            sizes.append(int(line.split(" ")[0]))
        else:
            dir_name = line.split(" ")[1]
            children[abspath].append(f"{abspath}/{dir_name}")

    dir_sizes[abspath] = sum(sizes)


for block in blocks:
    part2(block)

unused = 70000000 - dfs("/")
required = 30000000 - unused

ans = 1 << 60
for abspath in dir_sizes:
    size = dfs(abspath)
    if size >= required:
        ans = min(ans, size)

print("part 2 - ", ans)