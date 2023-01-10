import numpy as np
from itertools import product # used for Functions creating iterators for efficient looping

# After 100 steps, there have been a total of 1656 flashes.
#Given the starting energy levels of the dumbo octopuses in your cavern, simulate 100 steps. How many total flashes are there after 100 steps?

def part01(data):
    answer = 0
    dataLength = len(data)
    octopuses = data

    for steps in range(100):
        flashed = np.zeros((dataLength, dataLength), dtype=bool)


    print(answer)

def part02(data):
  print(data)
if __name__ == "__main__":
   with open("day11.txt") as fin:
    input = fin.read().strip()
    data = np.array([[int(d) for d in list(i)] for i in input.split("\n")], dtype=int)
part01(data) #Answer  
part02(data) #Answer 