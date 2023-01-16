using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2018.Day14;

public class Helper {
    public IEnumerable<(int i, string st)> GetRecipes(int recipe) {
        var st = "";
        var i = 0;
        // To create new recipes, the two Elves combine their current recipes.
        //  This creates new recipes from the digits of the sum of the current recipes' scores.
        foreach (var score in Scores()) {
            i++;
            st += score;
            if (st.Length > recipe) {
                st = st.Substring(st.Length - recipe);
            }
            if (st.Length == recipe) {
                yield return (i - recipe, st);
            }
        }
    }

    // The Elves think their skill will improve after making a few recipes (your puzzle input). 
    // However, that could take ages; you can speed this up considerably by identifying the scores of the ten recipes after that. For example
    // If the Elves think their skill will improve after making 9 recipes, 
    // the scores of the ten recipes after the first nine on the scoreboard would be 5158916779 (highlighted in the last line of the diagram).
    // After 5 recipes, the scores of the next ten would be 0124515891.
    // After 18 recipes, the scores of the next ten would be 9251071085.
    // After 2018 recipes, the scores of the next ten would be 5941429882.

    public IEnumerable<int> Scores() {
        var scores = new List<int>();
        Func<int, int> add = (i) => { scores.Add(i); return i; };

        var elf1 = 0;
        var elf2 = 1;

        yield return add(3); // the first recipe got a score of 3
        yield return add(7); // he second, 7


        // To create new recipes, the two Elves combine their current recipes. 
        // This creates new recipes from the digits of the sum of the current recipes' scores.
        //  With the current recipes' scores of 3 and 7, their sum is 10, and so two new recipes would be created:
        //  the first with score 1 and the second with score 0. If the current recipes' scores were 2 and 3, 
        // the sum, 5, would only create one recipe (with a score of 5) with its single digit.
        while (true) {
            var sum = scores[elf1] + scores[elf2];
            if (sum >= 10) {
                yield return add(sum / 10);
            }
            yield return add(sum % 10); // 

            elf1 = (elf1 + scores[elf1] + 1) % scores.Count;
            elf2 = (elf2 + scores[elf2] + 1) % scores.Count;
        }
    }
    
}