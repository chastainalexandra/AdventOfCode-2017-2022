# part 1 = 111210
# part 2 =  15447387620

from collections import defaultdict

dataFile = open("puzzleFile.txt").read().split("\n\n")
args = [[unstripped.strip() for unstripped in data.splitlines()] for data in dataFile]
found_monkeys = {}

#Operation shows how your worry level changes as that monkey inspects an item.
# (An operation like new = old * 5 means that your worry level after the monkey inspected the item is five times whatever your worry level was before inspection.)
def multiply(x, y):
    return x*y

def add(x, y):
    return x+y

# Test shows how the monkey uses your worry level to decide where to throw an item next.
# If true shows what happens with an item if the Test was true.
# If false shows what happens with an item if the Test was false.
def testWorryLevel(monkey_true, monkey_false, test, value):
    if value % test == 0:
        return monkey_true
    return monkey_false


def prep_monkeys():
    div_factor = 1
    for m in args:
        found_monkeys[m[0]] = {}
        starting_items = m[1].split(": ")[1].split(', ')
        starting_items = [int(item) for i, item in enumerate(starting_items)]
        operation, value = m[2].split("old ")[1].split(" ")
        if operation == '*':
            operation = (multiply, value)
        else:
            operation = (add, value)
        divisible_by = int(m[3].split("by ")[-1])
        true = int(m[4].split("monkey ")[-1])
        false = int(m[5].split("monkey ")[-1])
        div_factor *= divisible_by

        found_monkeys[m[0]]['div_by'] = divisible_by
        found_monkeys[m[0]]['true'] = true
        found_monkeys[m[0]]['false'] = false
        found_monkeys[m[0]]['starting_items'] = starting_items
        found_monkeys[m[0]]['operation'] = operation
        found_monkeys[m[0]]['name'] = m[0]
    return div_factor, found_monkeys

def determine_monkey_activity():
    inspections = defaultdict(int)
    rounds = 20 # total number of times each monkey inspects items
    div_factor, parsed_monkeys = prep_monkeys()
    for _ in range(rounds):
        for _, monkey in parsed_monkeys.items():
            items = list(monkey['starting_items'])
            for item in items:
                inspections[monkey['name']] += 1
                op, val = monkey['operation']
                if val == "old":
                    item = op(item, item)
                else:
                    item = op(item, int(val))       
                item //= 3                  #After each monkey inspects an item but before it tests your worry level, your relief that the monkey's inspection didn't damage the item causes your worry level to be divided by three and rounded down to the nearest integer.
                item = item % div_factor
                recieve_monkey = testWorryLevel(monkey['true'], monkey['false'], monkey['div_by'], item)
                parsed_monkeys[f"Monkey {recieve_monkey}:"]['starting_items'].append(item)
                monkey['starting_items'].pop(0)

    sorted_ = sorted(inspections.values())
    return multiply(sorted_[-2], sorted_[-1])

def determine_monkey_activity_1():
    inspections = defaultdict(int)
    rounds = 10000 # total number of times each monkey inspects items
    div_factor, parsed_monkeys = prep_monkeys()
    for _ in range(rounds):
        for _, monkey in parsed_monkeys.items():
            items = list(monkey['starting_items'])
            for item in items:
                inspections[monkey['name']] += 1
                op, val = monkey['operation']
                if val == "old":
                    item = op(item, item)
                else:
                    item = op(item, int(val))
                item = item % div_factor
                recieve_monkey = testWorryLevel(monkey['true'], monkey['false'], monkey['div_by'], item)
                parsed_monkeys[f"Monkey {recieve_monkey}:"]['starting_items'].append(item)
                monkey['starting_items'].pop(0)

    sorted_ = sorted(inspections.values())
    return multiply(sorted_[-2], sorted_[-1])


twentyRoundsOfMB = determine_monkey_activity()
print("Part 1 ", twentyRoundsOfMB)

thousandRoundsOfMB = determine_monkey_activity_1()
print("Part 2", thousandRoundsOfMB)

#could merge the determine monkey activity methods and pass the rounds 