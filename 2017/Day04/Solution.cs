using System;
using System.Linq;

namespace AdventOfCode.Y2017.Day04;

[ProblemName("High-Entropy Passphrases")]
class Solution : Solver {
// public string GetName() => "High-Entropy Passphrases"; have to move this to above with upgrade 
    
    int ValidPassphrase(string lines, Func<string, string> normalizer) =>
        lines.Split('\n').Where(line => IsValidPassphrase(line.Split(' '), normalizer)).Count();

    bool IsValidPassphrase(string[] passphrase, Func<string, string> normalizer) =>
        passphrase.Select(normalizer).Distinct().Count() == passphrase.Count();
    
    public object PartTwo(string fileData) =>
        ValidPassphrase(fileData, passphrase => string.Concat(passphrase.OrderBy(ch => ch)));
    
    public object PartOne(string fileData) =>
        ValidPassphrase(fileData, passphrase => passphrase); // How many passphrases are valid?



}
