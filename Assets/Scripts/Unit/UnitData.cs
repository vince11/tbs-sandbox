using System.Collections.Generic;
using Enums;

public class UnitData
{
    public CharacterData characterData;
    public UnitClass unitClass;

    public Dictionary<Stat, UnitStat> stats;
    public Dictionary<SkillType, Skill> skills;
    public int specialCooldown;

    public void InitialiseStats(Stats currentStats)
    {
        stats = new Dictionary<Stat, UnitStat>
        {
            { Stat.HP, new UnitStat(currentStats.hp) },
            { Stat.Attack, new UnitStat(currentStats.attack) },
            { Stat.Speed, new UnitStat(currentStats.speed) },
            { Stat.Defense, new UnitStat(currentStats.defense) },
            { Stat.Resistance, new UnitStat(currentStats.resistance) }
        };
    }

    public void InitialiseSkills(string weapon, string assist, string special, string a, string b, string c, string seal)
    {
        skills = new Dictionary<SkillType, Skill>
        {
            { SkillType.Weapon, GameManager.Instance.skillDatabase.GetSkill(weapon)},
            { SkillType.Assist, GameManager.Instance.skillDatabase.GetSkill(assist)},
            { SkillType.Special, GameManager.Instance.skillDatabase.GetSkill(special)},
            { SkillType.A, GameManager.Instance.skillDatabase.GetSkill(a)},
            { SkillType.B, GameManager.Instance.skillDatabase.GetSkill(b)},
            { SkillType.C, GameManager.Instance.skillDatabase.GetSkill(c)},
            { SkillType.Seal, GameManager.Instance.skillDatabase.GetSkill(seal)}
        };
    }
}
