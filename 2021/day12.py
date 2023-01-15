from collections import defaultdict, deque
from pprint import pprint


def is_small(cave):
    return cave.islower()

def finPath(cave, visited, adjacent):
    global ans

    if cave == "end": # your destination is the cave named end
        ans += 1
        return

    if is_small(cave) and cave in visited:
        return

    if is_small(cave):
        visited.add(cave)

    
    for adj in adjacent[cave]:
        if adj == "start":
            # Don't explore start cave again
            continue
        finPath(adj, visited, adjacent)

    # At the end, remove this from the DFS
    if is_small(cave):
        visited.remove(cave)


# How many paths through this cave system are there that visit small caves at most once?
def part01(data):
    adjacent = defaultdict(list)
    for a, b in data:
        adjacent[a].append(b)
        adjacent[b].append(a)
    global ans
    ans = 0
    visited = set()
    finPath("start", visited, adjacent)    # You start in the cave named start
    print(ans)


# Given these new rules, how many paths through this cave system are there?
def part02(data):
    print(data)

if __name__ == "__main__":
   with open("day12.txt") as fin:
    input = fin.read().strip()
    data = [i.split("-") for i in input.split("\n")]
part01(data) #Answer 3292
part02(data) #Answer 