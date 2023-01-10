using System;
using System.Collections.Generic;

namespace AdventOfCode.Y2017.Day22;


[ProblemName("Sporifica Virus")]
class Solution : Solver {

    // public string GetName() => "Sporifica Virus"; have to move this to above with upgrade 

    
   // Given your actual map, after 10000000 bursts of activity, 
   // how many bursts cause a node to become infected? (Do not count nodes that begin infected.) 
    public object PartTwo(string fileData) =>
         Solve.FindInfections(fileData, 10000000, 
            (state, drow, dcol) => 
                state switch {
                    VirusStates.Clean => (VirusStates.Weakened, -dcol, drow),
                    VirusStates.Weakened => (VirusStates.Infected, drow, dcol),
                    VirusStates.Infected => (VirusStates.Flagged, dcol, -drow),
                    VirusStates.Flagged => (VirusStates.Clean, -drow, -dcol),
                    _ => throw new ArgumentException()
                }
        );

    // Given your actual map, after 10000 bursts of activity, 
    // how many bursts cause a node to become infected? (Do not count nodes that begin infected.)
    public object PartOne(string fileData) =>
        Solve.FindInfections(fileData, 10000, 
            (state, drow, dcol) => 
                state switch {
                    VirusStates.Clean => (VirusStates.Infected, -dcol, drow),
                    VirusStates.Infected => (VirusStates.Clean, dcol, -drow),
                    _ => throw new ArgumentException()
                }
        );
}
