from collections import deque
import copy

filename = "puzzleFile.txt"

# A shallow copy constructs a new compound object and then (to the extent possible)
# inserts references into it to the objects found in the original.
#A deep copy constructs a new compound object and then, recursively, 
# inserts copies into it of the objects found in the original.

# Types of Restricted Deque Input
# Input Restricted Deque:  Input is limited at one end while deletion is permitted at both ends.
# Output Restricted Deque: output is limited at one end but insertion is permitted at both ends.

def allUnique(b):
    for j in range(0, len(b)):
        substr = copy.deepcopy(b)
        del substr[j]
        if b[j] in substr:
            return False
    return True


def findMarker(line):
    i = 0
    buff = deque([])
    for ch in line:
        if len(buff) == 14: # change from last solution to 14 from 4
            buff.popleft()  #stays the same from last time 
        buff.append(ch) #stays the same from last time 
        if i >= 13 and allUnique(buff): #change from 3 to 13 for this line 
            return i + 1
        i += 1
    return (-1)


with open(filename, "r") as f: #standard read the file line 
    for line in f:
        print(findMarker(line))