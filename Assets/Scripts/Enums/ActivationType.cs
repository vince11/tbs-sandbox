namespace Enums
{
    public enum ActivationType
    {
        OnEquip,
        OnUnEquip,

        OnAssist,
        OnTurnStart,
        PostCombat,
        PostAssist,
        OnUnitSelected,

        PreCombat,
        InCombat,
        CombatSupport, //effects for supporting allies
        CombatDebuff, //effects for debuffing enemies
        PreDamage,
        PostDamage
    }
}
