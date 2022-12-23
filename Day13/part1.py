with open("puzzleFile.txt") as fin:
    data = fin.read().strip().split("\n\n")

def compare(a, b):
    if isinstance(a, list) and isinstance(b, int):
        b = [b]

    if isinstance(a, int) and isinstance(b, list):
        a = [a]

    if isinstance(a, int) and isinstance(b, int):
        if a < b:
            return 1
        if a == b:
            return 0
        return -1

    if isinstance(a, list) and isinstance(b, list):
        i = 0
        while i < len(a) and i < len(b):
            x = compare(a[i], b[i])
            if x == 1:
                return 1
            if x == -1:
                return -1

            i += 1

        if i == len(a):
            if len(a) == len(b):
                return 0
            return 1  # a ended first

        return -1


ans = 0

for i, block in enumerate(data):
    a, b = map(eval, block.split("\n"))
    if compare(a, b) == 1:
        ans += i + 1

print(ans)