using System.Collections.Generic;
using UnityEngine;

public class GameManager : ISingleton<GameManager>
{
    void Awake()
    {
        InitialiseDatabases();
    }

    private void InitialiseDatabases()
    {

    }
}
