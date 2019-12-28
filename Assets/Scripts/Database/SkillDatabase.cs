using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SkillDatabase
{
    private Dictionary<string, Skill> skills;
    private SkillEffectDatabase effectsDB;

    [System.Serializable]
    public class SkillData
    {
        public string name;
        public string description;
        public string type;
        public int priority;

        //weapon properties
        public int might;
        public string targetStat;

        //special skill properties
        public int cooldown;
        public bool isDefensive;

        //assist skill properties
        public int range;
        public string assistType;

        public string[] effects;
    }

    public SkillDatabase()
    {
        effectsDB = new SkillEffectDatabase();
        skills = new Dictionary<string, Skill>();
        LoadSkills(Path.Combine(Application.streamingAssetsPath, "Databases/Skills/Weapons/swords.json"));
    }

    private void LoadSkills(string path)
    {
        string data = File.ReadAllText(path);

        DatabaseWrapper<SkillData> db = JsonUtility.FromJson<DatabaseWrapper<SkillData>>(data);

        foreach (SkillData skill in db.entries)
        {
            Skill newSkill = new Skill();
            newSkill.name = skill.name;
            newSkill.description = skill.description;
            Enum.TryParse(skill.type, out newSkill.type);
            newSkill.priority = skill.priority;

            newSkill.might = skill.might;
            if(skill.targetStat != null) Enum.TryParse(skill.targetStat, out newSkill.targetStat);

            newSkill.cooldown = skill.cooldown;
            newSkill.isDefensive = skill.isDefensive;

            newSkill.range = skill.range;
            if(skill.assistType != null) Enum.TryParse(skill.assistType, out newSkill.assistType);

            foreach (string effectName in skill.effects)
            {
                newSkill.effects.Add(effectsDB.GetSkillEffect(effectName));
            }

            skills.Add(skill.name, newSkill);
        }


    }

    public Skill GetSkill(string skillName)
    {
        if (skillName != null) return skills[skillName];

        return null;
    }

    public List<string> GetSkillNames(Enums.SkillType skillType)
    {
        List<string> names = skills.Values.ToList().Where(skill => skill.type == skillType).Select(skill => skill.name).ToList();
        names.Add("None");
        return names;
    }
}

