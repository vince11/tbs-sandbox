using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class SkillEffectDatabase
{
    [Serializable]
    public class SkillEffectData
    {
        public string name;
        public string[] conditions;
        public string activationType;

        public string className;
        public string[] parameters;
    }

    private SkillConditionDatabase conditionsDB;
    private Dictionary<string, SkillEffect> skillEffects;

    public SkillEffectDatabase()
    {
        conditionsDB = new SkillConditionDatabase();
        skillEffects = new Dictionary<string, SkillEffect>();

        LoadEffects(Path.Combine(Application.streamingAssetsPath, "Databases/Skills/SkillEffects/equip_effects.json"));
        LoadEffects(Path.Combine(Application.streamingAssetsPath, "Databases/Skills/SkillEffects/pre_combat_effects.json"));
        LoadEffects(Path.Combine(Application.streamingAssetsPath, "Databases/Skills/SkillEffects/in_combat_effects.json"));
        LoadEffects(Path.Combine(Application.streamingAssetsPath, "Databases/Skills/SkillEffects/pre_damage_effects.json"));
        LoadEffects(Path.Combine(Application.streamingAssetsPath, "Databases/Skills/SkillEffects/post_damage_effects.json"));
        LoadEffects(Path.Combine(Application.streamingAssetsPath, "Databases/Skills/SkillEffects/post_combat_effects.json"));
    }

    public void LoadEffects(string path)
    {
        string data = File.ReadAllText(path);

        DatabaseWrapper<SkillEffectData> db = JsonUtility.FromJson<DatabaseWrapper<SkillEffectData>>(data);

        foreach (SkillEffectData s in db.entries)
        {
            SkillEffect newEffect = (SkillEffect)Activator.CreateInstance(Type.GetType(s.className), s.parameters);
            newEffect.conditions = new List<Func<Unit, Unit, BattleStateMachine, bool>>();

            Enum.TryParse(s.activationType, out Enums.ActivationType activationType);
            newEffect.activationType = activationType;

            foreach (string conditionName in s.conditions)
            {
                newEffect.conditions.Add(conditionsDB.GetSkillConditions(conditionName));
            }

            skillEffects.Add(s.name, newEffect);
        }
    }

    public SkillEffect GetSkillEffect(string effectName)
    {
        return skillEffects[effectName];
    }
}
