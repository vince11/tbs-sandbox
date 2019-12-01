using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string name;
    public string spritePath;
    public string baseClass;
    public Stats maxStats;

    [System.NonSerialized]
    public Sprite sprite;

    public void LoadSprite()
    {
        sprite = Resources.Load<Sprite>(spritePath);
    }
}
