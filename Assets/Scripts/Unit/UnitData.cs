using System.Collections.Generic;
using Enums;

public class UnitData
{
    public CharacterData characterData;
    public UnitClass unitClass;

    public Dictionary<Stat, UnitStat> stats;

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

    public int specialCooldown;
}
