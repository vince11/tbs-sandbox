using System.Collections.Generic;
using Enums;

public class Skill
{
    //common data
    public string name;
    public string description;
    public SkillType type;
    public int priority;

    //weapon properties
    public int might;
    public Stat targetStat;

    //special skill properties
    public int cooldown;
    public bool isDefensive;

    //assist skill properties
    public int range;
    public AssistType assistType;

    public List<SkillEffect> effects = new List<SkillEffect>();
    
    public virtual void ApplyEffects(Unit user, Unit target, ActivationType activationType, BattleStateMachine bsm)
    {
        foreach (SkillEffect e in effects)
        {
            if(activationType == e.activationType) e.Activate(user, target, bsm);
        }
    }
}
