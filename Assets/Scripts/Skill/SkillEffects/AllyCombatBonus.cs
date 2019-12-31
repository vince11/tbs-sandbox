using Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllyCombatBonus : SkillEffect
{
    private readonly List<Stat> stats;
    private readonly int tier;
    private readonly string type;

    public AllyCombatBonus(string stats, string tier, string type)
    {
        this.stats = stats.Split('/').Select((s) => System.Enum.Parse(typeof(Stat), s)).Cast<Stat>().ToList();
        this.tier = int.Parse(tier);
        this.type = type;
    }

    protected override void Apply(Unit user, Unit target, BattleStateMachine bsm)
    {
        if (type.Equals("form")) FormEffect(user, target, bsm);
    }

    private void FormEffect(Unit user, Unit target, BattleStateMachine bsm)
    {
        int bonus, val;

        switch (tier) //TODO: maybe a formula
        {
            case 1:
                val = bsm.unitManager.GetAllies(user, 2).Count;
                bonus = Mathf.Clamp(val, 0, 3);
                break;
            case 2:
                val = bsm.unitManager.GetAllies(user, 2).Count + 2;
                bonus = Mathf.Clamp(val, 0, 5);
                break;
            case 3:
                val = 2 * bsm.unitManager.GetAllies(user, 2).Count + 1;
                bonus = Mathf.Clamp(val, 0, 7);
                break;
            default:
                bonus = 0;
                break;
        }

        for (int i = 0; i < stats.Count; i++)
        {
            user.Stats[stats[i]].inCombatBonus += bonus;
        }
    }
}
