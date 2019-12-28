﻿using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<Dropdown> skillDropdowns;

    public GameObject actionMenu;
    public GameObject sandBoxMenu;

    public EventSystem eventSystem;

    void Start()
    {
        texts = new List<Text>();
        List<SkillType> skillTypes = Enum.GetValues(typeof(SkillType)).Cast<SkillType>().ToList();
        for (int i = 0; i < skillDropdowns.Count; i++)
        {
            skillDropdowns[i].AddOptions(GameManager.Instance.skillDatabase.GetSkillNames(skillTypes[i]));
        }
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

        List<SkillType> skillTypes = Enum.GetValues(typeof(SkillType)).Cast<SkillType>().ToList();

        for (int i = 0; i < skillDropdowns.Count; i++)
        {
            if(unit.Skills[skillTypes[i]] != null)
            {
                skillDropdowns[i].value = skillDropdowns[i].options.FindIndex(option => option.text.Equals(unit.Skills[skillTypes[i]].name));
            }
            else
            {
                skillDropdowns[i].value = skillDropdowns[i].options.Count - 1;
            }
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
