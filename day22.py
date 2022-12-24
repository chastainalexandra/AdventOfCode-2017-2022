# The monkeys take you on a surprisingly easy trail through the jungle. 
# They're even going in roughly the right direction according to your handheld device's Grove Positioning System.
# As you walk, the monkeys explain that the grove is protected by a force field. 
# To pass through the force field, you have to enter a password; doing so involves tracing a specific path on a strangely-shaped board.
# At least, you're pretty sure that's what you have to do; the elephants aren't exactly fluent in monkey.
# The monkeys give you notes that they took when they last saw the password entered (your puzzle input).

import re

def part01(board_lines,path_line):
    tiles = set()
    walls = set()
    rows = {}
    cols = {}

    r = 1
    for row in board_lines.split("\n"): # have to split yet again 
        c = 1
        rowStart = 10000 # we want 1000 time the sum 
        rowEnd = 0
        for column in row:
            if c not in cols:
                cols[c] = (10000, 0)
            if column == ".":
                tiles.add(complex(c, r))
                rowStart = min(rowStart, c)
                rowEnd = max(rowEnd, c)
                cols[c] = ((min(cols[c][0], r), max(cols[c][1], r)))
            elif column == "#":
                walls.add(complex(c, r))
                rowStart = min(rowStart, c)
                rowEnd = max(rowEnd, c)
                cols[c] = ((min(cols[c][0], r), max(cols[c][1], r)))
            c += 1
        rows[r] = (rowStart, rowEnd)
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

    for i in instructions:
        if isinstance(i, int):
            for i in range(i):
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
        elif i == 'R':
            facing = (facing + 1) % 4
        elif i == 'L':
            facing = (facing - 1) % 4

     # The final password is the sum of 1000 times the row, 4 times the column, and the facing.
    answer = int(pos.imag * 1000 + pos.real * 4 + facing)
    print("part 1 - ", answer)

# As you reach the force field, you think you hear some Elves in the distance. Perhaps they've already arrived?
# You approach the strange input device, but it isn't quite what the monkeys drew in their notes. Instead, you are met with a large cube; each of its six faces is a square of 50x50 tiles.
# To be fair, the monkeys' map does have six 50x50 regions on it. If you were to carefully fold the map, you should be able to shape it into a cube!

def part02(data):
    grid = data[0].split('\n') #splitting yet again :( )
    instructions = data[1]

    #setting up our directions 
    LEFT=(-1,0)
    RIGHT=(1,0)
    UP=(0,-1)
    DOWN=(0,1)

    dir=RIGHT
    x,y=0,0
    for i, v in enumerate(grid[0]):
        if v=='.':
            x=i
            break

    newdir = {
        RIGHT:{'R':DOWN,'L':UP},
        LEFT:{'R':UP,'L':DOWN},
        DOWN:{'R':LEFT,'L':RIGHT},
        UP:{'R':RIGHT,'L':LEFT},
    }

    moves = re.findall('[0-9]+', instructions)
    turns = re.findall('[LR]', instructions)

    # To be fair, the monkeys' map does have six 50x50 regions on it. 
    # If you were to carefully fold the map, you should be able to shape it into a cube!
    # In the example above, the six (smaller, 4x4) faces of the cube are
    def getSquare(x,y, size=50):
        if size <= x < 2*size:
            if 0<=y<size:
                return 'A'
            if size<=y<2*size:
                return 'C'
            if 2*size<=y<3*size:
                return 'E'
        elif 2*size<=x<3*size:
            if 0<=y<size:
                return 'B'
        elif 0<=x<size:
            if 2*size<=y<3*size:
                return 'D'
            if 3*size<=y<4*size:
                return 'F'
        else:
            return False

    # time to list out all the combinations 
    def nextSide(current_Side, dir):
        if current_Side=='A' and dir==UP:
            return 'F', RIGHT
        if current_Side=='A' and dir==LEFT:
            return 'D', RIGHT
        if current_Side=='B' and dir==UP:
            return 'F', UP
        if current_Side=='B' and dir==RIGHT:
            return 'E', LEFT
        if current_Side=='B' and dir==DOWN:
            return 'C',LEFT
        if current_Side=='C' and dir==RIGHT:
            return 'B', UP
        if current_Side == 'C' and dir==LEFT:
            return 'D', DOWN
        if current_Side == 'D' and dir == UP:
            return 'C', RIGHT
        if current_Side == 'D' and dir == LEFT:
            return 'A', RIGHT
        if current_Side == 'E' and dir == RIGHT:
            return 'B', LEFT
        if current_Side == 'E' and dir == DOWN:
            return 'F', LEFT
        if current_Side == 'F' and dir == LEFT:
            return 'A', DOWN
        if current_Side == 'F' and dir == RIGHT:
            return 'E', UP
        if current_Side == 'F' and dir == DOWN:
            return 'B', DOWN
        raise Exception('Should not be here')

    def combinations(x,y,current_Side,new_Side):
        if current_Side=='A':
            if new_Side=='F':
                newy=100+x
                newx=0
            if new_Side=='D':
                newx=0
                newy=149-y
        if current_Side=='F':
            if new_Side=='A':
                newy=0
                newx=y-100
            if new_Side=='B':
                newy=0
                newx=x+100
            if new_Side=='E':
                newy=149
                newx=y-100
        if current_Side=='B':
            if new_Side=='F':
                newy=199
                newx=x-100
            if new_Side=='E':
                newx=99
                newy=149-y
            if new_Side=='C':
                newx=99
                newy=x-50
        if current_Side=='E':
            if new_Side=='B':
                newy=149-y
                newx=149
            if new_Side=='F':
                newy=x+100
                newx=49
        if current_Side=='C':
            if new_Side=='B':
                newy=49
                newx=y+50
            if new_Side=='D':
                newx=y-50
                newy=100
        if current_Side=='D':
            if new_Side=='A':
                newx=50
                newy=149-y
            if new_Side=='C':
                newy=x+50
                newx=50
       
        return newx, newy
            
    def move(x,y, dir):
        newx, newy = (x+dir[0])%150,(y+dir[1])%200
        new_dir=dir
        if not getSquare(newx, newy):
            curr_face = getSquare(x,y)
            new_face, new_dir = nextSide(curr_face, dir)
            newx, newy = combinations(x,y,curr_face, new_face)
        return newx%150, newy%200, new_dir

    for i,m in enumerate(moves):
        a=1
        for tiles in range(int(m)):
            newx,newy,new_dir=move(x,y,dir)
            if grid[newy][newx]=='.':
                x,y = newx, newy
                dir=new_dir
            elif grid[newy][newx]==' ':
                raise Exception()
            else:
                break
        if i<len(turns):
            dir = newdir[dir][turns[i]]

    dirscore = {
        RIGHT:0,
        DOWN:1,
        LEFT:2,
        UP:3
    }

    # The final password is still calculated from your final position and facing from the perspective of the map. 
    score = (y+1)*1000+(x+1)*4+dirscore[dir]
    print("part 2 - ", score)

if __name__ == "__main__":
   with open("day22.txt") as fin:
    inp = fin.read()
    board_lines, path_line = inp.split("\n\n")
    path_line = path_line.strip()

   part01(board_lines,path_line)
   with open('day22.txt','r') as f: #come back and clean this up, this is gross
    data=f.read().split('\n\n')
   part02(data)


# answers 
# Part 1 - 122082
# Part 2 - 134076