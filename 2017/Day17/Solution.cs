using System.Collections.Generic;

namespace AdventOfCode.Y2017.Day17;

[ProblemName("Spinlock")]
class Solution : Solver {

    // public string GetName() => "Spinlock"; have to move this to above with upgrade 

  // What is the value after 0 the moment 50000000 is inserted?
    public object PartTwo(string fileData) {
        var fd = int.Parse(fileData);
        var position = 0;
        var circularBufferCount = 1;
        var res = 0;
        for (int i = 1; i < 50000001; i++) {
            position = (position + fd) % circularBufferCount + 1;
            if (position == 1) {
                res = i;
            }
            circularBufferCount++;
        }
        return res;
    }

    // What is the value after 2017 in your completed circular buffer?
     public object PartOne(string fileData) {
        var fd = int.Parse(fileData);
        var circularBuffer = new List<int>() { 0 };
        var position = 0;
         /* 
         * It repeats this process of stepping forward, inserting a new value,
         * and using the location of the inserted value as the new current position a total of 2017 times, inserting 2017 as its final operation,
         *  and ending with a total of 2018 values (including 0) in the circular buffer.
         */
        for (int i = 1; i < 2018; i++) {
            position = (position + fd) % circularBuffer.Count + 1;
            circularBuffer.Insert(position, i);
        }
        return circularBuffer[(position + 1) % circularBuffer.Count];
    }
}
