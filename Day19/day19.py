# part 1 = 1659
# part 2 = 6804
import math
from collections import defaultdict
from functools import cache
import re

with open('puzzleFile.txt','r') as f:
    puzzleFile=f.read().splitlines()

# Determine the quality level of each blueprint by multiplying that blueprint's
# ID number with the largest number of geodes that can be opened in 24 minutes using that blueprint. 
# In this example, the first blueprint has ID 1 and can open 9 geodes, so its quality level is 9. 
# The second blueprint has ID 2 and can open 12 geodes, so its quality level is 24. 
# Finally, if you add up the quality levels of all of the blueprints in the list, you get 33.

#Determine the quality level of each blueprint using the largest number of geodes it could produce in 24 minutes. 
# What do you get if you add up the quality level of all of the blueprints in your list?

material = {
    'ore':0,
    'clay':1,
    'obsidian':2,
    'geode': 3
}

bp = {}
for blueprints in puzzleFile:
    idx, costs = blueprints.split(':')
    idx = int(idx.split()[-1])
    bp[idx]={}
    for c in costs.split('.'):
        if c:
            g = re.match(r'Each (.*) robot costs (.*)', c.strip()).groups()
            pay = {material[v.split()[1]]:int(v.split()[0]) for v in g[1].split(' and ')}
            pay = tuple([pay.get(i,0) for i in range(4)])

            bp[idx][material[g[0]]]=pay


def has_leftover(cost, resources, n=1):
    return all(resources[x]>=cost[x]*n for x in range(4))

def refine_states(states):
    states_by_base = defaultdict(set)
    for state in states:
        states_by_base[state[0]].add(state[1])
    gmax = max(x[0][-1]+x[1][-1] for x in states)

    ret = set()
    for base, res_v in states_by_base.items():
        for res in res_v:
            if base[-1]+res[-1]>=gmax-2:
                if not any(all(i >= j for i, j in zip(s, res)) for s in res_v if s!=res):
                    ret.add((base,res))
    return ret
# The elephants are starting to look hungry, so you shouldn't take too long;
# you need to figure out which blueprint would maximize the number of 
# opened geodes after 24 minutes by figuring out which robots to build and when to build them.
def get_q(costs,time=24): 
    states=defaultdict(set)
    states[0]={((1,0,0,0),(0,0,0,0))}

    @cache
    def buildable(res):
        bot_pairs = set()
        explore = [((0, 0, 0, 0), res)]
        while explore:
            start = explore.pop()
            bot_pairs.add(start)
            bots, resources = start
            for bot, expense in costs.items():
                leftover = [resources[n] - expense[n] for n in range(4)]
                if all(x >= 0 for x in leftover):
                    new_bots = (tuple([x if i != bot else x + 1 for i, x in enumerate(bots)]), tuple(leftover))
                    if new_bots not in bot_pairs:
                        bot_pairs.add(new_bots)
        return bot_pairs

    for i in range(time-1):
        states[i]=refine_states(states[i])
        for bots, res in states[i]:
            new_bots = buildable(res)
            states[i+1].update([(tuple([bots[n] + new_bot[n] for n in range(4)]), tuple([bots[n]+leftover[n] for n in range(4)])) for new_bot, leftover in new_bots])
    result=max(x[0][-1]+x[1][-1] for x in states[time-1])
    return result

q = {blueprint: get_q(v) for blueprint,v in bp.items()}

print(sum(a*b for a,b in q.items()))

# for part 2 we are going to pass in 32 instead of 24 
# Instead, for each of the first three blueprints, determine the largest number of geodes you could open; 
# then, multiply these three values together.
part2 = [get_q(v,32) for blueprint,v in bp.items() if blueprint<=3]
print(part2,math.prod(part2))
