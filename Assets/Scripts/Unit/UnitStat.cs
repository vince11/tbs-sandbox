[System.Serializable]
public class UnitStat
{
    public int baseValue;
    public int currentValue;

    public int buff; // visible stat bonus, can be positive or negative, only highest applied, update on turn start
    public int debuff; // visible stat reduction, only highest applied, update on turn start
    public int inCombatBonus; // can stack, can become negative

    public bool useBuff;
    public bool useDebuff;

    public UnitStat(int startValue, int extraValue)
    {
        baseValue = startValue + extraValue;
        currentValue = baseValue;
    }

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
