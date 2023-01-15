from collections import defaultdict, deque
from pprint import pprint

def is_small(cave):
    return cave.islower()

def findPath(cave, visited, adjacent, part):
    global ans

    if cave == "end": # your destination is the cave named end
        ans += 1
        return

    if part != "part2":
        if is_small(cave) and cave in visited:
            return

        if is_small(cave):
            visited.add(cave)

    
        for adj in adjacent[cave]:
            if adj == "start":
                # Don't explore start cave again
                continue
            findPath(adj, visited, adjacent, "part1")

  
        if is_small(cave):
            visited.remove(cave)
    
    if part == "part2":
        if is_small(cave):
            visited[cave] += 1

        # Check if this cave is good to go
        visited_twice = 0  # How many small caves are visited more than once
        for small_cave in visited:
            visited_twice += visited[small_cave] > 1

            # No small cave can be visited more than twice
            if visited[small_cave] > 2:
                visited[cave] -= 1
                return

        if visited_twice > 1:
            visited[cave] -= 1
            return

        # Add all neighbors to queue
        for adj in adjacent[cave]:
            if adj == "start":
                # Don't explore start cave again
                continue
            findPath(adj, visited, adjacent, "part2")

    
        if is_small(cave):
            visited[cave] -= 1

# How many paths through this cave system are there that visit small caves at most once?
def part01(data):
    adjacent = defaultdict(list)
    for a, b in data:
        adjacent[a].append(b)
        adjacent[b].append(a)
    global ans
    ans = 0
    visited = set()
    findPath("start", visited, adjacent, "part1")    # You start in the cave named start
    print(ans)


# Given these new rules, how many paths through this cave system are there?
def part02(data):
    adjacent = defaultdict(list)
    for a, b in data:
        adjacent[a].append(b)
        adjacent[b].append(a)
    global ans
    ans = 0
    visited =  defaultdict(int)
    findPath("start", visited, adjacent, "part2")    # You start in the cave named start
    print(ans)

if __name__ == "__main__":
   with open("day12.txt") as fin:
    input = fin.read().strip()
    data = [i.split("-") for i in input.split("\n")]
part01(data) #Answer 3292
part02(data) #Answer 89592