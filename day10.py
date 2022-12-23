with open("day10.txt") as fin:
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


print("part 1 - ", ans)


# row = 0
# col = 0

# X = [1] * 241

# for line in data:
#     parts = line.split(" ")

#     if parts[0] == "noop":
#         op += 1
#         X[op] = cur_X

#     elif parts[0] == "addx":
#         V = int(parts[1])

#         X[op + 1] = cur_X
#         cur_X += V

#         op += 2
#         X[op] = cur_X


# # Ranges from [1, 39]
# ans = [[None] * 40 for _ in range(6)]

# for row in range(6):
#     for col in range(40):
#         counter = row * 40 + col + 1
#         if abs(X[counter - 1] - (col)) <= 1:
#             ans[row][col] = "##"
#         else:
#             ans[row][col] = "  "


# for row in ans:
#     print("".join(row))