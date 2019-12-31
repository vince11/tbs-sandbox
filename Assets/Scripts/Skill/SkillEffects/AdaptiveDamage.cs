public class AdaptiveDamage : SkillEffect
{
    private readonly string action;

    public AdaptiveDamage(string action)
    {
        this.action = action;
    }
    
    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (action.Equals("apply")) CalculateAdaptive(user, target, bsm);
        else NegateAdaptive(user, target, bsm);
    }

    private void CalculateAdaptive(Unit user, Unit target, BattleStateMachine bsm)
    {
        int def = target.Defense.GetCombatValue();
        int res = target.Resistance.GetCombatValue();

        user.TargetedStat = (def < res) ? Enums.Stat.Defense : Enums.Stat.Resistance;
    }

    private void NegateAdaptive(Unit user, Unit target, BattleStateMachine bsm)
    {
        if(target.Skills[Enums.SkillType.Weapon] != null) target.TargetedStat = target.Skills[Enums.SkillType.Weapon].targetStat;
    }
}
