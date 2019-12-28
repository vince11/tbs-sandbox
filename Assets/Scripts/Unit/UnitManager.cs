using System.Collections.Generic;
using UnityEngine;
using Enums;

//Spawning units into the battle
public class UnitManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public List<string> unitNames;

    [System.NonSerialized]
    public List<Unit> playerUnits;
    
    void Awake()
    {
        playerUnits = new List<Unit>();

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
}
