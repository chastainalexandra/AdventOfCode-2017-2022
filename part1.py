from string import ascii_lowercase, ascii_uppercase

key = ascii_lowercase + ascii_uppercase
print(key)

with open("day03.txt") as fin:
    data = fin.read().strip().split("\n")

ans = 0

lines = data
for line in lines:
    n = len(line)
    a = line[:(n//2)]  # First compartment slicing the lines and doing interger divison 
    b = line[(n//2):]  # Second compartment

    for i, c in enumerate(key):
        if c in a and c in b:
            ans += key.index(c) + 1

print("part 1 - ", ans)


ans = 0

lines = data
for i in range(0, len(lines), 3):
    a = lines[i:(i+3)] #grabbing items in lines I and then grabbing I+3 in each interations

    for i, c in enumerate(key):
        if all([c in li for li in a]): #all function to check that all items are true before moving on 
            ans += key.index(c) + 1

print("part 2 - ", ans)