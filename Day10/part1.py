with open("puzzleFile.txt") as fin:
    data = fin.read().strip().split("\n")

X = 1
op = 0

ans = 0
interesting = [20, 60, 100, 140, 180, 220]

for line in data:
    parts = line.split(" ")

    if parts[0] == "noop":
        op += 1

        if op in interesting:
            ans += op * X

    elif parts[0] == "addx":
        V = int(parts[1])
        X += V

        op += 1

        if op in interesting:
            ans += op * (X - V)

        op += 1

        if op in interesting:
            ans += op * (X - V)


print(ans)