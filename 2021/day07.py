def part01(data):
    position = max(data)

    for pos in range(position):
        req = 0
        for i in data:
            req += abs(pos - i)
        ans = min(ans, req)

    print(ans)

def part02(data):
    position = max(data)

    for pos in range(position):
        req = 0
        for i in data:
            distance = abs(i - pos)
            cost = distance * (distance + 1) // 2
            req += cost
        ans = min(ans, req)

    print(ans)

if __name__ == "__main__":
   with open("day07.txt") as fin:
    input = fin.read().strip().split(",")
    data = [int(i) for i in input]
part01(data) #Answer  344735
part02(data) #Answer 96798233