using System.Collections.Generic;
using UnityEngine;

public class GameManager : ISingleton<GameManager>
{
    public CharacterDatabase characters;
    public UnitClassDatabase unitClasses;
    public SkillDatabase skillDatabase;

    void Awake()
    {
        InitialiseDatabases();
    }

    private void InitialiseDatabases()
    {
        characters = new CharacterDatabase();
        unitClasses = new UnitClassDatabase();
        skillDatabase = new SkillDatabase();
    }
}
