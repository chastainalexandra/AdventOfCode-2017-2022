# The distress signal leads you to a giant waterfall! Actually, hang on - the signal seems like it's coming from the waterfall itself, and that doesn't make any sense. 
# However, you do notice a little path that leads behind the waterfall.
# Correction: the distress signal leads you behind a giant waterfall! There seems to be a large cave system here, and the signal definitely leads further inside.
# As you begin to make your way deeper underground, you feel the ground rumble for a moment. 
# Sand begins pouring into the cave! If you don't quickly figure out where the sand is going, you could quickly become trapped!
# Fortunately, your familiarity with analyzing the path of falling material will come in handy here. 
# You scan a two-dimensional vertical slice of the cave above you (your puzzle input) and discover that it is mostly air with structures made of rock.
# Your scan traces the path of each solid rock structure and reports the x,y coordinates that form the shape of the path, where x represents distance to the right and y represents distance down. 
# Each path appears as a single line of text in your scan. After the first point of each path, each point indicates the end of a straight horizontal or vertical line to be drawn from the previous point.

sand_source = 500, 0
filled = set()

def part01(lines): 
    for line in lines:
        coords = []

        for str_coord in line.split(" -> "):
            x, y = map(int, str_coord.split(","))
            coords.append((x, y))

        for i in range(1, len(coords)):
            cx, cy = coords[i]  # cur
            px, py = coords[i - 1]

            if cy != py:
                assert cx == px
                for y in range(min(cy, py), max(cy, py) + 1):
                    filled.add((cx, y))

            if cx != px:
                assert cy == py
                for x in range(min(cx, px), max(cx, px) + 1):
                    filled.add((x, cy))


    max_y = max([coord[1] for coord in filled])

    def simulate_sand():
        global filled
        x, y = 500, 0

        while y <= max_y:
            if (x, y + 1) not in filled:
                y += 1
                continue

            if (x - 1, y + 1) not in filled:
                x -= 1
                y += 1
                continue

            if (x + 1, y + 1) not in filled:
                x += 1
                y += 1
                continue

            # Everything filled, come to rest
            filled.add((x, y))
            return True

        return False

    ans = 0

    while True:
        res = simulate_sand()
        if not res:
            break
        ans += 1
    print(ans)

# You realize you misread the scan. There isn't an endless void at the bottom of the scan - there's floor, and you're standing on it!
# You don't have time to scan the floor, so assume the floor is an infinite horizontal line with a y coordinate equal to two plus the highest y coordinate of any point in your scan.
# In the example above, the highest y coordinate of any point is 9, and so the floor is at y=11.
#  (This is as if your scan contained one extra rock path like -infinity,11 -> infinity,11.) With the added floor, the example above now looks like this:

def part02(lines): 
    ans = 0
    print(ans)

if __name__ == "__main__":
   with open("day14.txt") as fin:
    lines = fin.read().strip().split("\n")
   part01(lines)
   part02(lines)

# answers 
# Part 1 - 768
# Part 2 - 26686