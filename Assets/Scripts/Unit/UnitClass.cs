using Enums;

[System.Serializable]
public class UnitClass
{
    public string name;
    public string moveType;
    public string weaponColor;
    public string weaponType;
    public int moveRange;
    public int attackRange;
    
    public MovementType MoveType { get { return (MovementType) System.Enum.Parse(typeof(MovementType), moveType); } }
    public WeaponType WeaponType { get { return (WeaponType)System.Enum.Parse(typeof(WeaponType), weaponType); } }
    public WeaponColor WeaponColor { get { return (WeaponColor)System.Enum.Parse(typeof(WeaponColor), weaponColor); } }
}
