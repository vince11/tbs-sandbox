using Enums;
using System.Collections.Generic;

public class CombatProperties
{
    //forecast values -- values only applied to unit when combat is done
    //set before combat and after combat using values from unit class
    public int currentHP;
    public int? specialCooldownCount;

    //Damage Properties
    public int totalDamage;
    public int baseDamage;
    public int boostedDamage;
    public int trueDamage;

    //Point system for determining value of combat booleans
    public List<int> followUp = new List<int>();
    public List<bool> counterAttack = new List<bool>();

    //Combat Booleans
    public bool isInitiator;
    public bool canCounterAttack;
    public bool hasAttackPriority; // vantage
    public bool hasFollowUpPriority; // desperation

    public float triangleAdvantage;
    
    public int specialCooldownCharge; //how much specialcooldown decreases per attack

    public Stat targetedStat;

    public bool firstHit;
    public bool consecutiveHit;

    public int effectiveness;
    public int attacksPerRound; // determined via follow up value
    public int hitsPerAttack; // brave effect

    public void Reset()
    {
        ResetDamageProperties();

        followUp.Clear();
        counterAttack.Clear();

        isInitiator = false;
        canCounterAttack = false;
        hasAttackPriority = false; // checked only by defending unit
        hasFollowUpPriority = false;
        
        specialCooldownCharge = 1; // only highest applied, no stacking

        triangleAdvantage = 0.0f;

        targetedStat = Stat.Defense; // defaulted to defense, updated when weapon skill activated

        firstHit = true;
        consecutiveHit = false;

        effectiveness = 1;
        attacksPerRound = 1;
        hitsPerAttack = 1;
    }

    public void ResetDamageProperties()
    {
        totalDamage = 0;
        baseDamage = 0;
        boostedDamage = 0;
        trueDamage = 0;
    }
}

