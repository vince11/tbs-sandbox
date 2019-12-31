public class UpdateDamage : SkillEffect
{
    private readonly int? userValue;
    private readonly int? targetValue;
    private readonly string type;

    public UpdateDamage(string userValue, string targetValue, string type)
    {
        this.userValue = string.IsNullOrEmpty(userValue) ? (int?)null : int.Parse(userValue);
        this.targetValue = string.IsNullOrEmpty(targetValue) ? (int?)null : int.Parse(targetValue);
        this.type = type;
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (type.Equals("truedmg")) TrueDamage(user, target, bsm);
        else TotalDamage(user, target, bsm);
    }

    private void TrueDamage(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (userValue.HasValue) user.TrueDamage += userValue.Value;
        if (targetValue.HasValue) target.TrueDamage += targetValue.Value;
    }

    private void TotalDamage(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (userValue.HasValue) user.TotalDamage += userValue.Value;
        if (targetValue.HasValue) target.TotalDamage += targetValue.Value;
    }
}
