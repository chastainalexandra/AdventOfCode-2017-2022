import sys
import copy

# You feel the ground rumble again as the distress signal 
# leads you to a large network of subterranean tunnels. 
# You don't have time to search them all, 
# but you don't need to: your pack contains a set of deployable 
# sensors that you imagine were originally built to locate lost Elves.
# The sensors aren't very powerful, but that's okay;
#  your handheld device indicates that you're close enough to the source of the distress signal to use them. You pull the emergency sensor system out of your pack, hit the big button on top, and the sensors zoom off down the tunnels.
# Once a sensor finds a spot it thinks will give it a good reading, 
# it attaches itself to a hard surface and begins monitoring for the nearest signal 
# source beacon. Sensors and beacons always exist at integer coordinates. 
# Each sensor knows its own position and can determine the position of a beacon precisely; however, sensors can only lock on to the one beacon closest to the sensor as measured by the Manhattan distance. (There is never a tie where two beacons are the same distance to a sensor.)


def part01(lines):
    def dist(a, b):
        return abs(a[0] - b[0]) + abs(a[1] - b[1])


    sensors = []
    beacons = []
    for line in lines:
        parts = line.split(" ")

        sx = int(parts[2][2:-1])
        sy = int(parts[3][2:-1])
        bx = int(parts[8][2:-1])
        by = int(parts[9][2:])

        sensors.append((sx, sy))
        beacons.append((bx, by))


    N = len(sensors)
    dists = []

    for i in range(N):
        dists.append(dist(sensors[i], beacons[i]))

    Y = 2000000

    intervals = []

    for i, s in enumerate(sensors):
        dx = dists[i] - abs(s[1] - Y)

        if dx <= 0:
            continue

        intervals.append((s[0] - dx, s[0] + dx))


    # INTERVAL OVERLAP ETC.
    allowed_x = []
    for bx, by in beacons:
        if by == Y:
            allowed_x.append(bx)

    print(allowed_x)

    min_x = min([i[0] for i in intervals])
    max_x = max([i[1] for i in intervals])

    ans = 0
    for x in range(min_x, max_x + 1):
        if x in allowed_x:
            continue

        for left, right in intervals:
            if left <= x <= right:
                ans += 1
                break


    print(ans)

def part02(lines):
    sensors = []
    sizeOfInterest = 4000000
    coordinatesOfInterest = []
    minX = sys.maxsize
    minY = sys.maxsize


    for s in sensors:
        sPos = s[0]
        distance = s[1]
        coord = [sPos[0] - distance - 1, sPos[1]]
        directions = [[1, -1], [1, 1], [-1, 1], [-1, -1]]
        for dir in range(4):
            for n in range(distance + 1):
                if (
                    coord[0] + minY >= 0
                    and coord[1] + minX >= 0
                    and coord[0] + minY <= sizeOfInterest
                    and coord[1] + minX <= sizeOfInterest
                ):
                    coordinatesOfInterest.append(copy.deepcopy(coord))
                coord[0] += directions[dir][0]
                coord[1] += directions[dir][1]

    for c in coordinatesOfInterest:
        y = c[0]
        x = c[1]
        distressBeacon = True
        for s in sensors:
            distance = s[1]
            if abs(y - s[0][0]) + abs(x - s[0][1]) <= distance:
                distressBeacon = False
        if distressBeacon:
            print(x + minX, y + minY)
            print((x + minX) * 4000000 + (y + minY))

if __name__ == "__main__":
   with open("day15.txt") as fin:
    lines = fin.read().strip().split("\n")
   part01(lines)
   part02(lines)

# answers 
# Part 1 - 5125700
# Part 2 - 11379394658764