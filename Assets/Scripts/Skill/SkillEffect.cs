using System;
using System.Collections.Generic;

public abstract class SkillEffect
{
    public List<Func<Unit, Unit, BattleStateMachine, bool>> conditions;
    public Enums.ActivationType activationType;

    protected abstract void Apply(Unit user, Unit target, BattleStateMachine bsm);

    public void Activate(Unit user, Unit target, BattleStateMachine bsm)
    {
        bool valid = true;

        foreach(Func<Unit, Unit, BattleStateMachine, bool> validate in conditions)
        {
            valid = valid && validate(user, target, bsm);
        }

        if (valid) Apply(user, target, bsm);
    }
}

