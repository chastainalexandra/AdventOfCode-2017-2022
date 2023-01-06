from tqdm import tqdm

def swap(nums, a, b): #This stays the same for both parts 
    assert (0 <= a < n) and (0 <= b < n)

    nums[a], nums[b] = nums[b], nums[a]
    return nums

def part1(nums,og,n): 
    part1Ans = 0
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

    for c in coords:  #summing the coordinates found at 1000 2000 and 3000
        part1Ans += nums[(zero_idx + c) % n][1]
        print("part 1 - ", part1Ans)


def part2(nums2,n2,og2):
    for _ in tqdm(range(10)):   #only need to go through this 10 time 
        for i, x in tqdm(og2):
            for idx in range(n2):
                if nums2[idx][0] == i:
                    break

            assert (nums2[idx][1]) == x

            x %= (n2 - 1)

            if x > 0:
                cur = idx
                for _ in range(x):
                    nums2 = swap(nums2, cur, (cur + 1) % n2)
                    cur = (cur + 1) % n2

    coords = [1000, 2000, 3000]


    for zero_idx in range(n2):
        if nums[zero_idx][1] == 0:
            break

    part2Ans = 0
    for c in coords:  #summing the coordinates found at 1000 2000 and 3000
        part2Ans += nums2[(zero_idx + c) % n2][1]
        print("part2 - ", part2Ans)

if __name__ == "__main__":
# This is gross and need to come back and clean this up
  with open("day20.txt") as fin:
    lines = fin.read().strip().split("\n")
    nums = list(enumerate(map(int, lines)))

  n = len(nums)
  og = nums.copy()
  part1(nums,og,n)

  DECRYPT_KEY = 811589153

  with open("day20.txt") as fin:
    lines2 = fin.read().strip().split("\n")
    nums2 = list(map(int, lines2))

    for i in range(len(nums2)):
        nums2[i] = (i, nums2[i] * DECRYPT_KEY)

    n2 = len(nums2)
    og2 = nums2.copy()
    part2(nums2,n2,og2)

# answers 
# Part 1 - 8764
# Part 2 - 535648840980