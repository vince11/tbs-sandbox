using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Enums;

public class UnitEditor : MonoBehaviour
{
    public List<InputField> statInputFields;
    public EventSystem eventSystem;

    public void Start()
    {
        
    }

    public void UpdateStatsView(Unit unit)
    {
        foreach(InputField input in statInputFields)
        {
            if (input.name.Equals("CurrentHP")) input.text = unit.Stats[Stat.HP].currentValue.ToString();
            else input.text = unit.Stats[(Stat) System.Enum.Parse(typeof(Stat), input.name)].baseValue.ToString();
        }
    }

    public void UpdateUnitStat(Unit unit)
    {
        string stat = eventSystem.currentSelectedGameObject.name;
        bool isParsed = int.TryParse(statInputFields.Find(x => x.name == stat).text, out int value);

        if (isParsed)
        {
            if (stat.Equals("CurrentHP")) unit.Stats[Stat.HP].currentValue = value;
            else if (stat.Equals("HP")) unit.Stats[Stat.HP].baseValue = value;
            else unit.Stats[(Stat)System.Enum.Parse(typeof(Stat), stat)].UpdateValues(value);
        }
    }
}
