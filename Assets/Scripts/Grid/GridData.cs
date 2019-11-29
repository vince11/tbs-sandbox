using Enums;
using System.Collections.Generic;

[System.Serializable]
public class GridData
{
    public string name;
    public int width;
    public int height;
    public int[] startPositions;
    public string[] map;

    [System.NonSerialized]
    public List<TerrainType> terrains;

    public void Initialise()
    {
        TerrainType t;
        terrains = new List<TerrainType>();
        for(int i = map.Length - 1; i >= 0; i--)
        {
            string[] row = map[i].Split('-');
            for(int j = 0; j < row.Length; j++)
            {
                t = (TerrainType) int.Parse(row[j]);
                terrains.Add(t);
            }
        }
    }

}
