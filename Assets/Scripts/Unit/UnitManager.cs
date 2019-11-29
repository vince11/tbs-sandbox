using System.Collections.Generic;
using UnityEngine;
using Enums;

//Spawning units into the battle
public class UnitManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public List<UnitClass> testClasses;

    [System.NonSerialized]
    public List<Unit> playerUnits;
    
    void Awake()
    {
        playerUnits = new List<Unit>();

        foreach(UnitClass uClass in testClasses)
        {
            GameObject unitGO = Instantiate(unitPrefab, unitPrefab.transform.position, Quaternion.identity, transform);

            if (uClass.movementType == MovementType.Flier) unitGO.GetComponent<Renderer>().material.color = Color.green;
            else if (uClass.movementType == MovementType.Armor) unitGO.GetComponent<Renderer>().material.color = Color.black;
            else if (uClass.movementType == MovementType.Cavalry) unitGO.GetComponent<Renderer>().material.color = Color.blue;

            Unit unit = unitGO.GetComponent<Unit>();
            unit.unitClass = uClass;

            playerUnits.Add(unit);
        }
    }
}
