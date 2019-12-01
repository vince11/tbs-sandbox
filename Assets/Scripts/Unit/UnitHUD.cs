using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUD : MonoBehaviour
{
    public Image unitImage;
    public Text unitName;
    public Text hp;
    public Text atk;
    public Text spd;
    public Text def;
    public Text res;

    public void UpdateHUD(Unit unit)
    {
        unitImage.sprite = unit.Sprite;
        unitName.text = unit.Name;
        hp.text = unit.HP.currentValue + " / " + unit.HP.baseValue;
        atk.text = "Atk: " + unit.Attack.currentValue;
        spd.text = "Spd: " + unit.Speed.currentValue;
        def.text = "Def: " + unit.Defense.currentValue;
        res.text = "Res: " + unit.Resistance.currentValue;
    }

}
