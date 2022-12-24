import re
with open('day22.txt','r') as f:
    text=f.read().split('\n\n')

grid = text[0].split('\n')
instr = text[1]

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

moves = re.findall('[0-9]+', instr)
turns = re.findall('[LR]', instr)

def get_sq(x,y, factor=50):
    if factor <= x < 2*factor:
        if 0<=y<factor:
            return 'A'
        if factor<=y<2*factor:
            return 'C'
        if 2*factor<=y<3*factor:
            return 'E'
    elif 2*factor<=x<3*factor:
        if 0<=y<factor:
            return 'B'
    elif 0<=x<factor:
        if 2*factor<=y<3*factor:
            return 'D'
        if 3*factor<=y<4*factor:
            return 'F'
    else:
        # out of bounds!
        return False

def next_fd(curr_face, dir):
    if curr_face=='A' and dir==UP:
        return 'F', RIGHT
    if curr_face=='A' and dir==LEFT:
        return 'D', RIGHT
    if curr_face=='B' and dir==UP:
        return 'F', UP
    if curr_face=='B' and dir==RIGHT:
        return 'E', LEFT
    if curr_face=='B' and dir==DOWN:
        return 'C',LEFT
    if curr_face=='C' and dir==RIGHT:
        return 'B', UP
    if curr_face == 'C' and dir==LEFT:
        return 'D', DOWN
    if curr_face == 'D' and dir == UP:
        return 'C', RIGHT
    if curr_face == 'D' and dir == LEFT:
        return 'A', RIGHT
    if curr_face == 'E' and dir == RIGHT:
        return 'B', LEFT
    if curr_face == 'E' and dir == DOWN:
        return 'F', LEFT
    if curr_face == 'F' and dir == LEFT:
        return 'A', DOWN
    if curr_face == 'F' and dir == RIGHT:
        return 'E', UP
    if curr_face == 'F' and dir == DOWN:
        return 'B', DOWN
    raise Exception('Should not be here')

def translate(x,y,curr_face,new_face):
    if curr_face=='A':
        if new_face=='F':
            newy=100+x
            newx=0
        if new_face=='D':
            newx=0
            newy=149-y
    if curr_face=='B':
        if new_face=='F':
            newy=199
            newx=x-100
        if new_face=='E':
            newx=99
            newy=149-y
        if new_face=='C':
            newx=99
            newy=x-50
    if curr_face=='C':
        if new_face=='B':
            newy=49
            newx=y+50
        if new_face=='D':
            newx=y-50
            newy=100
    if curr_face=='D':
        if new_face=='A':
            newx=50
            newy=149-y
        if new_face=='C':
            newy=x+50
            newx=50
    if curr_face=='E':
        if new_face=='B':
            newy=149-y
            newx=149
        if new_face=='F':
            newy=x+100
            newx=49
    if curr_face=='F':
        if new_face=='A':
            newy=0
            newx=y-100
        if new_face=='B':
            newy=0
            newx=x+100
        if new_face=='E':
            newy=149
            newx=y-100
    return newx, newy
        
def move(x,y, dir):
    newx, newy = (x+dir[0])%150,(y+dir[1])%200
    new_dir=dir
    if not get_sq(newx, newy):
        # we're out of bounds
        curr_face = get_sq(x,y)
        new_face, new_dir = next_fd(curr_face, dir)
        newx, newy = translate(x,y,curr_face, new_face)
    return newx%150, newy%200, new_dir

for i,m in enumerate(moves):
    a=1
    for tiles in range(int(m)):
        newx,newy,new_dir=move(x,y,dir)
        # handle out of bounds by region
        if grid[newy][newx]=='.':
            x,y = newx, newy
            dir=new_dir
        elif grid[newy][newx]==' ':
            raise Exception()
        else:
            # hit a wall
            break
    if i<len(turns):
        dir = newdir[dir][turns[i]]

dirscore = {
    RIGHT:0,
    DOWN:1,
    LEFT:2,
    UP:3
}

score = (y+1)*1000+(x+1)*4+dirscore[dir]
print(score)

# answers 
# Part 1 - 122082
# Part 2 - 134076