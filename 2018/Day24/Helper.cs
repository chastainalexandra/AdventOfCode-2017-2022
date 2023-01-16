using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace AdventOfCode.Y2018.Day24;

public class Helper {
    public List<Groups> GetUnits(string fileInput) {
        var fl = fileInput.Split("\n");
        var immuneSystem = false;
        var gr = new List<Groups>();
        foreach (var line in fl)
            if (line == "Immune System:") {
                immuneSystem = true;
            } else if (line == "Infection:") {
                immuneSystem = false;
            } else if (line != "") {
                var regex = @"(\d+) units each with (\d+) hit points(.*)with an attack that does (\d+)(.*)damage at initiative (\d+)";
                var regexMatch = Regex.Match(line, regex);
                if (regexMatch.Success) {
                    Groups group = new Groups();
                    group.immuneSystem = immuneSystem;
                    group.units = int.Parse(regexMatch.Groups[1].Value);
                    group.hitPoints = int.Parse(regexMatch.Groups[2].Value);
                    group.damage = int.Parse(regexMatch.Groups[4].Value);
                    group.attackType = regexMatch.Groups[5].Value.Trim();
                    group.initiative = int.Parse(regexMatch.Groups[6].Value);
                    var str = regexMatch.Groups[3].Value.Trim();
                    if (str != "") {
                        str = str.Substring(1, str.Length - 2);
                        foreach (var li in str.Split(";")) {
                            var l = li.Split(" to ");
                            var fight = new HashSet<string>(l[1].Split(", "));
                            var weaknessOrImmunities = l[0].Trim();
                            if (weaknessOrImmunities == "immune") {
                                group.immuneTo = fight;
                            } else if (weaknessOrImmunities == "weak") {
                                group.weakTo = fight;
                            } else {
                                throw new Exception();
                            }
                        }
                    }
                    gr.Add(group);
                } else {
                    throw new Exception();
                }

            }
        return gr;
    }

    public (bool immuneSystem, long units) DetermineUnitsNeeded(string fileInput, int boost) {
        var armies = GetUnits(fileInput);
        foreach (var g in armies) {
            if (g.immuneSystem) {
                g.damage += boost;
            }
        }
        var attack = true;
        // The damage an attacking group deals to a defending group depends on the attacking group's attack type
        // and the defending group's immunities and weaknesses. By default, an attacking group would deal damage 
        // equal to its effective power to the defending group. However, if the defending group is immune to the 
        // attacking group's attack type, the defending group instead takes no damage; if the defending group is 
        // weak to the attacking group's attack type, the defending group instead takes double damage.

        // The defending group only loses whole units from damage; damage is always dealt in such a way that it 
        // kills the most units possible, and any remaining damage to a unit that does not immediately kill it 
        // is ignored. For example, if a defending group contains 10 units with 10 hit points each and receives 
        // 75 damage, it loses exactly 7 units and is left with 3 units at full health.

        while (attack) {
            attack = false;
            var attackingGroup = new HashSet<Groups>(armies);
            var targets = new Dictionary<Groups, Groups>();
            foreach (var g in armies.OrderByDescending(g => (g.effectivePower, g.initiative))) {
                var fullDamage = attackingGroup.Select(t => g.Damage(t)).Max();
                if (fullDamage > 0) {
                    var possibleTargets = attackingGroup.Where(t => g.Damage(t) == fullDamage);
                    targets[g] = possibleTargets.OrderByDescending(t => (t.effectivePower, t.initiative)).First();
                    attackingGroup.Remove(targets[g]);
                }
            }
            foreach (var g in targets.Keys.OrderByDescending(g => g.initiative)) {
                if (g.units > 0) {
                    var targetSelection = targets[g];
                    var damage = g.Damage(targetSelection);
                    if (damage > 0 && targetSelection.units > 0) {
                        var dies = damage / targetSelection.hitPoints;
                        targetSelection.units = Math.Max(0, targetSelection.units - dies);
                        if (dies > 0) {
                            attack = true;
                        }
                    }
                }
            }
            armies = armies.Where(g => g.units > 0).ToList();
        }
        return (armies.All(x => x.immuneSystem), armies.Select(x => x.units).Sum());
    }

    
}