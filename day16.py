import re
from dataclasses import dataclass
from collections import defaultdict, deque
from typing import NamedTuple

PARSER = re.compile(
    r"Valve (\w+) has flow rate=(\d+); tunnel(?:s)? lead(?:s)? to valve(?:s)? (.*)"
)

MAX_MINUTES = 30

@dataclass()
class Valve:
    name: str
    flow_rate: int


State = NamedTuple(
    "State",
    [
        ("loc", str),
        ("open_valves", frozenset),
        ("pressure_released", int),
        ("minutes", int),
        ("last_loc", str),
    ],
)
StateNoLastLoc = NamedTuple(
    "StateNoLastLoc",
    [
        ("loc", str),
        ("open_valves", frozenset),
        ("pressure_released", int),
        ("minutes", int),
    ],
)
StateNoPres = NamedTuple(
    "StateNoPres",
    [
        ("loc", str),
        ("open_valves", frozenset),
        ("minutes", int),
    ],
)


def get_state_no_last_loc(s):
    return StateNoLastLoc(s.loc, s.open_valves, s.pressure_released, s.minutes)


def reverse_me_and_ele(snll):
    locs = snll.loc.split(",")
    locs = f"{locs[1]},{locs[0]}"
    return StateNoLastLoc(locs, snll.open_valves, snll.pressure_released, snll.minutes)


def get_state_no_press(s):
    # return StateNoPres(s.loc, s.open_valves, s.minutes)
    # return StateNoPres(s.loc, frozenset(), s.minutes)
    return StateNoPres("XYZ", s.open_valves, s.minutes)


def parse(filename):
    valves = {}
    vmap = defaultdict(list)

    with open(filename) as file:
        for line in file.readlines():
            line = line.strip()

            (valve, flow, tunnels) = re.search(PARSER, line).groups()
            flow = int(flow)
            valves[valve] = Valve(valve, flow)

            tunnels = tunnels.split(", ")
            for dest_tunnel in tunnels:
                vmap[valve].append(dest_tunnel)

    return valves, vmap


def init_state():
    return State("AA", frozenset(), 0, 0, "")


def init_state_ele():
    return State("AA,AA", frozenset(), 0, 0, ",")


def get_neighbors(s, valves, vmap):
    if s.minutes >= MAX_MINUTES:
        return []

    r = []
    last_loc = s.loc

    pressure_delta = 0
    for ov in s.open_valves:
        pressure_delta += valves[ov].flow_rate
    new_pressure = s.pressure_released + pressure_delta

    ## Moving to another valve
    for dest in vmap[s.loc]:
        if dest == s.last_loc:
            continue
        r.append(State(dest, s.open_valves, new_pressure, s.minutes + 1, last_loc))

    ## Opening a valve
    if s.loc not in s.open_valves and valves[s.loc].flow_rate > 0:
        r.append(
            State(s.loc, s.open_valves | {s.loc}, new_pressure, s.minutes + 1, last_loc)
        )

    return r


def get_neighbors_ele(s, valves, vmap):
    if s.minutes >= MAX_MINUTES:
        return []

    r = []

    last_loc_me, last_loc_ele = s.last_loc.split(",")
    loc_me, loc_ele = s.loc.split(",")

    pressure_delta = 0
    for ov in s.open_valves:
        pressure_delta += valves[ov].flow_rate
    new_pressure = s.pressure_released + pressure_delta

    ## (ME+Ele) Moving to another valve
    my_acts = []
    ele_acts = []
    for dest_me in vmap[loc_me]:
        if dest_me == last_loc_me:
            continue
        my_acts.append(["move", dest_me])

    for dest_ele in vmap[loc_ele]:
        if dest_ele == last_loc_ele:
            continue
        ele_acts.append(["move", dest_ele])

    ## Opening a valve
    if loc_me not in s.open_valves and valves[loc_me].flow_rate > 0:
        my_acts.append(["open", loc_me])
    if loc_ele not in s.open_valves and valves[loc_ele].flow_rate > 0:
        ele_acts.append(["open", loc_ele])

    for my_act in my_acts:
        for ele_act in ele_acts:

            new_open_valves = s.open_valves
            if my_act[0] == "open":
                new_open_valves = new_open_valves | {my_act[1]}
            if ele_act[0] == "open":
                new_open_valves = new_open_valves | {ele_act[1]}

            new_loc_me = loc_me
            new_loc_ele = loc_ele
            if my_act[0] == "move":
                new_loc_me = my_act[1]
            if ele_act[0] == "move":
                new_loc_ele = ele_act[1]

            if new_loc_me == new_loc_ele:
                continue
            r.append(
                State(
                    f"{new_loc_me},{new_loc_ele}",
                    new_open_valves,
                    new_pressure,
                    s.minutes + 1,
                    s.loc,
                )
            )

    return r







# The sensors have led you to the origin of the distress signal: yet another handheld device, just like the one the Elves gave you. However, you don't see any Elves around; instead, the device is surrounded by elephants! They must have gotten lost in these tunnels, 
# and one of the elephants apparently figured out how to turn on the distress signal.
# The ground rumbles again, much stronger this time. What kind of cave is this, exactly? 
# You scan the cave with your handheld device; it reports mostly igneous rock, some ash,
#  pockets of pressurized gas, magma... this isn't just a cave, it's a volcano!
# You need to get the elephants out of here, quickly. 
# Your device estimates that you have 30 minutes before the volcano erupts, so you don't have time to go back out the way you came in.
# You scan the cave for other options and discover a 
# network of pipes and pressure-release valves. You aren't sure how such a system got into a volcano, but you don't have time to complain; your device produces a report (your puzzle input) of each valve's flow rate if it were opened (in pressure per minute) and the tunnels you could use to move between the valves.
# There's even a valve in the room you and the elephants are currently standing 
# in labeled AA. You estimate it will take you one minute to open a single valve 
# and one minute to follow any tunnel from one valve to another. What is the most pressure you could release?

def part01(valves, map):
    q = deque([init_state()])
    seen = set()
    i = 0

    final_states = []
    max_press_for = defaultdict(int)
        
    while q:
        s = q.popleft()  # BFS
        # s = q.pop()  # DFS

        ## Skip if we've seen this state before
        snll = get_state_no_last_loc(s)
        if snll in seen:
            continue
        seen.add(snll)

        ## Skip if we've seen this state before, but with more pressure
        snp = get_state_no_press(s)
        if s.pressure_released < max_press_for[snp]:
            continue
        max_press_for[snp] = s.pressure_released

        i += 1
        if i % 500000 == 0:
            maxp = 0
            if len(final_states) > 0:
                maxp = max(s.pressure_released for s in final_states)
            print(
                f"Seen {i} states | {len(q)} in queue | {len(seen)} seen | s.minutes={s.minutes} | current max {maxp}"
            )

        # print(s)
        for n in get_neighbors(s, valves, map):
            # print("--> ", n)
            q.append(n)

        if s.minutes >= MAX_MINUTES:
            final_states.append(s)

    m = max(final_states, key=lambda s: s.pressure_released)
    # print(m)
    return m.pressure_released


# You're worried that even with an optimal approach, the pressure released won't be enough. 
# What if you got one of the elephants to help you?
# It would take you 4 minutes to teach an elephant how to open the right valves
#  in the right order, leaving you with only 26 minutes to actually execute your plan. 
#  Would having two of you working together be better, 
#  even if it means having less time? (Assume that you teach the elephant before opening any valves yourself, giving you both the same full 26 minutes.)

def part02(valves, vmap):
    q = deque([init_state_ele()])
    seen = set()
    i = 0

    final_states = []
    max_press_for = defaultdict(int)

    while q:
        s = q.popleft()  # BFS
        # s = q.pop()  # DFS

        ## Skip if we've seen this state before
        snll = get_state_no_last_loc(s)
        if snll in seen:
            continue
        seen.add(snll)
        seen.add(reverse_me_and_ele(snll))

        ## Skip if we've seen this state before, but with more pressure
        snp = get_state_no_press(s)
        if s.pressure_released < max_press_for[snp]:
            continue
        max_press_for[snp] = s.pressure_released

        i += 1
        if i % 500000 == 0:
            maxp = 0
            if len(final_states) > 0:
                maxp = max(s.pressure_released for s in final_states)
            print(
                f"Seen {i} states | {len(q)} in queue | {len(seen)} seen | s.minutes={s.minutes} | current max {maxp}"
            )

        # print(s)
        for n in get_neighbors_ele(s, valves, vmap):
            # print("--> ", n)
            q.append(n)

        if s.minutes >= MAX_MINUTES:
            final_states.append(s)

        if len(q) > 1000000:
            q = deque(
                sorted(q, key=lambda s: s.pressure_released, reverse=True)[:500000]
            )

    m = max(final_states, key=lambda s: s.pressure_released)
    # print("Final state count:", i)
    # print(m)
    return m.pressure_released

if __name__ == "__main__":
    valves, vmap = parse("day16.txt")
    print(part01(valves, vmap))
    print(part02(valves, vmap))

# answers 
# Part 1 - 1845
# Part 2 - 2286