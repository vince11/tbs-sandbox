using System.Collections.Generic;
using System.Linq;
using Enums;

public class EquipStat : SkillEffect
{
    private readonly List<Stat> stats;
    private readonly List<int> values;

    public EquipStat(string stats, string values)
    {
        this.stats = stats.Split('/').Select((s) => System.Enum.Parse(typeof(Stat), s)).Cast<Stat>().ToList();
        this.values = values.Split('/').Select((s) => int.Parse(s)).ToList();
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        int newValue;

        for(int i = 0; i < stats.Count; i++)
        {
            newValue = user.Stats[stats[i]].baseValue + values[i];
            user.Stats[stats[i]].UpdateValues(newValue);
        }
    }

    public void Revert(Unit user, Unit target, BattleStateMachine bsm)
    {
        int newValue;

        for (int i = 0; i < stats.Count; i++)
        {
            newValue = user.Stats[stats[i]].baseValue - values[i];
            user.Stats[stats[i]].UpdateValues(newValue);
        }
    }
}
