# The walls of the valley are drawn as #;
# everything else is ground. 
# Clear ground - where there is currently no blizzard - is drawn as .. 
# Otherwise, blizzards are drawn with an arrow indicating their direction of motion: up (^), down (v), left (<), or right (>).

walls = "#"
noBlizzard = ".."
blizzardUp = "^"
blizzardDown = ""
blizzardLeft = "<"
blizzardRight = ">"




def part01(lines):
    print("part 1 - ",)

def part02(lines):
    print("part 2 - ",)

if __name__ == "__main__":
   with open("day24.txt") as fin:
    lines = fin.read().strip().split("\n")
   part01(lines)
   part02(lines)


# answers 
# Part 1 - 
# Part 2 - 