using UnityEngine;
using Enums;
using System.Linq;

public class CombatManager : MonoBehaviour
{
    public void Combat(Unit attacker, Unit target)
    {
        Unit currentAttacker = attacker;
        Unit currentDefender = target;

        currentAttacker.ResetCombat(true);
        currentDefender.ResetCombat(false);

        //setting triangle advantage
        bool? attackerTA = currentAttacker.HasTriangleAdvantage(currentDefender);
        if (attackerTA.HasValue)
        {
            if (attackerTA.Value)
            {
                currentAttacker.TriangleAdvantage += 0.2f;
                currentDefender.TriangleAdvantage -= 0.2f;
            }
            else
            {
                currentAttacker.TriangleAdvantage -= 0.2f;
                currentDefender.TriangleAdvantage += 0.2f;
            }
        }
 
        // Counter Attack check. Before skill calculations
        if (currentDefender.AttackRange == currentAttacker.AttackRange) currentDefender.CounterAttackBuffer.Add(true);

        // Speed Check. After skill calculations
        bool? attackerFaster = currentAttacker.IsFasterThan(currentDefender);
        if (attackerFaster.HasValue)
        {
            if(attackerFaster.Value) currentAttacker.FollowUpBuffer.Add(1);
            else currentDefender.FollowUpBuffer.Add(1);
        }

        // Finalise follow up values
        if (currentAttacker.FollowUpBuffer.Sum() > 0) currentAttacker.AttacksPerRound = 2;
        if (currentDefender.FollowUpBuffer.Sum() > 0) currentDefender.AttacksPerRound = 2;

        // Finalise counterattack value
        if (currentDefender.CounterAttackBuffer.Count != 0 && !currentDefender.CounterAttackBuffer.Exists(x => x == false))
        {
            currentDefender.CanCounter = true;
        }

        // Switching attacker to defender depending on defender priority
        if (currentDefender.HasAttackPriority && currentDefender.CanCounter)
        {
            Swap(ref currentAttacker, ref currentDefender);
        }

        // Combat Loop
        while (currentAttacker.AttacksPerRound > 0 && currentAttacker.CurrentHP > 0 && currentDefender.CurrentHP > 0)
        {
            currentAttacker.ResetDamages();
            while(currentAttacker.HitsPerAttack > 0)
            {
                CalculateDamage(currentAttacker, currentDefender);
                UpdateSpecialCooldown(currentAttacker);
                UpdateSpecialCooldown(currentDefender);

                currentAttacker.HitsPerAttack--;
                currentAttacker.FirstHit = false;
                currentAttacker.ConsecutiveHit = true;
            }

            currentAttacker.AttacksPerRound--;

            if((currentAttacker.HasFollowUpPriority || currentAttacker.AttacksPerRound == 0) &&
               ((currentDefender.CanCounter || currentDefender.IsInitiator) && currentDefender.AttacksPerRound > 0))
            {
                currentAttacker.ConsecutiveHit = false;
                Swap(ref currentAttacker, ref currentDefender);
            }
        }

        Debug.Log("Combat End");
    }

    private void Swap(ref Unit A, ref Unit B)
    {
        Unit tmp = A;
        A = B;
        B = tmp;
    }

    private void UpdateSpecialCooldown(Unit unit)
    {
        if(unit.SpecialCDCount != null)
        {
            if (unit.SpecialCDCount == 0) unit.SpecialCDCount = unit.OriginalSpecialCD;
            else unit.SpecialCDCount -= unit.SpecialCDCharge;
        }
    }

    private void CalculateDamage(Unit attacker, Unit target)
    {
        int mit = attacker.TargetedStat == Stat.Defense ? target.Defense.GetCombatValue() : target.Resistance.GetCombatValue();
        int wta = (int) Mathf.Sign(attacker.TriangleAdvantage) * Mathf.FloorToInt(Mathf.Abs(attacker.Attack.GetCombatValue() * attacker.TriangleAdvantage));

        // Base Damage
        attacker.BaseDamage += attacker.Attack.GetCombatValue() + wta - mit;

        int adjustedDamage = (attacker.BaseDamage + attacker.BoostedDamage) > 0 ? attacker.BaseDamage + attacker.BoostedDamage : 0;

        // Total Damage
        attacker.TotalDamage += adjustedDamage + attacker.TrueDamage;

        // Apply Effectiveness
        attacker.TotalDamage *= attacker.Effectiveness;
    }
}
