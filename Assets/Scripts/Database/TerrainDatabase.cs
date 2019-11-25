using System.Collections.Generic;
using Enums;
using System.IO;
using UnityEngine;

public class TerrainDatabase
{
    private Dictionary<TerrainType, NodeTerrain> terrains;

    public TerrainDatabase()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Databases/terrains.json");
        string data = File.ReadAllText(path);

        DatabaseWrapper<NodeTerrain> db = JsonUtility.FromJson<DatabaseWrapper<NodeTerrain>>(data);

        terrains = new Dictionary<TerrainType, NodeTerrain>();

        foreach(NodeTerrain terrain in db.entries)
        {
            terrain.Initialise();
            terrains.Add((TerrainType)System.Enum.Parse(typeof(TerrainType), terrain.terrainName), terrain);
        }
    }

    public NodeTerrain GetTerrain(TerrainType terrainType)
    {
        return terrains[terrainType];
    }
}
