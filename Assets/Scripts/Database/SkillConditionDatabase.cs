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
            {"OnAetherRaids", (u, t, b) => b.battleMode.Equals("Aether Raids") },
            {"OnDefenseTeam", (u, t, b) => b.unitManager.GetTeam(u).Equals("Defense") },
            {"OnOffenseTeam", (u, t, b) => b.unitManager.GetTeam(u).Equals("Offense") },
            {"IsInitiator", (u, t, b) => u.IsInitiator },
            {"IsDefender", (u, t, b) => !u.IsInitiator },
            {"IsAdjacent", (u, t, b) => b.unitManager.GetAllies(u, 1).Count >= 1 },
            {"IsSolo", (u, t, b) => b.unitManager.GetAllies(u, 1).Count == 0 },
            {"HP=100%", (u, t, b) => u.CurrentHP == u.HP.baseValue},
            {"HP>=25%", (u, t, b) => u.CurrentHP >= u.HP.baseValue * .25f},
            {"HP>=50%", (u, t, b) => u.CurrentHP >= u.HP.baseValue * .5f},
            {"HP>=70%", (u, t, b) => u.CurrentHP >= u.HP.baseValue * .7f},
            {"HP>=80%", (u, t, b) => u.CurrentHP >= u.HP.baseValue * .8f},
            {"HP>=90%", (u, t, b) => u.CurrentHP >= u.HP.baseValue * .9f},
            {"HP>=FoeHP+3", (u, t, b) => u.CurrentHP >= t.HP.baseValue + 3},
            {"HP<=25%", (u, t, b) => u.CurrentHP <= u.HP.baseValue * .25f},
            {"HP<=30%", (u, t, b) => u.CurrentHP <= u.HP.baseValue * .3f},
            {"HP<=40%", (u, t, b) => u.CurrentHP <= u.HP.baseValue * .4f},
            {"HP<=50%", (u, t, b) => u.CurrentHP <= u.HP.baseValue * .5f},
            {"HP<=75%", (u, t, b) => u.CurrentHP <= u.HP.baseValue * .75f},
            {"HP<=80%", (u, t, b) => u.CurrentHP <= u.HP.baseValue * .8f},
            {"Spd>=FoeSpd+5", (u, t, b) => u.Speed.GetCombatValue() >= t.Speed.GetCombatValue() + 5},
            {"Spd>=FoeSpd+3", (u, t, b) => u.Speed.GetCombatValue() >= t.Speed.GetCombatValue() + 3},
            {"Spd>=FoeSpd", (u, t, b) => u.Speed.GetCombatValue() >= t.Speed.GetCombatValue()},
            {"Atk>=FoeAtk+5", (u, t, b) => u.Attack.GetCombatValue() >= t.Attack.GetCombatValue() + 5},
            {"Atk>=FoeAtk+3", (u, t, b) => u.Attack.GetCombatValue() >= t.Attack.GetCombatValue() + 3},
            {"Atk>=FoeAtk", (u, t, b) => u.Attack.GetCombatValue() >= t.Attack.GetCombatValue()},
            {"FoeIsMelee", (u, t, b) => t.AttackRange == 1 },
            {"FoeIsRange", (u, t, b) => t.AttackRange == 2 },
            {"NumAlliesWithin2>=2", (u, t, b) => b.unitManager.GetAllies(u, 2).Count >= 2},
            {"TargetUseMagic", (u, t, b) => t.WeapType == WeaponType.Tome || t.WeapType == WeaponType.Staff || t.WeapType == WeaponType.Dragonstone},
            {"TargetUsePhysical", (u, t, b) => t.WeapType != WeaponType.Tome && t.WeapType != WeaponType.Staff && t.WeapType != WeaponType.Dragonstone},
            {"IsAdjacentToMagic", (u, t, b) => b.unitManager.GetAllies(u, 1).Exists( x => x.WeapType == WeaponType.Tome ) },
            {"HasMovementAssist", (u, t, b) => u.Skills[SkillType.Assist].assistType == AssistType.Movement },
            {"HasRallyAssist", (u, t, b) => u.Skills[SkillType.Assist].assistType == AssistType.Rally },
            {"HasRefreshAssist", (u, t, b) => u.Skills[SkillType.Assist].assistType == AssistType.Refresh },
            {"TargetIsBlue", (u, t, b) => t.WeapColor == WeaponColor.Blue},
            {"TargetIsGreen", (u, t, b) => t.WeapColor == WeaponColor.Green},
            {"TargetIsRed", (u, t, b) => t.WeapColor == WeaponColor.Red},
            {"TargetHas-A-S-L", (u, t, b) => t.WeapType == WeaponType.Axe || t.WeapType == WeaponType.Lance || t.WeapType == WeaponType.Sword},
            {"TargetHasAxe", (u, t, b) => t.WeapType == WeaponType.Axe},
            {"TargetHasLance", (u, t, b) => t.WeapType == WeaponType.Lance},
            {"TargetHasSword", (u, t, b) => t.WeapType == WeaponType.Sword},
            {"TargetHasBow", (u, t, b) => t.WeapType == WeaponType.Bow},
            {"TargetHasDagger", (u, t, b) => t.WeapType == WeaponType.Dagger},
            {"TargetHasTome", (u, t, b) => t.WeapType == WeaponType.Tome},
            {"TargetHasDragonstone", (u, t, b) => t.WeapType == WeaponType.Dragonstone},
            {"TargetIsCavalry", (u, t, b) => t.MoveType == MovementType.Cavalry},
            {"TargetIsFlier", (u, t, b) => t.MoveType == MovementType.Flier},
            {"TargetCanCounter", (u, t, b) => t.CounterAttackBuffer.Count != 0 && !t.CounterAttackBuffer.Exists(x => x == false) },
            {"HasSpecial", (u, t, b) => u.Skills[SkillType.Special] != null},
            {"SpecialTriggers", (u, t, b) => u.SpecialCDCount == 0},
            {"SpecialIsDefensive", (u, t, b) => u.Skills[SkillType.Special].isDefensive},
            {"SpecialIsOffensive", (u, t, b) => !u.Skills[SkillType.Special].isDefensive},
            {"TargetConsecutiveHit", (u, t, b) => t.ConsecutiveHit},
            {"KillingIntentCondition", (u, t, b) => t.CurrentHP <= t.HP.baseValue || t.unitData.status.Exists(x => x.name.Equals("Penalty"))},
            {"OddTurn", (u, t, b) => b.turn % 2 != 0},
            {"TurnDivisible4", (u, t, b) => b.turn % 4 == 0},
            {"TurnDivisible3", (u, t, b) => b.turn % 3 == 0},
            {"TurnDivisible2", (u, t, b) => b.turn % 2 == 0},
            {"IsTurn1", (u, t, b) => b.turn == 1},
            {"AsheraChosenCondition", (u, t, b) => 
                !b.unitManager.GetAllies(u, 1).Exists((ally) => ally.WeapType != WeaponType.Dragonstone && ally.WeapType != WeaponType.Beast) ||
                b.unitManager.GetAllies(u, 1).Count == 0
            }
        };
    }

    public Func<Unit, Unit, BattleStateMachine, bool> GetSkillConditions(string conditionName)
    {
        return conditions[conditionName];
    }
}
