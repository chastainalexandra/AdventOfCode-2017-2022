# As the expedition finally reaches the extraction point, several large hot air balloons drift down to meet you. 
# Crews quickly start unloading the equipment the balloons brought: many hot air balloon kits, some fuel tanks, and a fuel heating machine.
# The fuel heating machine is a new addition to the process. 
# When this mountain was a volcano, the ambient temperature was more reasonable; now, it's so cold that the fuel won't work at all without being warmed up first.
# The Elves, seemingly in an attempt to make the new machine feel welcome, have already attached a pair of googly eyes and started calling it "Bob".
# To heat the fuel, Bob needs to know the total amount of fuel that will be processed ahead of time so it can correctly calibrate heat output and flow rate. 
# This amount is simply the sum of the fuel requirements of all of the hot air balloons, and those fuel requirements are even listed clearly on the side of each hot air balloon's burner.
# You assume the Elves will have no trouble adding up some numbers and are about to go back to figuring out which balloon is yours when you get a tap on the shoulder. 
# Apparently, the fuel requirements use numbers written in a format the Elves don't recognize; predictably, they'd like your help deciphering them.


# As you go to input this number on Bob's console, 
# you discover that some buttons you expected are missing. 
# Instead, you are met with buttons labeled =, -, 0, 1, and 2
buttonsThatHaveLabels = {
        "0": 0,
        "1": 1,
        "2": 2,
        "-": -1,
        "=": -2
    }

digitsToSNAF = {digit: s for s, digit in buttonsThatHaveLabels.items()}

def parse(s):
    SNAFUNumber = 0
    d = len(s)
    p = 1 
    s = s[::-1]
    for i in range(d):
        SNAFUNumber += p * buttonsThatHaveLabels[s[i]]
        p *= 5 
    return SNAFUNumber   

def convert(num):
    start = ""
    while num > 0:
        digit = ((num +2) % 5) -2
        start += digitsToSNAF[digit]
        num -= digit
        num //=5 # SNAFU works the same way, except it uses powers of five instead of ten
    return start[::-1]

if __name__ == "__main__":
   with open("day25.txt") as fin:   
    data = fin.read().strip().split()

   SNAFUNumber = 0
   for da in data: 
     SNAFUNumber += parse(da)
   print("part 1 - ", convert(SNAFUNumber))

# answers 
# Part 1 - 2--2-0=--0--100-=210