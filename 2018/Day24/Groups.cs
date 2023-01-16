using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day24;

public class Groups {
    // Units within a group all have the same hit points (amount of damage a unit can take before it is destroyed),
    // attack damage (the amount of damage each unit deals), an attack type, 
    // an initiative (higher initiative units attack first and win ties), 
    // and sometimes weaknesses or immunities. 
    public bool immuneSystem;
    public long units;
    public int hitPoints;
    public int damage;
    public int initiative;
    public string attackType;
    public HashSet<string> immuneTo = new HashSet<string>();
    public HashSet<string> weakTo = new HashSet<string>();

    // Each group also has an effective power: 
    // the number of units in that group multiplied by their attack damage. 
    // The above group has an effective power of 18 * 8 = 144. Groups never have zero or negative units; 
    // instead, the group is removed from combat.
    public long effectivePower {
        get {
            return units * damage;
        }
    }

    // The damage an attacking group deals to a defending group depends on
    // the attacking group's attack type and the defending group's immunities and weaknesses.
    // By default, an attacking group would deal damage equal to its effective power to the defending group.
    // However, if the defending group is immune to the attacking group's attack type, the defending group 
    // instead takes no damage; if the defending group is weak to the attacking group's attack type,
    // the defending group instead takes double damage.
    public long Damage(Groups targetSelection) {
        if (targetSelection.immuneSystem == immuneSystem) {
            return 0;
        } else if (targetSelection.immuneTo.Contains(attackType)) {
            return 0;
        } else if (targetSelection.weakTo.Contains(attackType)) {
            return effectivePower * 2;
        } else {
            return effectivePower;
        }
    }
}
