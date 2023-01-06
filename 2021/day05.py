import numpy as np

def parse(input):
    begin, _, end = input.split(" ")
    begin = [int(i) for i in begin.split(",")]
    end = [int(i) for i in end.split(",")]
    return begin, end

def parse2(input):
    begin, _, end = input.split(" ")
    begin = [int(i) for i in begin.split(",")]
    end = [int(i) for i in end.split(",")]

    direc = [sign(end[0] - begin[0]), sign(end[1] - begin[1])]
    return begin, end, direc

def sign(x):
    if x > 0:
        return 1
    if x < 0:
        return -1
    return 0

def part01(input):
    lines = [parse(line) for line in input]

    lines = [li for li in lines
            if li[0][0] == li[1][0] or li[0][1] == li[1][1]]

    x = 0
    y = 0
    for line in lines:
        x = max(x, line[0][0], line[1][0])
        y = max(y, line[0][1], line[1][1])

    cover = np.zeros((x + 1, y + 1))
    for line in lines:
        start, end = line
        if start[0] == end[0]:
            bottom = min(start[1], end[1])
            top = max(start[1], end[1])
            for yy in range(bottom, top + 1):
                cover[start[0]][yy] += 1

        else:
            assert start[1] == end[1]
            left = min(start[0], end[0])
            right = max(start[0], end[0])
            for xx in range(left, right + 1):
                cover[xx][start[1]] += 1


    # Find out how many points are covered more than once
    ans = 0
    for count in cover.flatten():
        ans += count >= 2

    print(cover.transpose())
    print(ans)

def part02(input):
    lines = [parse(line) for line in input]

    x = 0
    y = 0
    for line in lines:
        x = max(x, line[0][0], line[1][0])
        y = max(y, line[0][1], line[1][1])

    cover = np.zeros((x + 1, y + 1))
    for line in lines:
        begin, end, direc = line
        p = begin
        while p != end:
            cover[p[0], p[1]] += 1
            p[0] += direc[0]
            p[1] += direc[1]
        cover[end[0]][end[1]] += 1

    ans = 0
    for count in cover.flatten():
        ans += count >= 2

    cover.transpose()

    print(ans)

if __name__ == "__main__":
   with open("day05.txt") as fin:
    input = fin.read().strip().split("\n")
part01(input) #Answer  5092
part02(input) #Answer 20484