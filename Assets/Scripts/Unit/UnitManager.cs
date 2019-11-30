using System.Collections.Generic;
using UnityEngine;
using Enums;

//Spawning units into the battle
public class UnitManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public List<UnitData> testUnits;

    [System.NonSerialized]
    public List<Unit> playerUnits;
    
    void Awake()
    {
        playerUnits = new List<Unit>();

        foreach(UnitData unitData in testUnits)
        {
            GameObject unitGO = Instantiate(unitPrefab, unitPrefab.transform.position, Quaternion.identity, transform);

            if (unitData.unitClass.movementType == MovementType.Flier) unitGO.GetComponent<Renderer>().material.color = Color.green;
            else if (unitData.unitClass.movementType == MovementType.Armor) unitGO.GetComponent<Renderer>().material.color = Color.black;
            else if (unitData.unitClass.movementType == MovementType.Cavalry) unitGO.GetComponent<Renderer>().material.color = Color.blue;

            Unit unit = unitGO.GetComponent<Unit>();
            unit.unitData = unitData;
            unit.specialCooldown = unit.OriginalSpecialCD;

            playerUnits.Add(unit);
        }
    }
}
