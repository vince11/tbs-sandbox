using UnityEngine;
using Enums;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public UnitClass unitClass;
    public Node node;
    public List<Node> movementNodes;
    public List<Node> attackNodes;

    public MovementType GetMovementType()
    {
        return unitClass.movementType;
    }

    public int GetMovementRange()
    {
        return unitClass.movementRange;
    }

    public int GetAttackRange()
    {
        return unitClass.attackRange;
    }
}
