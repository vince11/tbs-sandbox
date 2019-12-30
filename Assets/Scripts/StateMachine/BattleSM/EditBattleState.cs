using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;

public class EditBattleState : BattleState
{
    private readonly List<SkillType> skillTypes = Enum.GetValues(typeof(SkillType)).Cast<SkillType>().ToList();
    private readonly List<Stat> stats = Enum.GetValues(typeof(Stat)).Cast<Stat>().ToList();

    public override void Enter()
    {
        base.Enter();

        for (int i = 0; i < UIManager.skillDropdowns.Count; i++)
        {
            int index = i;
            UIManager.skillDropdowns[i].onValueChanged.AddListener((option) => OnSkillChanged(option, index));
        }

        for (int i = 0; i < UIManager.statInputFields.Count; i++)
        {
            int index = i;
            UIManager.statInputFields[i].onEndEdit.AddListener((input) => OnStatChanged(input, index));
        }
    }

    public override void Exit()
    {
        base.Exit();

        for (int i = 0; i < UIManager.skillDropdowns.Count; i++)
        {
            UIManager.skillDropdowns[i].onValueChanged.RemoveAllListeners();
        }

        for (int i = 0; i < UIManager.statInputFields.Count; i++)
        {
            UIManager.statInputFields[i].onEndEdit.RemoveAllListeners();
        }
    }

    public override void OnMouseMovement(int index)
    {
        if (index != Selector.index)
        {
            Selector.MoveTo(Grid.nodes[index].worldPos);
            Selector.index = index;

            DisplayUnitHUD(Grid.nodes[index].unit);
        }
    }

    public override void OnSelect()
    {
        if (Grid.nodes[Selector.index].unit != null)
        {
            SelectedNode = Grid.nodes[Selector.index];
            UIManager.UpdateUnitEditor(SelectedNode.unit);
            UIManager.unitEditor.SetActive(true);

            InputManager.onMouseMovement = null;
            InputManager.onSelect = null;
        }
    }

    public override void OnCancel()
    {
        if (UIManager.unitEditor.activeSelf)
        {
            UIManager.unitEditor.SetActive(false);
            InputManager.onMouseMovement = OnMouseMovement;
            InputManager.onSelect = OnSelect;
        }
    }

    public override void OnReturn()
    {
        if (!UIManager.unitEditor.activeSelf) bsm.ChangeState<UnitSelectionState>();
    }

    public override void OnEscape()
    {

    }

    private void OnStatChanged(string input, int inputFieldIndex)
    {
        bool isParsed = int.TryParse(input, out int value);
        if (isParsed)
        {
            if(inputFieldIndex == 0) SelectedNode.unit.Stats[stats[0]].currentValue = value;
            else if(inputFieldIndex == 1) SelectedNode.unit.Stats[stats[0]].baseValue = value;
            else SelectedNode.unit.Stats[stats[inputFieldIndex - 1]].UpdateValues(value);
        }
    }

    private void OnSkillChanged(int selectedIndex, int dropdownIndex)
    {
        string skill = UIManager.skillDropdowns[dropdownIndex].options[selectedIndex].text;
        if (skill.Equals("None")) SelectedNode.unit.Skills[skillTypes[dropdownIndex]] = null;
        else SelectedNode.unit.Skills[skillTypes[dropdownIndex]] = GameManager.Instance.skillDatabase.GetSkill(skill);
    }
}
