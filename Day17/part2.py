import numpy as np

from util import lines #need to learn how to put these things in a helper folder 


def start_fall(chamber):
    empty_rows = (chamber == 0).all(axis=1)
    return np.argwhere(empty_rows)[-1, 0]


def overlap(chamber, shape, r, c):
    shape_r, shape_c = shape.shape
    o = (chamber[r : r + shape_r, c : c + shape_c] * shape).any()
    return o


def drop_rock(chamber, shape, moves, move_i):
    shape_r, shape_c = shape.shape
    r = start_fall(chamber) - 2 - shape_r
    c = 2
    while True:
        move = moves[move_i]
        if move == "<" and c - 1 >= 0 and not overlap(chamber, shape, r, c - 1):
            c -= 1

        elif move == ">" and c + 1 + shape_c <= 7 and not overlap(chamber, shape, r, c + 1):
            c += 1

        move_i = (move_i + 1) % len(moves)

        if r + 1 + shape_r <= chamber.shape[0] and not overlap(chamber, shape, r + 1, c):
            r += 1
        else:
            chamber[r : r + shape_r, c : c + shape_c] = shape + chamber[r : r + shape_r, c : c + shape_c]
            return chamber, move_i


def print_chamber(chamber, h=-1):
    r = start_fall(chamber)
    ch = ""
    if h == -1:
        for i in range(r, chamber.shape[0]):
            for j in range(chamber.shape[1]):
                if chamber[i, j]:
                    ch += "#"
                else:
                    ch += "."
            ch += "\n"
    else:
        if r + h > height:
            r = height - h
        for i in range(r, r + h):
            for j in range(chamber.shape[1]):
                if chamber[i, j]:
                    ch += "#"
                else:
                    ch += "."
            ch += "\n"
    return ch


if __name__ == "__main__":
    m = list(list(lines("puzzleFile.txt"))[0].strip())

    shapes = [
        np.ones((1, 4)),
        np.ones((3, 3)),
        np.ones((3, 3)),
        np.ones((4, 1)),
        np.ones((2, 2)),
    ]
    shapes[1][0::2, 0::2] = 0
    shapes[2][0:2, 0:2] = 0

    height = 35000
    chamber = np.zeros((height, 7))

    total_r = 1000000000000

    cycles = {}

    move_i = 0
    checking_remainder = False
    r = -1
    for i in range(20000):
        chamber, move_i = drop_rock(chamber, shapes[i % len(shapes)], m, move_i)

        cycle_check = (move_i, i % len(shapes), print_chamber(chamber, h=30))


        if not checking_remainder and cycle_check in cycles:
            prev_cycle_h, prev_cycle_i = cycles[cycle_check]

            period = i - prev_cycle_i
            end_cycle_height = height - start_fall(chamber) - 1
            cycle_height = end_cycle_height - prev_cycle_h
            cycle_repeats = (total_r - prev_cycle_i) // period
            remainder = (total_r - prev_cycle_i) % period
            checking_remainder = True
        else:
            cycles[cycle_check] = (height - start_fall(chamber) - 1, i)

        if checking_remainder:
            r += 1
            if r == remainder:
                r_height = height - start_fall(chamber) - 2 - end_cycle_height
                print(cycle_height * cycle_repeats + prev_cycle_h + r_height)
                break