using System.Collections.Generic;
using UnityEngine;
using Enums;

//Spawning units into the battle
public class UnitManager : MonoBehaviour
{
    public GameObject unitPrefab;

    public List<Unit> playerUnits;
    
    void Start()
    {
        playerUnits = new List<Unit>();

        // TODO: loop -> list of player units and enemy units from data then spawn
        // just spawn one unit for now
        GameObject unitGO = Instantiate(unitPrefab, unitPrefab.transform.position, Quaternion.identity, transform);

        Unit unit = unitGO.GetComponent<Unit>();
        unit.unitClass = new UnitClass(MovementType.Infantry, WeaponType.Sword, WeaponColor.Red, 2, 1); //testing only TODO
        //initialise unit properties here

        playerUnits.Add(unit);
        
    }
}
