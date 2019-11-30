[System.Serializable]
public class UnitStat
{
    public int baseValue;
    public int currentValue;

    [System.NonSerialized]
    public int buff; // visible stat bonus, can be positive or negative, only highest applied, update on turn start

    [System.NonSerialized]
    public int debuff; // visible stat reduction, only highest applied, update on turn start

    [System.NonSerialized]
    public int inCombatBonus; // can stack, can become negative

    [System.NonSerialized]
    public bool useBuff;

    [System.NonSerialized]
    public bool useDebuff;

    public int GetCombatValue()
    {
        int buffValue = useBuff ? buff : 0;
        int debuffValue = useDebuff ? debuff : 0;

        return currentValue + inCombatBonus + buffValue + debuffValue;
    }

    public void ResetVisibleModifiers()
    {
        buff = 0;
        debuff = 0;
    }

    public void ResetCombatModifiers()
    {
        inCombatBonus = 0;
        useBuff = true;
        useDebuff = true;
    }
}
