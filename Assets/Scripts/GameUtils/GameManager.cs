using System.Collections.Generic;
using UnityEngine;

public class GameManager : ISingleton<GameManager>
{
    public CharacterDatabase characters;
    public UnitClassDatabase unitClasses;

    void Awake()
    {
        InitialiseDatabases();
    }

    private void InitialiseDatabases()
    {
        characters = new CharacterDatabase();
        unitClasses = new UnitClassDatabase();
    }
}
