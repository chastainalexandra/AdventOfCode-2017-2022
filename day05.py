from collections import deque
import re

def part01(stackCount,moves, stacks):
    sol = ''
    for move in moves:
        for i in range(0, move[0]):
            el = stacks[move[1] - 1].popleft()
            stacks[move[2] - 1].appendleft(el)


    for j in range(0, stackCount):
        sol += stacks[j][0]
    print("part 1 -", sol)



# def part02(stackCount, moves, stacks): 
#     sol2 = ''
#     for move in moves:
#         els = []
#         for i in range(0, move[0]):
#             els.append(stacks[move[1] - 1].popleft()) #error here ! 
#         for i in range(0, move[0]):
#             stacks[move[2] - 1].appendleft(els.pop())
#     for j in range(0, stackCount):
#         sol2 += stacks[j][0]
#     print("part 2 -", sol2)

def part02():
    def get_stacks(text: list[str]):
        stacks = [[] for x in range(len(text[-1].strip().split()))]
        for line in text[:-1][::-1]:
            line = line[:-1]
            for i in range(len(stacks)):
                if len(line) < i *4 + 1:
                    continue
                entry = line[i*4+1]
                if entry != " ":
                    stacks[i].append(entry)
        return stacks


    def do_move(entry: str, stacks: list[list[str]]):
        m = re.match(r'move (\d+) from (\d+) to (\d+)', entry)
        amount = int(m.group(1))
        origin = int(m.group(2)) - 1
        dest = int(m.group(3)) - 1

        stacks[dest].extend(stacks[origin][-amount:])
        stacks[origin] = stacks[origin][:-amount]


    with open('day05.txt') as f:
        stack_text = []
        for line in f:
            if line.strip() == "":
                break
            stack_text.append(line)

        stacks = get_stacks(stack_text)

        for line in f:
            do_move(line.strip(), stacks)

    print(' '.join(s[-1] for s in stacks))


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
   # part02(stackCount, moves, stacks)
    part02()


  



# answers 
# Part 1 - RFFFWBPNS
# Part 2 - CQQBBJFCS