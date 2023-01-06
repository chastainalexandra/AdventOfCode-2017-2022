
def parsePart1(line):
    cmd, amount = line.split(" ")
    amount = int(amount)
    if cmd == "forward":
        return (amount, 0)
    elif cmd == "down":
        return (0, amount)
    else:
        return (0, -amount)

def parsePart2(line, prev_aim):
    cmd, amount = line.split(" ")
    amount = int(amount)
    if cmd == "forward":
        return (amount, amount * prev_aim, 0)
    elif cmd == "down":
        return (0, 0, amount)
    else:
        return (0, 0, -amount)

def part01(input):
    pos, depth = 0, 0

    for line in input:
        dpos, ddepth = parsePart1(line)
        pos += dpos
        depth += ddepth

    ans = pos * depth
    print(ans)

def part02(input):
    pos, depth, aim = 0, 0, 0

    for line in input:
        dpos, ddepth, daim = parsePart2(line, aim)
        pos += dpos
        depth += ddepth
        aim += daim

    ans = pos * depth
    print(ans)

if __name__ == "__main__":
   with open("day02.txt") as fin:
    input = fin.read().strip().split("\n")
part01(input) #Answer 2027977 
part02(input) #Answer 1903644897
