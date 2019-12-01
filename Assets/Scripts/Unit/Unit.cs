using UnityEngine;
using Enums;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public UnitData unitData;

    public WeaponColor WeapColor { get { return unitData.unitClass.WeaponColor; } }
    public MovementType MoveType { get { return unitData.unitClass.MoveType; } }
    public int AttackRange { get { return unitData.unitClass.attackRange; } }
    public int MoveRange { get { return unitData.unitClass.moveRange; } }

    public float TriangleAdvantage { get { return combatProperties.triangleAdvantage; } set { combatProperties.triangleAdvantage = value; } }
    public int AttacksPerRound { get { return combatProperties.attacksPerRound; } set { combatProperties.attacksPerRound = value; } }
    public int HitsPerAttack { get { return combatProperties.hitsPerAttack; } set { combatProperties.hitsPerAttack = value; } }
    public bool FirstHit { get { return combatProperties.firstHit; } set { combatProperties.firstHit = value; } }
    public bool ConsecutiveHit { get { return combatProperties.consecutiveHit; } set { combatProperties.consecutiveHit = value; } }
    public bool CanCounter { get { return combatProperties.canCounterAttack; } set { combatProperties.canCounterAttack = value; } }
    public bool HasAttackPriority { get { return combatProperties.hasAttackPriority; } set { combatProperties.hasAttackPriority = value; } }
    public bool HasFollowUpPriority { get { return combatProperties.hasFollowUpPriority; } set { combatProperties.hasFollowUpPriority = value; } }
    public int CurrentHP { get { return combatProperties.currentHP; } set { combatProperties.currentHP = value; } }
    public int? SpecialCDCount { get { return combatProperties.specialCooldownCount; } set { combatProperties.specialCooldownCount = value; } }
    public int SpecialCDCharge { get { return combatProperties.specialCooldownCharge; } set { combatProperties.specialCooldownCharge = value; } }
    public bool IsInitiator { get { return combatProperties.isInitiator; } }
    public List<bool> CounterAttackBuffer { get { return combatProperties.counterAttack; } }
    public List<int> FollowUpBuffer { get { return combatProperties.followUp; } }
    public Stat TargetedStat { get { return combatProperties.targetedStat; } }
    public int BaseDamage { get { return combatProperties.baseDamage; } set { combatProperties.baseDamage = value; } }
    public int BoostedDamage { get { return combatProperties.boostedDamage; } set { combatProperties.boostedDamage = value; } }
    public int TrueDamage { get { return combatProperties.trueDamage; } set { combatProperties.trueDamage = value; } }
    public int TotalDamage { get { return combatProperties.totalDamage; } set { combatProperties.totalDamage = value; } }
    public int Effectiveness { get { return combatProperties.effectiveness; } set { combatProperties.effectiveness = value; } }

    public Dictionary<Stat, UnitStat> Stats { get { return unitData.stats; }}
    public UnitStat HP { get { return Stats[Stat.HP]; } }
    public UnitStat Attack { get { return Stats[Stat.Attack]; } }
    public UnitStat Speed { get { return Stats[Stat.Speed]; } }
    public UnitStat Defense { get { return Stats[Stat.Defense]; } }
    public UnitStat Resistance { get { return Stats[Stat.Resistance]; } }
    public int? OriginalSpecialCD { get { return unitData.specialCooldown; } } // null if no special equipped

    //actual special cooldown you see in battle -- set with originalspecialCD at the start of every battle
    [System.NonSerialized]
    public int? specialCooldown; 

    [System.NonSerialized]
    public Node node;

    [System.NonSerialized]
    public List<Node> movementNodes;

    [System.NonSerialized]
    public List<Node> attackNodes;

    private CombatProperties combatProperties;

    public void ResetCombat(bool isInitiator)
    {
        if(combatProperties == null) combatProperties = new CombatProperties();

        combatProperties.Reset();
        combatProperties.specialCooldownCount = specialCooldown; //forecast value
        combatProperties.currentHP = HP.currentValue; // forecast value
        combatProperties.isInitiator = isInitiator;
    }

    public void ResetDamages()
    {
        combatProperties.ResetDamageProperties();
    }

    public bool? HasTriangleAdvantage(Unit target)
    {
        if (WeapColor == target.WeapColor) return null;

        if (WeapColor == WeaponColor.Red && target.WeapColor == WeaponColor.Green ||
            WeapColor == WeaponColor.Green && target.WeapColor == WeaponColor.Blue ||
            WeapColor == WeaponColor.Blue && target.WeapColor == WeaponColor.Red)
        {
            return true;
        }
        else return false;
    }

    public bool? IsFasterThan(Unit target)
    {
        int val = Speed.GetCombatValue() - target.Speed.GetCombatValue();
        if (val >= 5) return true;
        else if (val <= -5) return false;
        else return null;
    }
}
