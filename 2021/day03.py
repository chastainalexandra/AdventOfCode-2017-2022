# The submarine has been making some odd creaking noises, so you ask it to produce a diagnostic report just in case.

# The diagnostic report (your puzzle input) consists of a list of binary numbers which, when decoded properly, can tell you many useful things about the conditions of the submarine. The first parameter to check is the power consumption.

# You need to use the binary numbers in the diagnostic report to generate two new binary numbers (called the gamma rate and the epsilon rate). The power consumption can then be found by multiplying the gamma rate by the epsilon rate.

# Each bit in the gamma rate can be determined by finding the most common bit in the corresponding position of all numbers in the diagnostic report. For example, given the following diagnostic report:

# 00100
# 11110
# 10110
# 10111
# 10101
# 01111
# 00111
# 11100
# 10000
# 11001
# 00010
# 01010
# Considering only the first bit of each number, there are five 0 bits and seven 1 bits. Since the most common bit is 1, the first bit of the gamma rate is 1.

# The most common second bit of the numbers in the diagnostic report is 0, so the second bit of the gamma rate is 0.

# The most common value of the third, fourth, and fifth bits are 1, 1, and 0, respectively, and so the final three bits of the gamma rate are 110.

# So, the gamma rate is the binary number 10110, or 22 in decimal.

# The epsilon rate is calculated in a similar way; rather than use the most common bit, the least common bit from each position is used. So, the epsilon rate is 01001, or 9 in decimal. Multiplying the gamma rate (22) by the epsilon rate (9) produces the power consumption, 198.

# Use the binary numbers in your diagnostic report to calculate the gamma rate and epsilon rate, then multiply them together. What is the power consumption of the submarine? (Be sure to represent your answer in decimal, not binary.)

def part01(input):
    numbers = len(input[0]) 

    gamma_rate = [None] * numbers
    epsilon_rate = [None] * numbers
    for i in range(numbers):
        zeros = sum([input[j][i] == "0" for j in range(len(input))])
        ones = sum([input[j][i] == "1" for j in range(len(input))])
        if zeros > ones:
            gamma_rate[i] = "0"
            epsilon_rate[i] = "1"
        else:
            gamma_rate[i] = "1"
            epsilon_rate[i] = "0"

    # Final answer
    ans = int("".join(gamma_rate), 2) * int("".join(epsilon_rate), 2)
    print(ans)

def part02(input):
    numbers = len(input[0])

    input = [int(i, 2) for i in input]

    o2_rem = input.copy()
    pos = numbers - 1
    while pos >= 0 and len(o2_rem) > 1:
        ones = sum([((x & (1 << pos)) >> pos) for x in o2_rem])
        zeros = len(o2_rem) - ones

        if zeros > ones:
            o2_rem = list(filter(lambda x: not (x & (1 << pos)), o2_rem))
        else:
            o2_rem = list(filter(lambda x: (x & (1 << pos)), o2_rem))

        pos -= 1

    co2_rem = input.copy()
    pos = numbers - 1
    while pos >= 0 and len(co2_rem) > 1:
        # Find the most common bit
        ones = sum([((x & (1 << pos)) >> pos) for x in co2_rem])
        zeros = len(co2_rem) - ones

        if zeros > ones:
            co2_rem = list(filter(lambda x: (x & (1 << pos)), co2_rem))
        else:
            co2_rem = list(filter(lambda x: not (x & (1 << pos)), co2_rem))

        pos -= 1


    ans = o2_rem[0] * co2_rem[0]
    print(ans)

if __name__ == "__main__":
   with open("day03.txt") as fin:
    input = fin.read().strip().split("\n")
part01(input) #Answer 741950 
part02(input) #Answer 903810
