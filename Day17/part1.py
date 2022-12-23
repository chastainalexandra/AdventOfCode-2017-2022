import itertools as it

rock1 = ['####']
rock2 = [
    '.#.',
    '###',
    '.#.'
]
rock3 = [
    '..#',
    '..#',
    '###'
]
rock4 = [
    '#',
    '#',
    '#',
    '#'
]
rock5 = [
    '##',
    '##'
]

def rockgen():
    while True:
        yield rock1, 1
        yield rock2, 2
        yield rock3, 3
        yield rock4, 4
        yield rock5, 5

def getJetPattern():
    with open('puzzleFile.txt', 'r') as reader:
        line = reader.readline().strip()
        return line

def rockHeight(rock: list):
    return len(rock)

def getRockCoords(rock: list, rockPos: tuple):
    # i refers to the row number so it is the height (y value)
    # j refers to the col number so it refers to the width (x value)
    for i, row in enumerate(rock):
        for j, col in enumerate(row):
            if col == '#':
                yield (rockPos[0] + j, rockPos[1] + i)

def getTowerHeight(grid: dict):
	if not grid:
		return 0

	return -min(y for x, y in grid.keys())

def part1(rocks, jetPattern):
    grid = {}
    jetIndex = 0
    count = 1

    while True:
        if count == 2023:
            break
        
        count += 1
        rock, rockIndex = next(rocks)

        # floor is at 0
        # coords above will be negative
        rockGround = -getTowerHeight(grid)
        rockPos = (2, rockGround - 3 - rockHeight(rock))

        while True:
            jet = jetPattern[jetIndex]
            jetIndex = (jetIndex + 1) % len(jetPattern)

            if jet == '>':
                newRockPos = (rockPos[0] + 1, rockPos[1])
            elif jet == '<':
                newRockPos = (rockPos[0] - 1, rockPos[1])
            else:
                assert False, jet
            
            for rockPartPos in getRockCoords(rock, newRockPos):
                if not (0 <= rockPartPos[0] < 7 and rockPartPos[1] < 0 and rockPartPos not in grid):
                    # we cannot move this rock
                    break
            else:
                rockPos = newRockPos

            # down move
            newRockPos = (rockPos[0], rockPos[1] + 1)
            finishedFalling = False

            for rockPartPos in getRockCoords(rock, newRockPos):
                # check for the ground or rock
                if rockPartPos[1] >= 0 or rockPartPos in grid:
                    # rock has finished falling
                    finishedFalling = True
                    break
            else:
                rockPos = newRockPos

            if finishedFalling:
                for rockPartPos in getRockCoords(rock, rockPos):
                    assert rockPartPos not in grid
                    grid[rockPartPos] = '#'
                break
    
    height = getTowerHeight(grid)
    print(f'Part 1: {height}')

def runSimulation(rocks, jetPattern):
    grid = {}
    jetIndex = -1

    while True:
        rock, rockIndex = next(rocks)

        # floor is at 0
        # coords above will be negative
        rockGround = -getTowerHeight(grid)
        rockPos = (2, rockGround - 3 - rockHeight(rock))

        newRockPos = None

        while True:
            jetIndex = (jetIndex + 1) % len(jetPattern)
            jet = jetPattern[jetIndex]

            if jet == '>':
                newRockPos = (rockPos[0] + 1, rockPos[1])
            elif jet == '<':
                newRockPos = (rockPos[0] - 1, rockPos[1])
            else:
                assert False, jet
            
            assert newRockPos is not None
            for rockPartPos in getRockCoords(rock, newRockPos):
                if not (0 <= rockPartPos[0] < 7 and rockPartPos[1] < 0 and rockPartPos not in grid):
                    # we cannot move this rock
                    break
            else:
                rockPos = newRockPos

            # down move
            newRockPos = (rockPos[0], rockPos[1] + 1)
            finishedFalling = False

            for rockPartPos in getRockCoords(rock, newRockPos):
                # check for the ground or rock
                if rockPartPos[1] >= 0 or rockPartPos in grid:
                    # rock has finished falling
                    finishedFalling = True
                    break
            else:
                rockPos = newRockPos

            if finishedFalling:
                for rockPartPos in getRockCoords(rock, rockPos):
                    assert rockPartPos not in grid
                    grid[rockPartPos] = '#'
                
                # we can purge all bits from the grid that are no longer relevant
				# for each x from 0 to 6, find the min y occupied by a rock.
				# we can ignore any parts of the grid below the max of all those ys.

                yMins = [min((0, *(
					y for x, y in grid.keys()
					if x == x0 and grid[(x, y)] == '#'
                ))) for x0 in range(7)]

                ylim = max(yMins)
                keysToRemove = [(x, y) for x, y in grid.keys() if y > ylim + 1]

                for k in keysToRemove:
                    del grid[k]

                yield rockIndex, jetIndex, grid
                break

def normaliseGridState(gridState: dict):
    maxY = max(y for x, y in gridState.keys())

    return {
        (x, y - maxY): v
        for (x, y), v
        in gridState.items()
    }

def part1Sim(rocks, jetPattern):
    gridStates = runSimulation(rocks, jetPattern)
    list(it.islice(gridStates, 2021))
    rockIndex, jetIndex, grid = next(gridStates)
    return getTowerHeight(grid)

def main():
    rocks = rockgen()
    jetPattern = getJetPattern()
    part1(rocks, jetPattern)


if __name__ == "__main__":
    main()