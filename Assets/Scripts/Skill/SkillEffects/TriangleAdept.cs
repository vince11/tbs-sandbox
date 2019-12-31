public class TriangleAdept : SkillEffect
{
    public float bonus;

    public TriangleAdept(string bonus)
    {
        this.bonus = float.Parse(bonus);
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        float limit = .4f;

        bool? userTA = user.HasTriangleAdvantage(target);
        if (userTA.HasValue)
        {
            if (userTA.Value)
            {
                user.TriangleAdvantage += bonus;
                target.TriangleAdvantage -= bonus;
            }
            else
            {
                user.TriangleAdvantage -= bonus;
                target.TriangleAdvantage += bonus;
            }
        }

        user.TriangleAdvantage = UnityEngine.Mathf.Clamp(user.TriangleAdvantage, -limit, limit);
        target.TriangleAdvantage = UnityEngine.Mathf.Clamp(target.TriangleAdvantage, -limit, limit);
    }
}
