public class BonusDoubler : SkillEffect
{
    private readonly float percentage;

    public BonusDoubler(string percentage)
    {
        this.percentage = float.Parse(percentage);
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        foreach (UnitStat stat in user.Stats.Values)
        {
            if(stat.buff > 0) stat.inCombatBonus += UnityEngine.Mathf.FloorToInt(stat.buff * percentage);
        }
    }
}
