with open("puzzleFile.txt") as fin:
    data = fin.read().strip().split("\n")

# offset of what in what to play 
play_map = {"A": 0, "B": 1, "C": 2,
            "X": -1, "Y": 0, "Z": 1} # decrease this by 1 from previous part

score_key = [1, 2, 3]

score = 0
for line in data:
    opp, me = [play_map[i] for i in line.split()]

    score += (me + 1) * 3
    score += score_key[(opp + me) % 3]


print(score)