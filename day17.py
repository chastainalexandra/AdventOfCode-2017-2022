from typing import List
import numpy as np
import math
from collections import defaultdict

# Your handheld device has located an alternative exit from the cave for you and the elephants.
#  The ground is rumbling almost continuously now, but the strange valves bought you some time.
#  It's definitely getting warmer in here, though.
# The tunnels eventually open into a very tall, narrow chamber.
#  Large, oddly-shaped rocks are falling into the chamber from above, presumably due to all the rumbling. 
# If you can't work out where the rocks will fall next, you might be crushed!
# The five types of rocks have the following peculiar shapes, where # is rock and . 
# is empty space:

def parse():
    with open("day17.txt") as file:
        line = file.readlines()[0]
        return list(line.strip())


def get_shape(i):
    return {
        0: shape_line,
        1: shape_plus,
        2: shape_l,
        3: shape_i,
        4: shape_square,
    }[i % 5]()


def shape_line():
    return np.ones((1, 4), dtype=bool)


def shape_plus():
    return np.array([[0, 1, 0], [1, 1, 1], [0, 1, 0]], dtype=bool)


def shape_square():
    return np.ones((2, 2), dtype=bool)


def shape_l():
    return np.array([[0, 0, 1], [0, 0, 1], [1, 1, 1]], dtype=bool)


def shape_i():
    return np.array([[1], [1], [1], [1]], dtype=bool)


def run(data, iterations=2022):
    height = 64
    bottom = 64  # highest rock in the room (or ground if there isn't one)

    field = np.zeros((height, 7), dtype=bool)
    width = 7

    piece_i = 0
    data_i = 0

    cache = defaultdict(list)
    cache2 = defaultdict(list)
    iteration = 0
    row_adj = 0
    while iteration < iterations:
        room_left = bottom
        if room_left < 7:
            bottom += height
            height *= 2
            new_field = np.zeros((height, width), dtype=bool)
            new_field[field.shape[0] :, 0:] = field
            field = new_field

        top24rows = field[bottom : bottom + 24]
        shape_i = iteration % 5
        direction = data[data_i % len(data)]
        key = (top24rows.tobytes(), shape_i, direction)
        if key in cache and len(cache2[key]) >= 2:
            skip_iterations = cache[key][1] - cache[key][0]
            add_rows = cache2[key][1] - cache2[key][0]

            for multiple in [7, 6, 5, 4, 3, 2, 1]:
                factor = 10**multiple
                if iteration + (skip_iterations * factor) < iterations:
                    iteration += skip_iterations * factor
                    row_adj += add_rows * factor
                    break
        else:
            cache[key].append(iteration)
            cache2[key].append(height - bottom)

        p = get_shape(iteration)

        px_sz = p.shape[1]
        py_sz = p.shape[0]
        px = 2
        py = (bottom - 3) - py_sz

        stopped = False
        while not stopped:
            # Pushed by a jet
            direction = data[data_i % len(data)]
            data_i += 1
            xd = 0
            if direction == ">":
                if px + px_sz + 1 <= width:
                    if not (field[py : py + py_sz, px + 1 : px + px_sz + 1] & p).any():
                        xd = 1
            elif direction == "<":
                if px - 1 >= 0:
                    if not (field[py : py + py_sz, px - 1 : px + px_sz - 1] & p).any():
                        xd = -1
            else:
                raise Exception("Unknown direction: " + direction)
            px += xd

            # Falling one unit down
            py += 1

            # Check if it is stopped
            if py + py_sz > height:
                stopped = True
            elif (field[py : py + py_sz, px : px + px_sz] & p).any():
                stopped = True

            if stopped:
                py -= 1
                field[py : py + py_sz, px : px + px_sz] |= p
                bottom = min(bottom, py)
        iteration += 1

    return (height - bottom) + row_adj


def display(a):
    for row in a:
        print("".join(["#" if x else "." for x in row]))


def lcm(a, b):
    return abs(a * b) // math.gcd(a, b)

if __name__ == "__main__":
    lines = parse()

    print(run(lines, 2022))
    print(run(lines, 10**2))


# answers 
# Part 1 - 3065
# Part 2 - 1562536022966