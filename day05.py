from collections import deque

filename = "day05.txt"

stacks = []
moves = []
moves2 = []

with open(filename, "r") as f:
    for line in f:
        if len(line) > 1 and line[1] == '1':
            stackCount = int(line.strip()[-1])
            for i in range(0, stackCount):
                stacks.append(deque([]))
with open(filename, "r") as f:
    for line in f:
        if 'move' in line:
            splitLine = line.strip().split(' ')
            moves.append([int(splitLine[1]), int(
                splitLine[3]), int(splitLine[5])])
            moves2.append([int(splitLine[1]), int(
                splitLine[3]), int(splitLine[5])])
        else:
            if line != '\n' and line[1] != '1':
                stripLine = line.strip().split(' ')
                for i in range(0, stackCount):
                    if stripLine[i][1] != '.': # had to modify puzzle input to have [.] in it to stop blank spaces causing issues. probably not the best answer here
                        stacks[i].append(stripLine[i][1])

for move in moves:
    for i in range(0, move[0]):
        el = stacks[move[1] - 1].popleft()
        stacks[move[2] - 1].appendleft(el)

sol = ''
sol2 = ''
for j in range(0, stackCount):
    sol += stacks[j][0]
print("part 1 -", sol)



def part2(): 
    for move in moves2:
        els = []
        for i in range(0, move[0]):
            els.append(stacks[move[1] - 1].popleft())
        for i in range(0, move[0]):
            stacks[move[2] - 1].appendleft(els.pop())
    for j in range(0, stackCount):
        sol2 += stacks[j][0]
print("part 2 -", part2())

# answers 
# Part 1 - RFFFWBPNS
# Part 2 - CQQBBJFCS