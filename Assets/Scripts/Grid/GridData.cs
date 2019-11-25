using Enums;

[System.Serializable]
public class GridData
{
    public string mapName;
    public int width;
    public int height;
    public int[] startPositions;
    public string[] terrains;
    public string mapSprite;

    public TerrainType GetTerrainAt(int index)
    {
        return (TerrainType)System.Enum.Parse(typeof(TerrainType), terrains[index]);
    }
}
