using System;
using System.Collections.Generic;
using System.Linq;
using Enums;

public class SkillConditionDatabase
{
    private Dictionary<string, Func<Unit, Unit, BattleStateMachine, bool>> conditions;

    public SkillConditionDatabase()
    {
        conditions = new Dictionary<string, Func<Unit, Unit, BattleStateMachine, bool>>
        {
            {"IsInitiator", (u, t, b) => u.IsInitiator }
        };
    }

    public Func<Unit, Unit, BattleStateMachine, bool> GetSkillConditions(string conditionName)
    {
        return conditions[conditionName];
    }
}
