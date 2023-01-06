import numpy as np
import re

class Board:
    def __init__(self, nums):
        self.board = [
            [[nums[i][j], False] for j in range(5)]
            for i in range(5)
        ]

    def score(self, last_called):
        uncalled_sum = 0
        for row in range(5):
            for col in range(5):
                if not self.board[row][col][1]:
                    uncalled_sum += self.board[row][col][0]

        return uncalled_sum * last_called

    def win(self):
        for row in range(5):
            if all([self.board[row][i][1] for i in range(5)]):
                return True
        for col in range(5):
            if all([self.board[i][col][1] for i in range(5)]):
                return True
        return False

    def visited(self, num):
        for row in self.board:
            for el in row:
                if el[0] == num:
                    el[1] = True

def board(input):
    return [[int(i) for i in re.split(" +", line.strip())] for line in input]


def part01(input, nums):
    boards = []
    i = 2
    while i < len(input):
        x = board(input[i:i+5])
        boards.append(Board(x))
        i += 6

    ans = None
    for x in nums:
        for b in boards:
            b.visited(x)
        for b in boards:
            if b.win():
                ans = b.score(x)
                break

        if ans != None:
            break
    print(ans)

def part02(input):
    boards = []
    i = 2
    while i < len(input):
        x = board(input[i:i+5])
        boards.append(Board(x))
        i += 6

    order = []
    for x in nums:
        for i in range(len(boards)):
            boards[i].visited(x)

            if i not in order and boards[i].win():
                order.append(i)

        if len(order) == len(boards):
            break

    ans = boards[order[-1]].score(x)
    print(ans)

if __name__ == "__main__":
   with open("day04.txt") as fin:
    input = fin.read().strip().split("\n")
    nums = [int(i) for i in input[0].split(",")]
part01(input, nums) #Answer 89001 
part02(input) #Answer 7296