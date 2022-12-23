from string import ascii_lowercase, ascii_uppercase

key = ascii_lowercase + ascii_uppercase
print(key)

with open("puzzleFile.txt") as fin:
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

print(ans)