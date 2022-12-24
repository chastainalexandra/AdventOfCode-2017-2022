from collections import defaultdict
with open('day23.txt','r') as f:
    text=f.read().splitlines()

N=(0,-1)
S=(0,1)
E=(1,0)
W=(-1,0)
NE=(1,-1)
NW=(-1,-1)
SE=(1,1)
SW=(-1,1)
all_dir=[N,S,E,W,NE,NW,SE,SW]

grid=[[x for x in r] for r in text]

def find_dest(pos, grid, cycle):
    empty = {(x,y) for x,y in all_dir if (pos[1]+y<0 or pos[1]+y>=len(grid) or pos[0]+x<0 or pos[0]+x>=len(grid[0])) or grid[pos[1]+y][pos[0]+x]!='#'}
    if len(empty)==8:
        return
    for o in range(cycle, cycle + 4):
        if o%4==0:  
            if NW in empty and N in empty and NE in empty:
                return N
        if o%4==1:
            if SW in empty and S in empty and SE in empty:
                return S
        if o%4==2:
            if NW in empty and W in empty and SW in empty:
                return W
        if o%4==3:
            if NE in empty and E in empty and SE in empty:
                return E

cycles=5000
curr_empty=0
for c in range(cycles):
    prev_empty=curr_empty
    dest = {}
    dest_counts = defaultdict(int)
    moved=False
    for j, row in enumerate(grid):
        for i, e in enumerate(row):
            if e=='#':
                if goto :=find_dest((i,j), grid,c):
                    dest[(i,j)]=i+goto[0],j+goto[1],
                    dest_counts[i+goto[0],j+goto[1]]+=1
                    moved=True
                else:
                    dest[(i,j)]=(i,j)
                    dest_counts[(i,j)]=1
    xmin,xmax = min(x[0] for x in dest_counts), max(x[0] for x in dest_counts)
    ymin, ymax = min(x[1] for x in dest_counts), max(x[1] for x in dest_counts)
    new_grid = [['.' for _ in range(xmin, xmax+1)] for _ in range(ymin, ymax+1)]
    grid_map = {(q,v):(grid_col_idx,grid_row_idx) for grid_row_idx, v in enumerate(range(ymin, ymax+1)) for grid_col_idx, q in enumerate(range(xmin, xmax+1))}
    for pos, goto in dest.items():
        if dest_counts[goto]==1:
            move = grid_map[goto]
        else:
            move = grid_map[pos]
        new_grid[move[1]][move[0]]='#'
    grid=new_grid
    curr_empty = sum(sum(1 for x in row if x=='.') for row in grid)
    if c==9:
        print(f'p1: {curr_empty}')
    if not moved:
        print(f'p2: {c+1}')
        break


# answers 
# Part 1 - 4146
# Part 2 - 