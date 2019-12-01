using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterDatabase
{
    private Dictionary<string, CharacterData> allCharacters;

    public CharacterDatabase()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Databases/characters.json");
        string data = File.ReadAllText(path);

        DatabaseWrapper<CharacterData> db = JsonUtility.FromJson<DatabaseWrapper<CharacterData>>(data);

        allCharacters = new Dictionary<string, CharacterData>();

        foreach (CharacterData c in db.entries)
        {
            allCharacters.Add(c.name, c);
        }
    }

    public CharacterData GetCharacterData(string name)
    {
        return allCharacters[name];
    }
}
