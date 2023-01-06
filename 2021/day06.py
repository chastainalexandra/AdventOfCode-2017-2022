from collections import defaultdict

def part01(lanternfish):
    days = 80

    for _ in range(days):
        n = len(lanternfish)
        for i in range(n):
            if lanternfish[i] == 0:
                lanternfish[i] = 6
                lanternfish.append(8)
            else:
                lanternfish[i] -= 1

    ans = len(lanternfish)
    print(ans)

def part02(lanternfish):
    days = 256
    for _ in range(days):
        newFish = defaultdict(int)

        for key in lanternfish:
            if key == 0:
                newFish[6] += lanternfish[key]
                newFish[8] = lanternfish[key]
            else:
                newFish[key - 1] += lanternfish[key]

        lanternfish = newFish

    ans = 0
    for key in lanternfish:
        ans += lanternfish[key]
    print(ans)

if __name__ == "__main__":
   with open("day06.txt") as fin:
    input = fin.read().strip().split(",")
    lanternfish = [int(i) for i in input]
part01(lanternfish) #Answer  394994

with open("day06.txt") as fin:
    input = fin.read().strip().split(",")
    lanternfish = defaultdict(int)
    for i in input:
        lanternfish[int(i)] += 1
part02(lanternfish) #Answer 1765974267455