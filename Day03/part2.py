from string import ascii_lowercase, ascii_uppercase

key = ascii_lowercase + ascii_uppercase
print(key)

with open("puzzleFile.txt") as fin:
    data = fin.read().strip().split("\n")

ans = 0

lines = data
for i in range(0, len(lines), 3):
    a = lines[i:(i+3)] #grabbing items in lines I and then grabbing I+3 in each interations

    for i, c in enumerate(key):
        if all([c in li for li in a]): #all function to check that all items are true before moving on 
            ans += key.index(c) + 1

print(ans)