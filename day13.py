# You climb the hill and again try contacting the Elves. However, you instead receive a signal you weren't expecting: a distress signal.
# Your handheld device must still not be working properly; the packets from the distress signal got decoded out of order. 
# You'll need to re-order the list of received packets (your puzzle input) to decode the message.
# Your list consists of pairs of packets; pairs are separated by a blank line. You need to identify how many pairs of packets are in the right order.

# Now, you just need to put all of the packets in the right order. Disregard the blank lines in your list of received packets.
# The distress signal protocol also requires that you include two additional divider packets:
# [[2]]
# [[6]]
# Using the same rules as before, organize all packets - the ones in your list of received packets as well as the two divider packets - into the correct order.

def cmpLst(left, right):
    for i in range(len(left)):
        if i == len(right):
            return False

        if type(left[i]) == list or type(right[i]) == list:
            l = left[i] if isinstance(left[i], list) else [left[i]]
            r = right[i] if isinstance(right[i], list) else [right[i]]
            cmp = cmpLst(l, r)
            if cmp:
                return True
            elif cmp is not None:
                return False
        elif not left[i] == right[i]:
            return left[i] < right[i]

    if len(left) < len(right):
        return True
    return None

with open('day13.txt') as f:
    lines = f.readlines()
    lines = [eval(line.strip()) for line in lines if not line == "\n"]

    pos2 = 1
    pos6 = 2
    sum = 0
    for i in range(0,len(lines),2):
        if not cmpLst(lines[i], lines[i+1]) == False:
            sum += int(i/2 + 1)

        pos2 += int(not cmpLst(lines[i], [[2]]) == False)
        pos2 += int(not cmpLst(lines[i+1], [[2]]) == False)
        pos6 += int(not cmpLst(lines[i], [[6]]) == False)
        pos6 += int(not cmpLst(lines[i+1], [[6]]) == False)

    print("Part 1:", sum)
    print("Part 2:", pos2 * pos6)

# # answers 
# # Part 1 - 6428
# # Part 2 - 22464
