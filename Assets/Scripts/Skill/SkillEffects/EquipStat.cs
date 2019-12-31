using System.Collections.Generic;
using System.Linq;
using Enums;

public class EquipStat : SkillEffect
{
    private readonly List<Stat> stats;
    private readonly List<int> values;

    private readonly int specialCDValue;

    private readonly string type;

    public EquipStat(string stats, string values)
    {
        this.stats = stats.Split('/').Select((s) => System.Enum.Parse(typeof(Stat), s)).Cast<Stat>().ToList();
        this.values = values.Split('/').Select((s) => int.Parse(s)).ToList();
        type = "stat";
    }

    public EquipStat(string specialCDValue)
    {
        this.specialCDValue = int.Parse(specialCDValue);
        type = "special";
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (type.Equals("stat")) UpdateStat(user, 1);
        else user.specialCooldown += specialCDValue;
    }

    public void Revert(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (type.Equals("stat")) UpdateStat(user, -1);
        else user.specialCooldown -= specialCDValue;
    }

    private void UpdateStat(Unit user, int sign)
    {
        int newValue;

        for (int i = 0; i < stats.Count; i++)
        {
            newValue = user.Stats[stats[i]].baseValue + (sign * values[i]);
            user.Stats[stats[i]].UpdateValues(newValue);
        }
    }
}
