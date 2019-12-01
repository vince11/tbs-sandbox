using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UnitClassDatabase
{
    private Dictionary<string, UnitClass> unitClasses;

    public UnitClassDatabase()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Databases/unit_classes.json");
        string data = File.ReadAllText(path);

        DatabaseWrapper<UnitClass> db = JsonUtility.FromJson<DatabaseWrapper<UnitClass>>(data);

        unitClasses = new Dictionary<string, UnitClass>();

        foreach (UnitClass uc in db.entries)
        {
            unitClasses.Add(uc.name, uc);
        }
    }

    public UnitClass GetUnitClass(string name)
    {
        return unitClasses[name];
    }
}
