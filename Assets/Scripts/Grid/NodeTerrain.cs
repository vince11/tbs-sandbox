using System.Collections.Generic;
using Enums;

[System.Serializable]
public class NodeTerrain
{
    public string terrainName;
    public TerrainCosts costs;

    private Dictionary<MovementType, int> terrainCosts = new Dictionary<MovementType, int>();

    [System.Serializable]
    public class TerrainCosts
    {
        public int infantry;
        public int armor;
        public int cavalry;
        public int flier;
    }

    public void Initialise()
    {
        terrainCosts.Add(MovementType.Infantry, costs.infantry);
        terrainCosts.Add(MovementType.Armor, costs.armor);
        terrainCosts.Add(MovementType.Cavalry, costs.cavalry);
        terrainCosts.Add(MovementType.Flier, costs.flier);
    }

    public int GetCost(MovementType moveType)
    {
        return terrainCosts[moveType];
    }

    public TerrainType GetTerrainType()
    {
        return (TerrainType)System.Enum.Parse(typeof(TerrainType), terrainName);
    }
}
