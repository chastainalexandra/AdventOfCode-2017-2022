namespace AdventOfCode.Y2018.Day20;
[ProblemName("A Regular Map")]
class Solution : Solver {

      Helper help = new Helper();

     // How many rooms have a shortest path from your current location that pass through at least 1000 doors?
     public object PartTwo(string fileInput) => help.GetDoors(fileInput).distantRooms;

     // What is the largest number of doors you would be required to pass through to reach a room? 
     // That is, find the room for which the shortest path from your starting location to that room would require passing through the most doors; 
     // what is the fewest doors you can pass through to reach it?
     public object PartOne(string fileInput) => help.GetDoors(fileInput).max;

}
