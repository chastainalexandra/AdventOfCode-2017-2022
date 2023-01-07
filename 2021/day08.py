def part01(data):
    good = [2, 4, 3, 7]

    ans = 0
    for output in data:
        for digit in output:
            if len(digit) in good:
                ans += 1

    print(ans)

#def part02(data):

if __name__ == "__main__":
   with open("day08.txt") as fin:
    input = fin.read().strip().split(",")
    data = [line[line.index("|") + 2:].split(" ") for line in input]
part01(data) #Answer  to high currently
#part02(data) #Answer 

