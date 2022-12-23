from collections import deque

def part01(stackCount,moves, stacks):
    sol = ''
    for move in moves:
        for i in range(0, move[0]):
            el = stacks[move[1] - 1].popleft()
            stacks[move[2] - 1].appendleft(el)


    for j in range(0, stackCount):
        sol += stacks[j][0]
    print("part 1 -", sol)



def part02(stackCount, moves, stacks): 
    sol2 = ''
    for move in moves:
        els = []
        for i in range(0, move[0]):
            els.append(stacks[move[1] - 1].popleft())
        for i in range(0, move[0]):
            stacks[move[2] - 1].appendleft(els.pop())
    for j in range(0, stackCount):
        sol2 += stacks[j][0]
    print("part 2 -", sol2)

if __name__ == "__main__":
    filename = "day05.txt"
    stacks = []
    moves = []
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
                
            else:
                if line != '\n' and line[1] != '1':
                    stripLine = line.strip().split(' ')
                    for i in range(0, stackCount):
                        if stripLine[i][1] != '.': # had to modify puzzle input to have [.] in it to stop blank spaces causing issues. probably not the best answer here
                            stacks[i].append(stripLine[i][1])
    part01(stackCount, moves, stacks)
    part02(stackCount, moves, stacks)
  



# answers 
# Part 1 - RFFFWBPNS
# Part 2 - CQQBBJFCS