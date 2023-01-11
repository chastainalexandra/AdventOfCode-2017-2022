import numpy as np
from itertools import product # used for Functions creating iterators for efficient looping

# After 100 steps, there have been a total of 1656 flashes.
#Given the starting energy levels of the dumbo octopuses in your cavern, simulate 100 steps. How many total flashes are there after 100 steps?

def part01(data):
    answer = 0
    dataLength = len(data)
    octopuses = data

    for steps in range(100): # There are 100 octopuses arranged neatly in a 10 by 10 grid. Each octopus slowly gains energy over time and flashes brightly for a moment when its energy is full.
        energyLevel = np.zeros((dataLength, dataLength), dtype=bool)

        # You can model the energy levels and flashes of light in steps. During a single step, the following occurs:
        # First, the energy level of each octopus increases by 1.
        # Then, any octopus with an energy level greater than 9 flashes. This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally adjacent. 
        # If this causes an octopus to have an energy level greater than 9, it also flashes. This process continues as long as new octopuses keep having their energy level increased beyond 9. 
        # (An octopus can only flash at most once per step.)
        # Finally, any octopus that flashed during this step has its energy level set to 0, as it used all of its energy to flash.

        for x, y in product(range(dataLength), repeat=2):
            octopuses[x,y] += 1
        
        while True:
            next_step = False

            didOctyFlash = np.zeros((dataLength, dataLength), dtype=int)
            for x, y in product(range(dataLength), repeat=2):
                if not energyLevel[x,y] and octopuses[x,y] > 9: # Then, any octopus with an energy level greater than 9 flashes. 
                    answer += 1 # This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally adjacent.  
                    energyLevel[x,y] = True
                    next_step = True

                    for diagonalX, diagonalY in product(range(-1,2), repeat=2):
                        if diagonalX == diagonalY == 0:
                            continue
                        
                        xx = x + diagonalX
                        yy = y + diagonalY

                        if not (0 <= xx < dataLength and 0 <= yy < dataLength):
                            continue

                        didOctyFlash[xx, yy] += 1
            octopuses += didOctyFlash

            if not next_step: 
                break
    
    for x, y in product(range(dataLength), repeat=2):
        if energyLevel[x,y]:
            octopuses[x,y] = 0



    print(answer)

def part02(data):
  print(data)
if __name__ == "__main__":
   with open("day11.txt") as fin:
    input = fin.read().strip()
    data = np.array([[int(d) for d in list(i)] for i in input.split("\n")], dtype=int)
part01(data) #Answer  9853 <-- to high 
part02(data) #Answer 