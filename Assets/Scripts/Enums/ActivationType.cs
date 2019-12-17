namespace Enums
{
    public enum ActivationType
    {
        OnEquip,

        OnAssist,
        OnTurnStart,
        PostCombat,
        PostAssist,
        OnUnitSelected,

        InCombat,
        InCombatSupport, //effects for supporting allies
        InCombatDebuff, //effects for debuffing enemies
        PreDamage,
        PostDamage
    }
}
