using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatData
{
    public Unit attacker;
    public Unit defender;

    public int attackerHP;
    public int defenderHP;

    public int attackerDamage;

    public bool attackerSpecialTriggered;
    public bool defenderSpecialTriggered;
}
