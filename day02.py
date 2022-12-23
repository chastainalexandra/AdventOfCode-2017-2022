with open("day02.txt") as fin:
    data = fin.read().strip().split("\n")

play_map_part_1 = {"A": 0, "B": 1, "C": 2,
            "X": 0, "Y": 1, "Z": 2}

score_key = [1, 2, 3]

score = 0
for line in data:
    opp, me = [play_map_part_1[i] for i in line.split()]

    # Win
    if (me - opp) % 3 == 1:
        score += 6
    elif (me == opp):
        score += 3

    score += score_key[me]


print("part 1 - ", score)

# offset of what in what to play 
play_map = {"A": 0, "B": 1, "C": 2,
            "X": -1, "Y": 0, "Z": 1} # decrease this by 1 from previous part

score_key = [1, 2, 3]

score = 0
for line in data:
    opp, me = [play_map[i] for i in line.split()]

    score += (me + 1) * 3
    score += score_key[(opp + me) % 3]


print("part 2 - ", score)