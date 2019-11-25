using System.Collections.Generic;
using UnityEngine;

//Spawning units into the battle
public class UnitManager : MonoBehaviour
{
    public GameObject unitPrefab;

    private List<Unit> playerUnits;
    
    void Start()
    {
        playerUnits = new List<Unit>();

        // TODO: loop -> list of player units and enemy units from data then spawn
        // just spawn one unit for now
        GameObject unitGO = Instantiate(unitPrefab, Vector3.zero, Quaternion.identity, transform);

        Unit unit = unitGO.GetComponent<Unit>();
        //initialise unit properties here

        playerUnits.Add(unit);
        
    }
}
