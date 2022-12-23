with open("puzzleFile.txt") as fin:
    data = fin.read().strip().split()

ans = 0
for da in data:
    elves = da.split(",") #need to split at this level 
    ranges = [list(map(int, elf.split("-"))) for elf in elves] #now we split athe actual ranges 

    a, b = ranges[0] #adding the first of the split to a and b
    c, d = ranges[1] #adding second number of the split to to c and d 

    if a <= c and b >= d or a >= c and b <= d: #Compare time!
        ans += 1


print(ans)