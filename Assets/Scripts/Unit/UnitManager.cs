using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq;

//Spawning units into the battle
public class UnitManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public List<string> unitNames;

    public GridManager grid;

    [System.NonSerialized]
    public List<Unit> playerUnits;

    [System.NonSerialized]
    public List<Unit> enemyUnits;

    void Awake()
    {
        grid = FindObjectOfType<GridManager>();

        playerUnits = new List<Unit>();
        enemyUnits = new List<Unit>();

        GameObject unitGO;
        Unit unit;
        CharacterData character;
        UnitData unitData;

        foreach (string name in unitNames)
        {
            unitGO = Instantiate(unitPrefab, unitPrefab.transform.position, Quaternion.identity, transform);

            unit = unitGO.GetComponent<Unit>();

            unitData = new UnitData();
            character = GameManager.Instance.characters.GetCharacterData(name);
            character.LoadSprite();
            unitData.characterData = character;
            unitData.unitClass = GameManager.Instance.unitClasses.GetUnitClass(character.baseClass);
            unitData.InitialiseStats(character.maxStats);
            unitData.InitialiseSkills(null, null, null, null, null, null, null);

            unit.unitData = unitData;
            //unit.specialCooldown = unit.OriginalSpecialCD;

            playerUnits.Add(unit);
        }
    }

    public string GetTeam(Unit unit)
    {
        if (playerUnits.Contains(unit)) return "Offense";
        else return "Defense";
    }

    public List<Unit> GetAllies(Unit unit, int range)
    {
        if (playerUnits.Contains(unit)) return playerUnits.Where((x) => grid.GetDistance(x.node, unit.node) == range).ToList();
        else return enemyUnits.Where((x) => grid.GetDistance(x.node, unit.node) == range).ToList();
    }

    public List<Unit> GetFoes(Unit unit, int range)
    {
        if (!playerUnits.Contains(unit)) return playerUnits.Where((x) => grid.GetDistance(x.node, unit.node) == range).ToList();
        else return enemyUnits.Where((x) => grid.GetDistance(x.node, unit.node) == range).ToList();
    }
}
