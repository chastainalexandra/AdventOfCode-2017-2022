# You and the elephants finally reach fresh air. You've emerged near the base of a large volcano that seems to be actively erupting! Fortunately, the lava seems to be flowing away from you and toward the ocean.
# Bits of lava are still being ejected toward you, so you're sheltering in the cavern exit a little longer. Outside the cave, you can see the lava landing in a pond and hear it loudly hissing as it solidifies.
# Depending on the specific compounds in the lava and speed at which it cools, it might be forming obsidian! The cooling rate should be based on the surface area of the lava droplets, so you take a quick scan of a droplet as it flies past you (your puzzle input).
# Because of how quickly the lava is moving, the scan isn't very good; its resolution is quite low and, as a result, it approximates the shape of the lava droplet with 1x1x1 cubes on a 3D grid, each given as its x,y,z position.
# To approximate the surface area, count the number of sides of each cube that are not immediately connected to another cube. So, if your scan were only two adjacent cubes like 1,1,1 and 2,1,1, each cube would have a single side covered and five sides exposed, a total surface area of 10 sides.

import numpy as np

def part01(lines):
    filled = set()
    for line in lines:
        x, y, z = map(int, line.split(","))
        filled.add((x, y, z))

    ans = 0
    for x, y, z in filled:
        covered = 0

        pos = np.array((x, y, z))

        for coord in range(3):
            dpos = np.array([0, 0, 0])
            dpos[coord] = 1

            dneg = np.array([0, 0, 0])
            dneg[coord] = -1

            covered += tuple(pos + dpos) in filled
            covered += tuple(pos + dneg) in filled

        ans += 6 - covered

    print(ans)

# Something seems off about your calculation. The cooling rate depends on exterior surface area, but your calculation also included the surface area of air pockets trapped in the lava droplet.
# Instead, consider only cube sides that could be reached by the water and steam as the lava droplet tumbles into the pond. 
# The steam will expand to reach as much as possible, completely displacing any air on the outside of the lava droplet but never expanding diagonally.
# In the larger example above, exactly one cube of air is trapped within the lava droplet (at 2,2,5), so the exterior surface area of the lava droplet is 58.

def part02(lines, droplet, directions, maxZ, maxY, maxX):
    surfaceArea = 0

    for z in range(maxZ + 1):
        for y in range(maxY + 1):
            for x in range(maxX + 1):
                if droplet[z, y, x] == 1.0:
                    for dir in directions:
                            try: 
                                if (
                                    droplet[z + dir[2], y + dir[1], x + dir[0]] == 0.0
                                    or droplet[z + dir[2], y + dir[1], x + dir[0]] == -1.0
                                ):
                                    if (
                                        droplet[z + dir[2], y + dir[1], x + dir[0]]
                                        != -1.0
                                    ):
                                        continue
                                    surfaceArea += 1      
                            except:
                                surfaceArea += 1      
    print(surfaceArea)
  

if __name__ == "__main__":
   with open("day18.txt") as fin:
    lines = fin.read().strip().split("\n")

   part01(lines)


   maxX = 0
   maxY = 0
   maxZ = 0

   for line in lines:
    line = line.split(",")
    maxX = max(maxX, int(line[0]))
    maxY = max(maxY, int(line[1]))
    maxZ = max(maxZ, int(line[2]))

   droplet = np.zeros((maxZ + 1, maxY + 1, maxX + 1))
   directions = [[-1, 0, 0], [1, 0, 0], [0, -1, 0], [0, 1, 0], [0, 0, -1], [0, 0, 1]]

   for line in lines:
        line = line.split(",")
        droplet[int(line[2]), int(line[1]), int(line[0])] = 1.0

   part02(lines, droplet, directions, maxZ, maxY, maxX)

# answers 
# Part 1 - 4636
# Part 2 - 2572