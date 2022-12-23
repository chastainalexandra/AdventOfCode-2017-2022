# The monkeys are back! You're worried they're going to try to steal your stuff again, but it seems like they're just holding their ground and making various monkey noises at you.
# Eventually, one of the elephants realizes you don't speak monkey and comes over to interpret. As it turns out, they overheard you talking about trying to find the grove; they can show you a shortcut if you answer their riddle.
# Each monkey is given a job: either to yell a specific number or to yell the result of a math operation. All of the number-yelling monkeys know their number from the start; however, the math operation monkeys need to wait for two other monkeys to yell a number, and those two other monkeys might also be waiting on other monkeys.
# Your job is to work out the number the monkey named root will yell before the monkeys figure it out themselves.

def part01(lines):
    lookup = {}

    def compute(name):
        if isinstance(lookup[name], int):
            return lookup[name]

        parts = lookup[name]

        left = compute(parts[0])
        right = compute(parts[2])

        return eval(f"{left}{parts[1]}{right}")

    for line in lines:
        parts = line.split(" ")

        monkey = parts[0][:-1]

        if len(parts) == 2:
            lookup[monkey] = int(parts[1])
        else:
            lookup[monkey] = parts[1:]

    ans = compute("root")
    print(ans)

# Due to some kind of monkey-elephant-human mistranslation, you seem to have misunderstood a few key details about the riddle.
# First, you got the wrong job for the monkey named root; specifically, you got the wrong math operation. The correct operation for monkey root should be =, which means that it still listens for two numbers (from the same two monkeys as before), but now checks that the two numbers match.
# Second, you got the wrong monkey for the job starting with humn:. It isn't a monkey - it's you. Actually, you got the job wrong, too: you need to figure out what number you need to yell so that root's equality check passes. (The number that appears after humn: in your input is now irrelevant.)
# In the above example, the number you need to yell to pass root's equality test is 301. (This causes root to get the same number, 150, from both of its monkeys.)

#def part02():


if __name__ == "__main__":
   with open("day21.txt") as fin:
        lines = fin.read().strip().split("\n")
   part01(lines)
   #part02()
  


# answers 
# Part 1 - 78342931359552
# Part 2 - 