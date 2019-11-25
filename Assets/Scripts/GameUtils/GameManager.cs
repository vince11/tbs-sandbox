using System.Collections.Generic;
using UnityEngine;

public class GameManager : ISingleton<GameManager>
{
    public TerrainDatabase terrainsDB;

    void Awake()
    {
        InitialiseDatabases();
    }

    private void InitialiseDatabases()
    {
        terrainsDB = new TerrainDatabase();
    }
}
