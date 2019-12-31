public class Priority : SkillEffect
{
    private readonly bool? userValue;
    private readonly bool? targetValue;
    private readonly string type;

    public Priority(string userValue, string targetValue, string type)
    {
        this.userValue = string.IsNullOrEmpty(userValue) ? (bool?)null : System.Convert.ToBoolean(userValue);
        this.targetValue = string.IsNullOrEmpty(targetValue) ? (bool?)null : System.Convert.ToBoolean(targetValue);
        this.type = type;
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (type.Equals("followup")) SetFollowUpPriority(user, target, bsm);
        else SetAttackPriority(user, target, bsm);
    }

    private void SetFollowUpPriority(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (userValue.HasValue) user.HasFollowUpPriority = userValue.Value;
        if (targetValue.HasValue) target.HasFollowUpPriority = targetValue.Value;
    }

    private void SetAttackPriority(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (userValue.HasValue) user.HasAttackPriority = userValue.Value;
        if (targetValue.HasValue) target.HasAttackPriority = targetValue.Value;
    }
}
