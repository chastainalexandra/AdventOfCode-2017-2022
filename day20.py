from tqdm import tqdm

def swap(nums, a, b): #This stays the same for both parts 
    assert (0 <= a < n) and (0 <= b < n)

    nums[a], nums[b] = nums[b], nums[a]
    return nums

def part1(nums,og,n): 
  

    for i, x in tqdm(og):   
        for idx in range(n):
            if nums[idx][0] == i:
                break

        assert nums[idx][1] == x

        if x < 0:
            cur = idx
            for _ in range(-x):
                nums = swap(nums, cur, (cur - 1) % n)
                cur = (cur - 1) % n

            continue

    if x > 0:
        cur = idx
        for _ in range(x):
            nums = swap(nums, cur, (cur + 1) % n)
            cur = (cur + 1) % n



    coords = [1000, 2000, 3000]


    for zero_idx in range(n):
        if nums[zero_idx][1] == 0:
            break

    part1Ans = 0

    for c in coords:  #summing the coordinates found at 1000 2000 and 3000
        part1Ans += nums[(zero_idx + c) % n][1]
        print("part 1 - ", part1Ans)


def part2():
    DECRYPT_KEY = 811589153

    with open("puzzleFile.txt") as fin:
        lines = fin.read().strip().split("\n")
        nums = list(map(int, lines))

        for i in range(len(nums)):
            nums[i] = (i, nums[i] * DECRYPT_KEY)

    n = len(nums)
    og = nums.copy()

    for _ in tqdm(range(10)):   #only need to go through this 10 time 
        for i, x in tqdm(og):
            for idx in range(n):
                if nums[idx][0] == i:
                    break

            assert (nums[idx][1]) == x

            x %= (n - 1)

            if x > 0:
                cur = idx
                for _ in range(x):
                    nums = swap(nums, cur, (cur + 1) % n)
                    cur = (cur + 1) % n

    coords = [1000, 2000, 3000]


    for zero_idx in range(n):
        if nums[zero_idx][1] == 0:
            break

    part2Ans = 0
    for c in coords:  #summing the coordinates found at 1000 2000 and 3000
        part2Ans += nums[(zero_idx + c) % n][1]
        print("part2 - ", part2Ans)

if __name__ == "__main__":
  with open("day20.txt") as fin:
        lines = fin.read().strip().split("\n")
        nums = list(enumerate(map(int, lines)))

        n = len(nums)
        og = nums.copy()
        part1(nums,og,n)
        #part2()

# answers 
# Part 1 - 8764
# Part 2 - 535648840980