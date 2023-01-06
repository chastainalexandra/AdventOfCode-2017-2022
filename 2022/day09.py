# This rope bridge creaks as you walk along it. You aren't sure how old it is, or whether it can even support your weight.

# It seems to support the Elves just fine, though. The bridge spans a gorge which was carved out by the massive river far below you.

# You step carefully; as you do, the ropes stretch and twist. You decide to distract yourself by modeling rope physics; maybe you can even figure out where not to step.

# Consider a rope with a knot at each end; these knots mark the head and the tail of the rope. If the head moves far enough away from the tail, the tail is pulled toward the head.

# Due to nebulous reasoning involving Planck lengths, you should be able to model the positions of the knots on a two-dimensional grid. Then, by following a hypothetical series of motions (your puzzle input) for the head, you can determine how the tail will move.

# Due to the aforementioned Planck lengths, the rope must be quite short; in fact, the head (H) and tail (T) must always be touching (diagonally adjacent and even overlapping both count as touching):

# A rope snaps! Suddenly, the river is getting a lot closer than you remember. The bridge is still there, but some of the ropes that broke are now whipping toward you as you fall through the air!

# The ropes are moving too quickly to grab; you only have a few seconds to choose how to arch your body to avoid being hit. Fortunately, your simulation can be extended to support longer ropes.

# Rather than two knots, you now must simulate a rope consisting of ten knots. One knot is still the head of the rope and moves according to the series of motions. Each knot further down the rope follows the knot in front of it using the same rules as before.

# Using the same series of motions as the above example, but with the knots marked H, 1, 2, ..., 9, the motions now occur as follows:


dataFile = open("day09.txt").read().splitlines()


def adjacent(x, y):
    directions = [(1, 0), (0, 1), (-1, 0), (0, -1), (-1, -1), (1, 1), (-1, 1), (1, -1)]
    adjacent = []
    for i, j in directions:
        new_x = x + i
        new_y = y + j
        adjacent.append((new_x, new_y))
    return adjacent


rope = [(0, 0) for _ in range(10)]
visited = [[] for _ in range(9)]
directions = {'U': (0, -1), 'R': (1, 0), 'D': (0, 1), 'L': (-1, 0)}
for line in dataFile:
    dir_, count = line.split(" ")
    for _ in range(int(count)):
        i = 9
        while i >= 1:
            visited[i-1].append((rope[i-1][0], rope[i-1][1]))
            if i == 9:
                head_x_move, head_y_move = directions[dir_]
                rope[i] = (rope[i][0]+head_x_move, rope[i][1]+head_y_move)

            if ((rope[i][0], rope[i][1]) in adjacent(rope[i-1][0], rope[i-1][1])) or\
               (rope[i][0], rope[i][1]) == (rope[i-1][0], rope[i-1][1]):
                break
            else:
                for adj_x, adj_y in adjacent(rope[i][0], rope[i][1]):
                    if (adj_x, adj_y) in adjacent(rope[i-1][0], rope[i-1][1]):
                        rope[i-1] = (adj_x, adj_y)
                        visited[i-1].append((rope[i-1][0], rope[i-1][1]))
                        i -= 1
                        break

print("Part 1 - ",len(set(visited[-1])), "Part 2 - ",len(set(visited[0])))


# answers 
# Part 1 - 6406
# Part 2 - 2643