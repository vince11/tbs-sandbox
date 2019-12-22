using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Unit HUD
    public GameObject unitHUD;
    public Image unitImage;
    public Text unitName;
    public Text hp;
    public Text atk;
    public Text spd;
    public Text def;
    public Text res;

    //Battle Log UI
    public GameObject battleLog;
    public GameObject battleLogText;
    public GameObject battleLogContentArea;

    private List<Text> texts;

    //Forecast UI
    public GameObject forecastUI;

    //Unit Editor UI
    public GameObject unitEditor;
    public List<InputField> statInputFields;

    public GameObject actionMenu;
    public GameObject sandBoxMenu;

    public EventSystem eventSystem;

    void Start()
    {
        texts = new List<Text>();
    }

    public void HideAllUI()
    {
        unitEditor.SetActive(false);
        forecastUI.SetActive(false);
        battleLog.SetActive(false);
        unitHUD.SetActive(false);
        actionMenu.SetActive(false);
    }

    public void UpdateStatsView(Unit unit)
    {
        foreach (InputField input in statInputFields)
        {
            if (input.name.Equals("CurrentHP")) input.text = unit.Stats[Stat.HP].currentValue.ToString();
            else input.text = unit.Stats[(Stat)System.Enum.Parse(typeof(Stat), input.name)].baseValue.ToString();
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

    public void LogBattle(List<CombatData> combatDatas)
    {
        GameObject textGO;
        Text t;
        int i = 0;

        foreach (CombatData fData in combatDatas)
        {
            if (i < texts.Count)
            {
                textGO = Instantiate(battleLogText, battleLogContentArea.transform);
                t = textGO.GetComponent<Text>();
                t.text = "";
                texts.Add(t);
            }
            else
            {
                texts[i].text = "";
            }

            i++;
        }

        for (int j = i; j < texts.Count; j++)
        {
            Destroy(texts[j].gameObject);
            texts.Remove(texts[j]);
        }
    }

    public void UpdateUnitHUD(Unit unit)
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
