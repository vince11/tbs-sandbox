using System.Collections.Generic;
using System.Linq;
using Enums;

public class CombatBonus : SkillEffect
{
    private readonly List<Stat> stats;
    private readonly List<int> userValues;
    private readonly List<int> targetValues;

    public CombatBonus(string stats, string userValues, string targetValues)
    {
        this.stats = stats.Split('/').Select((s) => System.Enum.Parse(typeof(Stat), s)).Cast<Stat>().ToList();
        this.userValues = string.IsNullOrEmpty(userValues) ? null : userValues.Split('/').Select((s) => int.Parse(s)).ToList();
        this.targetValues = string.IsNullOrEmpty(targetValues) ? null : targetValues.Split('/').Select((s) => int.Parse(s)).ToList();
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (userValues != null) user.Stats[stats[i]].inCombatBonus += userValues[i];

            if (targetValues != null) target.Stats[stats[i]].inCombatBonus += targetValues[i];
        }
    }
}
