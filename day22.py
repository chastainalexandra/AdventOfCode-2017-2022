
f = open("day22.txt")
inp = f.read()

board_lines, path_line = inp.split("\n\n")

path_line = path_line.strip()

tiles = set()
walls = set()
rows = {}
cols = {}

r = 1
for row in board_lines.split("\n"):
    c = 1
    rstart = 10000
    rend = 0
    for col in row:
        if c not in cols:
            cols[c] = (10000, 0)
        if col == ".":
            tiles.add(complex(c, r))
            rstart = min(rstart, c)
            rend = max(rend, c)
            cols[c] = ((min(cols[c][0], r), max(cols[c][1], r)))
        elif col == "#":
            walls.add(complex(c, r))
            rstart = min(rstart, c)
            rend = max(rend, c)
            cols[c] = ((min(cols[c][0], r), max(cols[c][1], r)))
        c += 1
    rows[r] = (rstart, rend)
    r+= 1

num = ""
instructions = []
p = 0
while p < len(path_line):
    if path_line[p].isnumeric():
        num += path_line[p]
    else:
        instructions.append(int(num))
        num = ""
        instructions.append(path_line[p])
    p += 1
instructions.append(int(num))

facing = 0
move_ops = [1 + 0j, 1j, -1 + 0j, -1j]
pos = complex(rows[1][0], 1)

for instr in instructions:
    if isinstance(instr, int):
        for i in range(instr):
            next_pos = pos + move_ops[facing]
            if next_pos not in walls and next_pos not in tiles:
                if facing == 0:
                    next_pos = complex(rows[pos.imag][0], pos.imag)
                elif facing == 1:
                    next_pos = complex(pos.real, cols[pos.real][0])
                elif facing == 2:
                    next_pos = complex(rows[pos.imag][1], pos.imag)
                elif facing == 3:
                    next_pos = complex(pos.real, cols[pos.real][1])
            if next_pos in tiles:
                pos = next_pos
    elif instr == 'R':
        facing = (facing + 1) % 4
    elif instr == 'L':
        facing = (facing - 1) % 4

answer = int(pos.imag * 1000 + pos.real * 4 + facing)
print(answer)

# answers 
# Part 1 - 122082
# Part 2 - 134076