public class CounterAttack : SkillEffect
{
    private readonly bool? userValue;
    private readonly bool? targetValue;
    private readonly string type;

    public CounterAttack()
    {
        type = "nullc";
    }

    public CounterAttack(string userValue, string targetValue)
    {
        this.userValue = string.IsNullOrEmpty(userValue) ? (bool?)null : System.Convert.ToBoolean(userValue);
        this.targetValue = string.IsNullOrEmpty(targetValue) ? (bool?)null : System.Convert.ToBoolean(targetValue);
        type = "set";
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (type.Equals("set")) AddCounterAttack(user, target, bsm);
        else NullCDisrupt(user, target, bsm);
    }

    private void AddCounterAttack(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (userValue.HasValue) user.CounterAttackBuffer.Add(userValue.Value);
        if (targetValue.HasValue) target.CounterAttackBuffer.Add(targetValue.Value);
    }

    private void NullCDisrupt(Unit user, Unit target, BattleStateMachine bsm)
    {
        user.CounterAttackBuffer.RemoveAll(x => x == false);
    }
}
