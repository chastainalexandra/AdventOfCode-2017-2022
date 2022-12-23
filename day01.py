with open("day01.txt") as fin:
    raw_data = fin.read().strip()
    parts = raw_data.split("\n\n")


loads = []
for part in parts:
    foods = map(int, part.split())
    loads.append(sum(foods))


print(max(loads))
print(sum(sorted(loads)[-3:]))