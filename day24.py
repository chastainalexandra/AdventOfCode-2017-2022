# With everything replanted for next year (and with elephants and monkeys to tend the grove), you and the Elves leave for the extraction point.
# Partway up the mountain that shields the grove is a flat, open area that serves as the extraction point. It's a bit of a climb, but nothing the expedition can't handle.
# At least, that would normally be true; now that the mountain is covered in snow, things have become more difficult than the Elves are used to.
# As the expedition reaches a valley that must be traversed to reach the extraction site, you find that strong, turbulent winds are pushing small blizzards of snow and sharp ice around the valley. It's a good thing everyone packed warm clothes! To make it across safely, you'll need to find a way to avoid them.

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
    blizzardPoss = [{(-1, 0)}]

    for i in range(556):
        grid.append(stepIntoStorm(grid[-1]))
    while True:
        grid.append(stepIntoStorm(grid[-1]))
        blizzardPoss.append(moreChancesForSnow(
            blizzardPoss[-1], grid[-1]))

        if (rows, columns - 1) in blizzardPoss[-1]:
            break

    print(len(grid) - 1)

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


# answers 
# Part 1 - 274
# Part 2 - 1091
