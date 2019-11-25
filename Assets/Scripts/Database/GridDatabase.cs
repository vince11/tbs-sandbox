using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridDatabase
{
    private Dictionary<string, GridData> grids;

    public GridDatabase()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Databases/grids.json");
        string data = File.ReadAllText(path);

        DatabaseWrapper<GridData> db = JsonUtility.FromJson<DatabaseWrapper<GridData>>(data);

        grids = new Dictionary<string, GridData>();

        foreach (GridData grid in db.entries)
        {
            grids.Add(grid.mapName, grid);
        }
    }

    public GridData GetGridData(string mapName)
    {
        return grids[mapName];
    }
}
