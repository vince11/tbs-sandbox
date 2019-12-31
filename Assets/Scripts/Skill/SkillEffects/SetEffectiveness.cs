public class SetEffectiveness : SkillEffect
{
    private readonly int? userValue;
    private readonly int? targetValue;

    public SetEffectiveness(string userValue, string targetValue)
    {
        this.userValue = string.IsNullOrEmpty(userValue) ? (int?)null : int.Parse(userValue);
        this.targetValue = string.IsNullOrEmpty(targetValue) ? (int?)null : int.Parse(targetValue);
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (userValue.HasValue) user.Effectiveness = userValue.Value;
        if (targetValue.HasValue) target.Effectiveness = targetValue.Value;
    }
}
