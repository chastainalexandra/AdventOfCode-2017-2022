# With everything replanted for next year (and with elephants and monkeys to tend the grove), you and the Elves leave for the extraction point.# Partway up the mountain that shields the grove is a flat, open area that serves as the extraction point. It's a bit of a climb, but nothing the expedition can't handle.
# At least, that would normally be true; now that the mountain is covered in snow, things have become more difficult than the Elves are used to.
# As the expedition reaches a valley that must be traversed to reach the extraction site, you find that strong, turbulent winds are pushing small blizzards of snow and sharp ice around the valley. It's a good thing everyone packed warm clothes! To make it across safely, you'll need to find a way to avoid them.

from math import lcm 
from heapq import heappop, heappush

def isThereABlizzard(loc, st):
    for d in range(4):
        if (loc[0], loc[1], d) in st:
            return True
    return False

class Blizzard(object):
    def __init__(self, x, y, directionCharacter): #ooo the this equalivant for python about time i learn this
        self.x = x
        self.y = y
        self.directionCharacter = directionCharacter
    
    def blizzardDirection(self):
        if self.directionCharacter == '<':
            self.directionCharacter = '>'
        elif self.directionCharacter == '>':
            self.directionCharacter ='<'
        elif self.directionCharacter == 'v': 
            self.directionCharacter = '^'
        elif self.directionCharacter == "^":
            self.directionCharacter = 'v'
        else:
            # uh oh, we're lost
            return

def moreChancesForSnow(current_possibilities, next_grid):
	nextChanceForSnow = set()
	blizzardMovements = {
		(0, 0),
		(0, 1),
		(0, -1),
		(1, 0),
		(-1, 0)
	}

	for p in current_possibilities:
		for m in blizzardMovements:
			next_row = p[0] + m[0]
			next_column = p[1] + m[1]
			
			# you can always hide at the beginning of the maze
			if (next_row == -1) and (next_column == 0):
				nextChanceForSnow.add((next_row, next_column))
				continue

			# you can always hide at the end of the maze
			if (next_row == rows) and (next_column == columns - 1):
				nextChanceForSnow.add((next_row, next_column))
				continue

			# can't add if there's a blizzard
			if (next_row, next_column) in next_grid:
				continue

			if (next_row < 0) or (next_row >= rows):
				continue

			if (next_column < 0) or (next_column >= columns):
				continue

			nextChanceForSnow.add((next_row, next_column))

	return nextChanceForSnow

def stepIntoStorm(currentGrid):
    nextGrid = {}
    for point in currentGrid.keys():
        for blizzard in currentGrid[point]:
            next_row = point[0] + blizzardToDirection[blizzard][0]
            next_column = point[1] + blizzardToDirection[blizzard][1]
            if (next_row < 0) or (next_row >= rows):
                next_row = next_row % rows
            if (next_column < 0) or (next_column >= columns):
                next_column = next_column % columns
            if (next_row, next_column) in nextGrid.keys():
                nextGrid[(next_row, next_column)].append(blizzard)
            else:
                nextGrid[(next_row, next_column)] = [blizzard]

    return nextGrid

def part01():
    blizzardPoss = [{(-1, 0)}]

    while True:
        grid.append(stepIntoStorm(grid[-1]))
        blizzardPoss.append(moreChancesForSnow(blizzardPoss[-1], grid[-1]))
        if (rows, columns - 1) in blizzardPoss[-1]:
            break
    
    print(len(blizzardPoss) - 1)

# As the expedition reaches the far side of the valley, one of the Elves looks especially dismayed:
# He forgot his snacks at the entrance to the valley!
# Since you're so good at dodging blizzards, the Elves humbly request that you go back for his snacks. 
# From the same initial conditions, how quickly can you make it from the start to the goal, then back to the start, then back to the goal?
# In the above example, the first trip to the goal takes 18 minutes, the trip back to the start takes 23 minutes, and the trip back to the goal again takes 13 minutes,
#  for a total time of 54 minutes.
# What is the fewest number of minutes required to reach the goal, go back to the start, then reach the goal again?

def part02():
    with open("day24.txt") as fin:
        data = fin.read().split("\n")

    i = len(data)
    ii = len(data[0])
    startingPoint = (0,1)
    endingPoint = (i - 1, ii -2)

    point = lcm(i -2, ii-2)

    blizzardArrow = ">v<^"

    blizzardMovement = blizzardMovement =  [[0,1], [1,0], [0,-1], [-1,0]]

    blizzards = set()
    for xx in range(i):
        for yy in range(ii):
            c = data [xx][yy]
            if c in blizzardArrow:
                blizzards.add((xx,yy, blizzardArrow.index(c)))
    
    blizzard_state = [None] * point
    blizzard_state[0] = blizzards
    for o in range(1, point): 
        new_blizzard = set()

        for bliz in blizzards: 
            row, col, z = bliz
            zrow, zcol = blizzardMovement[z]
            newRow, newCol = row + zrow, col + zcol

            if newRow == 0: 
                assert z == 3
                newRow = i - 2
            elif newRow == i -1:
                assert z == 1
                newRow = 1 
            
            if newCol == 0:
                assert z == 2 
                newCol = ii -2 
            elif newCol == ii - 1: 
                assert z == 0
                newCol = 1
            
            new_blizzard.add((newRow, newCol, z))

            blizzard_state[o] = new_blizzard
            blizzards = new_blizzard

    pq = [(0, startingPoint, False, False)]
    visited = set()

    while len(pq) > 0:
        top = heappop(pq)
        if top in visited:
            continue
        visited.add(top)

        t, loc, hit_end, hit_start = top
        row, col = loc

        assert not (hit_start and not hit_end)

        # did we land on a blizzard?
        assert not isThereABlizzard(loc, blizzard_state[t % point])

        if loc == endingPoint:
            if hit_end and hit_start:
                print(t)
                break

            hit_end = True

        if loc == startingPoint:
            if hit_end:
                hit_start = True

        # Go through grid
        for zrow, zcol in (blizzardMovement + [[0, 0]]):
            new_row, new_col = row + zrow, col + zcol
            new_loc = (new_row, new_col)

            # are we stll on the board
            if (not new_loc in [startingPoint, endingPoint]) \
                and not (1 <= new_row <= i - 2
                        and 1 <= new_col <= ii - 2):
                continue

            # did we land on a blizzard?
            if isThereABlizzard(new_loc, blizzard_state[(t + 1) % point]):
                continue

            new_state = (t + 1, new_loc, hit_end, hit_start)
            heappush(pq, new_state)


    ans = [["."] * ii for _ in range(i)]
    for i in range(i):
            for j in range(ii):
                if (i in [0, i - 1] or j in [0, ii - 1]) and not (i, j) in [startingPoint, endingPoint]:
                    ans[i][j] = "#"
                    continue

                for d in range(4):
                    if (i, j, d) in blizzards:
                        if isinstance(ans[i][j], int):
                            ans[i][j] += 1
                        elif ans[i][j] != ".":
                            ans[i][j] = 2
                        else:
                            ans[i][j] = blizzardMovement[d]

    return ans

    # blizzardPoss = [{(-1, 0)}]

    # for i in range(556):
    #     grid.append(stepIntoStorm(grid[-1]))
    # while True:
    #     grid.append(stepIntoStorm(grid[-1]))
    #     blizzardPoss.append(moreChancesForSnow(
    #         blizzardPoss[-1], grid[-1]))

    #     if (rows, columns - 1) in blizzardPoss[-1]:
    #         break

    # print(len(grid) - 1)


# blizzards = []
# data = [line.strip() for line in open('day24.txt').readlines()]
# for y, line in enumerate (data):
#     for x, c in enumerate(data):
#         if c in '<^>v':
#             blizzards.append((x,y,c))
# maxX, maxY = len(data[0]) - 1, len(data) - 1
# move = {'<': (-1,0), '^':(0,-1), '>':(1,0), 'v':(0,1)}

# def step(blizzards):
#     new = []
#     for b in blizzards:
#         x, y = b[0] + move[b[2]][0], b[1] + move[b[2]][1]
#         if x == 0: x = maxX - 1
#         if x == maxX: x = 1
#         if y == 0: y = maxY - 1
#         if y == maxY: y = 1
#         new.append((x, y, b[2]))
#     return new


# def occupancy(blizzards):
#     return {(x, y) for x, y, c in blizzards}


# steps, lcm = {}, math.lcm(maxX - 1, maxY - 1)
# for i in range(lcm):
#     steps[i] = {(x, y) for x, y, _ in blizzards}
#     blizzards = step(blizzards)


# def trip(src, dest, step):
#     queue = [(src[0], src[1], step)]
#     while True:
#         x, y, step = queue.pop(0)
        
#         for x, y in [(x + m[0], y + m[1]) for m in move.values()] + [(x, y)]:
#             if (x, y) == (dest[0], dest[1]):
#                 return step + 1

#             if (x, y) != (src[0], src[1]):
#                 if x <= 0 or x >= maxX or y <= 0 or y >= maxY:
#                     continue

#             if (x, y) in steps[(step + 1) % lcm]:
#                 continue

#             if (x, y, step + 1) not in queue:
#                 queue.append((x, y, step + 1))

if __name__ == "__main__":
   with open("day24.txt") as fin:
    data = fin.read().splitlines()
    rows = len(data) - 2
    columns = len(data[0]) - 2
    grid = [{}]

    blizzardToDirection = {
        '>': (0, 1),
		'<': (0, -1),
		'^': (-1, 0),
		'v': (1, 0)
    }

for i in range(1, len(data) - 1):
	for j in range(1, len(data[1]) - 1):
		row = i - 1
		col = j - 1
		if data[i][j] != ".":
			grid[0][(row, col)] = [data[i][j]]


part01()
part02()
# trip1 = trip((1, 0), (maxX - 1, maxY), 0)
# trip2 = trip((maxX - 1, maxY), (1, 0), trip1)
# trip3 = trip((1, 0), (maxX - 1, maxY), trip2)
# print(trip3) answer to low 
# print(trip2)
# print(trip1) 



# answers 
# Part 1 - 274
# Part 2 - 1091 too high
# part 2 - 839 - correct! 
