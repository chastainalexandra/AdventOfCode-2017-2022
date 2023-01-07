# --- Day 10: Syntax Scoring ---
# If a chunk opens with (, it must close with ).
# If a chunk opens with [, it must close with ].
# If a chunk opens with {, it must close with }.
# If a chunk opens with <, it must close with >.

pairs = ["()", "[]", "<>", "{}"] #legal pairs 
points = {
    ")": 3, # ): 3 points.
    "]": 57, # ]: 57 points.
    "}": 1197, # }: 1197 points.
    ">": 25137 # >: 25137 points.
}

def parse(input_data):
    line = []
    for chunk in input_data:
        legal = False
        for p in pairs:
            if chunk == p[0]:
                line.append(chunk)
                legal = True
            elif chunk == p[1]:
                if line[-1] == p[0]:
                    line.pop()
                    legal = True

        if not legal:  # Find the first illegal character in each corrupted line of the navigation subsystem. 
            return points[chunk]  # What is the total syntax error score for those errors?

    return points



def part01(input_data):
    ans = 0
    totalAnswer = 0
    for chunk in input_data:
        ans = parse(chunk) #error here! conversion error! 
        totalAnswer += ans
    print(totalAnswer)

#def part02(data):

if __name__ == "__main__":
   with open("day10.txt") as fin:
    input = fin.read().strip()
    input_data = input.split("\n")
    new_data = [int(i) for i in input_data]
part01(new_data) #Answer  
#part02(data) #Answer 