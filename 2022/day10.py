# You avoid the ropes, plunge into the river, and swim to shore.
# The Elves yell something about meeting back up with them upriver, but the river is too loud to tell exactly what they're saying. They finish crossing the bridge and disappear from view.
# Situations like this must be why the Elves prioritized getting the communication system on your handheld device working. 
# You pull it out of your pack, but the amount of water slowly draining from a big crack in its screen tells you it probably won't be of much immediate use.
# Unless, that is, you can design a replacement for the device's video system! It seems to be some kind of cathode-ray tube screen and simple CPU that are both driven by a precise clock circuit.
# The clock circuit ticks at a constant rate; each tick is called a cycle.
# Start by figuring out the signal being sent by the CPU. The CPU has a single register, X, which starts with the value 1. It supports only two instructions:
# addx V takes two cycles to complete. After two cycles, the X register is increased by the value V. (V can be negative.)
# noop takes one cycle to complete. It has no other effect.
# The CPU uses these instructions in a program (your puzzle input) to, somehow, tell the screen what to draw.

def part01(lines):
    X = 1
    op = 0

    ans = 0

    # Maybe you can learn something by looking at the value of the X register throughout execution.
    # For now, consider the signal strength (the cycle number multiplied by the value of the X register) 
    # during the 20th cycle and every 40 cycles after that 
    # (that is, during the 20th, 60th, 100th, 140th, 180th, and 220th cycles).
    signals = [20, 60, 100, 140, 180, 220]

    # Consider the following small program:
    # noop
    # addx 3
    # addx -5

    for line in lines:
        parts = line.split(" ")

        if parts[0] == "noop":
            op += 1

            if op in signals:
                ans += op * X

        elif parts[0] == "addx":
            V = int(parts[1])
            X += V

            op += 1

            if op in signals:
                ans += op * (X - V)

            op += 1

            if op in signals:
                ans += op * (X - V)

    print("part 1 - ", ans)

# It seems like the X register controls the horizontal position of a sprite. 
# Specifically, the sprite is 3 pixels wide, and the X register sets the horizontal position 
# of the middle of that sprite. (In this system, there is no such thing as "vertical position":
# if the sprite's horizontal position puts its pixels where the CRT is currently drawing, then those pixels will be drawn.)
# You count the pixels on the CRT: 40 wide and 6 high. 
# This CRT screen draws the top row of pixels left-to-right, then the row below that, and so on. The left-most pixel in each row is in position 0, and the right-most pixel in each row is in position 39.
# Like the CPU, the CRT is tied closely to the clock circuit: 
# the CRT draws a single pixel during each cycle.
# Representing each pixel of the screen as a #, here are the cycles during which the first and last pixel in each row are drawn:

def part02(lines): 
    X = 1
    op = 0
    ans = 0
    row = 0
    col = 0 

    cycles = [1] * 241 # 241 is the cycles 

    for line in lines:
        parts = line.split(" ")

        if parts[0] == "noop":
            op += 1
            cycles[op] = X

        elif parts[0] == "addx":
            part = int(parts[1])

            cycles[op + 1] = X
            X += part

            op += 2
            cycles[op] = X

        ans = [[None] * 40 for _ in range(6)]

        for row in range(6):
            for col in range(40):
                counter = row * 40 + col + 1
                if abs(cycles[counter - 1] - (col)) <= 1:
                    ans[row][col] = "##"
                else:
                    ans[row][col] = "  "


        for row in ans:
            print("".join(row))
        print("part 2 - ", ans)


if __name__ == "__main__":
   with open("day10.txt") as fin:
    lines = fin.read().strip().split("\n")
   part01(lines)
   part02(lines)

# answers 
# Part 1 - 13760
# Part 2 - RFKZCPEF