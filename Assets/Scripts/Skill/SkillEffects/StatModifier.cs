using System.Collections.Generic;
using System.Linq;
using Enums;

public class StatModifier : SkillEffect
{
    private readonly List<Stat> stats;
    private readonly List<int> userValues;
    private readonly List<int> targetValues;
    private readonly string type;

    public StatModifier(string stats, string userValues, string targetValues, string type)
    {
        this.stats = stats.Split('/').Select((s) => System.Enum.Parse(typeof(Stat), s)).Cast<Stat>().ToList();
        this.userValues = string.IsNullOrEmpty(userValues) ? null : userValues.Split('/').Select((s) => int.Parse(s)).ToList();
        this.targetValues = string.IsNullOrEmpty(targetValues) ? null : targetValues.Split('/').Select((s) => int.Parse(s)).ToList();
        this.type = type;
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (type.Equals("buff"))
            {
                if (userValues != null) user.Stats[stats[i]].buff = userValues[i];
                if (targetValues != null) target.Stats[stats[i]].buff = targetValues[i];
            }
            else
            {
                if (userValues != null) user.Stats[stats[i]].debuff = userValues[i];
                if (targetValues != null) target.Stats[stats[i]].debuff = targetValues[i];
            }
        }
    }
}
