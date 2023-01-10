namespace AdventOfCode.Y2017.Day19;

[ProblemName("A Series of Tubes")]
class Solution : Solver {

    // public string GetName() => "A Series of Tubes"; have to move this to above with upgrade 
    Helper h = new Helper();

    // How many steps does the packet need to go?
    public object PartTwo(string fileData) => h.NetworkPacketPath(fileData).steps;

    // The little packet looks up at you, hoping you can help it find the way.
    //  What letters will it see (in the order it would see them) if it follows the path? 
    // (The routing diagram is very wide; make sure you view it without line wrapping.)
     public object PartOne(string fileData) => h.NetworkPacketPath(fileData).msg;
 
}
