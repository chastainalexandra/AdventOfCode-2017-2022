# You climb the hill and again try contacting the Elves. However, you instead receive a signal you weren't expecting: a distress signal.
# Your handheld device must still not be working properly; the packets from the distress signal got decoded out of order. 
# You'll need to re-order the list of received packets (your puzzle input) to decode the message.
# Your list consists of pairs of packets; pairs are separated by a blank line. You need to identify how many pairs of packets are in the right order.

from functools import cmp_to_key

def compare(a, b):
    if isinstance(a, list) and isinstance(b, int):
        b = [b]
    if isinstance(a, int) and isinstance(b, list):
        a = [a]
    if isinstance(a, int) and isinstance(b, int):
        if a < b:
            return 1
        if a == b:
            return 0
        return -1

    if isinstance(a, list) and isinstance(b, list):
        i = 0
        while i < len(a) and i < len(b):
            x = compare(a[i], b[i])
            if x == 1:
                return 1
            if x == -1:
                return -1

            i += 1

        if i == len(a):
            if len(a) == len(b):
                return 0
            return 1  # a ended first

        return -1

def part01(data):

    ans = 0

    for i, block in enumerate(data):
        a, b = map(eval, block.split("\n"))
        if compare(a, b) == 1:
            ans += i + 1

    print(ans)

# Now, you just need to put all of the packets in the right order. Disregard the blank lines in your list of received packets.
# The distress signal protocol also requires that you include two additional divider packets:
# [[2]]
# [[6]]
# Using the same rules as before, organize all packets - the ones in your list of received packets as well as the two divider packets - into the correct order.

def part02(data): 
    lists = list(map(eval, data)) #error here!
    lists.append([[2]])
    lists.append([[6]])
    lists = sorted(lists, key=cmp_to_key(compare), reverse=True)


    for i, li in enumerate(lists):
        if li == [[2]]:
            a = i + 1
        if li == [[6]]:
            b = i + 1

    print(a * b)

    

if __name__ == "__main__":
   with open("day13.txt") as fin:
    lines = fin.read().strip().split("\n\n")

   part01(lines)
   part02(lines)

# answers 
# Part 1 - 6428
# Part 2 - 22464