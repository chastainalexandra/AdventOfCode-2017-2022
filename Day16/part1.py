from __future__ import annotations

import argparse
import collections
import itertools
import os.path
import re


RATE = re.compile(r'rate=(\d+);')
VALVES = re.compile(r'to valves? (.*)$')

INPUT_TXT = os.path.join(os.path.dirname(__file__), 'puzzleFile.txt')


def compute(s: str) -> int:
    edges = {}
    rates = {}

    for line in s.splitlines():
        _, name, *_ = line.split()
        match_valves = VALVES.search(line)
        assert match_valves is not None
        targets = match_valves[1].split(', ')
        match = RATE.search(line)
        assert match is not None
        edges[name] = targets
        rates[name] = int(match[1])

    weights = {}
    positive_rates = frozenset(k for k, v in rates.items() if v)
    meaningful_edges = ['AA', *positive_rates]
    for a, b in itertools.combinations(meaningful_edges, r=2):
        todo_bfs: collections.deque[tuple[str, ...]]
        todo_bfs = collections.deque([(a,)])
        while todo_bfs:
            path = todo_bfs.popleft()
            if path[-1] == b:
                break
            else:
                todo_bfs.extend((*path, n) for n in edges[path[-1]])
        weights[(a, b)] = len(path)
        weights[(b, a)] = len(path)

    # time to total
    best = -1
    todo: list[tuple[int, int, tuple[str, ...], frozenset[str]]]
    todo = [(0, 0, ('AA',), positive_rates)]
    while todo:
        score, time, route, possible = todo.pop()
        best = max(best, score)

        for p in possible:
            needed_time = time + weights[(route[-1], p)]
            if needed_time < 30:
                todo.append((
                    score + (30 - needed_time) * rates[p],
                    needed_time,
                    route + (p,),
                    possible - {p},
                ))

    return best




def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument('data_file', nargs='?', default=INPUT_TXT)
    args = parser.parse_args()

    with open(args.data_file) as f:
        print(compute(f.read()))

    return 0


if __name__ == '__main__':
    raise SystemExit(main())