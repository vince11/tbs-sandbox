using System.Collections.Generic;

public class StatusModifier : SkillEffect
{
    private readonly string status;
    private readonly string type;
    private readonly int hpOffset;

    public StatusModifier(string status)
    {
        this.status = status;
        type = "cardinal";
    }

    public StatusModifier(string status, string hpOffset)
    {
        this.status = status;
        this.hpOffset = int.Parse(hpOffset);
        type = "globalAdjacent";
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (type.Equals("cardinal")) CardinalDirections(user, target, bsm);
        else if (type.Equals("globalAdjacent")) GlobalAdjacent(user, target, bsm);
    }

    private void CardinalDirections(Unit user, Unit target, BattleStateMachine bsm)
    {
        List<Unit> foes = bsm.unitManager.GetFoes(user, 99);

        foreach (Unit foe in foes)
        {
            if(bsm.grid.IsCardinal(user, foe))
            {
                foe.unitData.status.Add(GameManager.Instance.skillDatabase.GetSkill(status));
            }
        }
    }

    private void GlobalAdjacent(Unit user, Unit target, BattleStateMachine bsm)
    {
        foreach (Unit foe in bsm.unitManager.GetFoes(user, 99))
        {
            if ((foe.CurrentHP <= user.CurrentHP - hpOffset) && bsm.unitManager.GetAllies(foe, 1).Count > 0)
            {
                foe.unitData.status.Add(GameManager.Instance.skillDatabase.GetSkill(status));
            }
        }
    }

    private void AllyBuff(Unit user, Unit target, BattleStateMachine bsm)
    {
        List<Unit> allies = bsm.unitManager.GetAllies(user, 1);

        foreach (Unit ally in allies)
        {
            //TODO:
        }
    }
}
