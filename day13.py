# You climb the hill and again try contacting the Elves. However, you instead receive a signal you weren't expecting: a distress signal.
# Your handheld device must still not be working properly; the packets from the distress signal got decoded out of order. 
# You'll need to re-order the list of received packets (your puzzle input) to decode the message.
# Your list consists of pairs of packets; pairs are separated by a blank line. You need to identify how many pairs of packets are in the right order.

from functools import cmp_to_key
import json
from copy import deepcopy

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

def comparePart2(a,b):
    def compare(a, b):
        for i in range(len(a)):
            if i >= len(b):
                return False, None

            atype = type(a[i])
            btype = type(b[i])
            # print(f"Looking at {a[i]} and {b[i]}. {atype} vs {btype}")

            ## First pass: Upgrade singleton lists
            if atype == int and btype == list:
                a = deepcopy(a)
                a[i] = [a[i]]
                atype = type(a[i])
            elif atype == list and btype == int:
                b = deepcopy(b)
                b[i] = [b[i]]
                btype = type(b[i])

            if atype == int and btype == int:
                #  If the left integer is higher than the right integer,
                #  the inputs are not in the right order.
                if a[i] < b[i]:
                    # print(f"Returning true because left int is smaller {a[i]} < {b[i]}")
                    return True, True
                if a[i] > b[i]:
                    # print(f"Returning false because left int is higher {a[i]} > {b[i]}")
                    return False, None
            elif atype == list and btype == list:
                compare_result, compare_override = compare(a[i], b[i])
                if not compare_result:
                    # print("Returning false because compare returned false")
                    return False, None
                if compare_override:
                    # print("Returning true because compare returned true")
                    return True, True
            else:
                raise Exception(f"Unknown types {atype} {btype}")

    # We compared everything. We're either in a "left ran out" state or a
    # "everything is equal" state, which is somewhat indeterminate.
    return True, len(a) < len(b)



def comparePart2(a,b):
    resultsComp, overrideComp = comparePart2(a,b)
    if resultsComp and overrideComp: 
        return -1
    elif not resultsComp: 
        return 1
    else: 
        return 0

def getFormattedData():
     with open("day13.txt") as f:
        data = f.read().strip()
        blocks = data.split("\n\n")
        return [parse_block(block) for block in blocks]  

def parse_block(block: str):
    b1, b2 = block.split("\n")
    return json.loads(b1), json.loads(b2)   

def part02(): 
    data = getFormattedData()
    pairs = []

    for(left, right) in data: 
        pairs.append(left)
        pairs.append(right)
    
    pairs.append([[2]])
    pairs.append([[6]])

    pairs.sort(key=cmp_to_key(comparePart2))
    total = 1
    for i, p in enumerate(pairs,1): 
        if p == [[6]] or p == [[2]]:
            total *= i
    
    print(total)

if __name__ == "__main__":
   with open("day13.txt") as fin:
    lines = fin.read().strip().split("\n\n")

   part01(lines)
   part02()

# answers 
# Part 1 - 6428
# Part 2 - 22464