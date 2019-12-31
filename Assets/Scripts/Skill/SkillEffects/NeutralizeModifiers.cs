using Enums;
using System.Collections.Generic;
using System.Linq;

public class NeutralizeModifiers : SkillEffect
{
    private readonly List<Stat> stats;
    private readonly string applyTo;
    private readonly string type;

    public NeutralizeModifiers(string stats, string applyTo, string type)
    {
        this.stats = stats.Split('/').Select((s) => System.Enum.Parse(typeof(Stat), s)).Cast<Stat>().ToList();
        this.applyTo = applyTo;
        this.type = type;
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        Unit myUnit = (applyTo.Equals("user")) ? user : target;

        foreach (Stat stat in stats)
        {
            if (type.Equals("buff")) myUnit.Stats[stat].useBuff = false;
            else myUnit.Stats[stat].useDebuff = false;
        }
    }
}
