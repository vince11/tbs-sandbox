public class FollowUp : SkillEffect
{
    private readonly int? userValue;
    private readonly int? targetValue;
    private readonly string type;

    public FollowUp()
    {
        type = "nullf";
    }
    public FollowUp(string userValue, string targetValue)
    {
        this.userValue = string.IsNullOrEmpty(userValue) ? (int?)null : int.Parse(userValue);
        this.targetValue = string.IsNullOrEmpty(targetValue) ? (int?)null : int.Parse(targetValue);
        type = "set";
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (type.Equals("set")) AddFollowUp(user, target, bsm);
        else NullFollowUp(user, target, bsm);
    }

    private void AddFollowUp(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (userValue.HasValue) user.FollowUpBuffer.Add(userValue.Value);
        if (targetValue.HasValue) target.FollowUpBuffer.Add(targetValue.Value);
    }

    private void NullFollowUp(Unit user, Unit target, BattleStateMachine bsm)
    {
        user.FollowUpBuffer.RemoveAll(x => x == -1);
        target.FollowUpBuffer.RemoveAll(x => x == 1);
    }
}
