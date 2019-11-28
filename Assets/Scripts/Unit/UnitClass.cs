using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[System.Serializable]
public class UnitClass
{
    public MovementType movementType;
    public WeaponType weaponType;
    public WeaponColor weaponColor;
    public int movementRange;
    public int attackRange;

    public UnitClass(MovementType movementType, WeaponType weaponType, WeaponColor weaponColor, int movementRange, int attackRange)
    {
        this.movementType = movementType;
        this.weaponType = weaponType;
        this.weaponColor = weaponColor;
        this.movementRange = movementRange;
        this.attackRange = attackRange;
    }
}
