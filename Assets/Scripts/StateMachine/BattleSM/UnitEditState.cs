using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;

public class UnitEditState : BattleState
{
    private readonly List<SkillType> skillTypes = Enum.GetValues(typeof(SkillType)).Cast<SkillType>().ToList();
    private readonly List<Stat> stats = Enum.GetValues(typeof(Stat)).Cast<Stat>().ToList();

    public override void Enter()
    {
        base.Enter();
        Grid.ClearArrows();
        UIManager.sandBoxMenu.SetActive(false);
        //SelectedNode.unit.transform.position = new Vector3(SelectedNode.worldPos.x, SelectedNode.unit.transform.position.y, SelectedNode.worldPos.z);

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
        UIManager.unitEditor.SetActive(false);
        UIManager.sandBoxMenu.SetActive(true);
    }

    public override void OnGridMovement(int index)
    {
        if (index != currentIndex)
        {
            Selector.MoveTo(Grid.nodes[index].worldPos);
            currentIndex = index;

            if (Grid.nodes[index].unit != null)
            {
                UIManager.unitHUD.SetActive(true);
                UIManager.UpdateUnitHUD(Grid.nodes[index].unit);
            }
            else UIManager.unitHUD.SetActive(false);
        }
    }

    public override void OnSelect()
    {
        if (Grid.nodes[currentIndex].unit != null)
        {
            UIManager.unitEditor.SetActive(true);
            InputManager.onGridMovement = null;
            InputManager.onSelect = null;

            SelectedNode = Grid.nodes[currentIndex];
            UIManager.UpdateStatsView(Grid.nodes[currentIndex].unit);
        }
    }

    public override void OnCancel()
    {
        if (UIManager.unitEditor.activeSelf)
        {
            UIManager.unitEditor.SetActive(false);
            InputManager.onGridMovement = OnGridMovement;
            InputManager.onSelect = OnSelect;
        }
        else bsm.ChangeState<UnitSelectionState>();
    }

    private void OnStatChanged(string input, int inputFieldIndex)
    {
        Unit unit = SelectedNode.unit;
        bool isParsed = int.TryParse(input, out int value);
        if (isParsed)
        {
            if(inputFieldIndex == 0) unit.Stats[stats[0]].currentValue = value;
            else if(inputFieldIndex == 1) unit.Stats[stats[0]].baseValue = value;
            else unit.Stats[stats[inputFieldIndex - 1]].UpdateValues(value);
        }
    }

    private void OnSkillChanged(int selectedIndex, int dropdownIndex)
    {
        string selected = UIManager.skillDropdowns[dropdownIndex].options[selectedIndex].text;
        if (selected.Equals("None")) SelectedNode.unit.Skills[skillTypes[dropdownIndex]] = null;
        else SelectedNode.unit.Skills[skillTypes[dropdownIndex]] = GameManager.Instance.skillDatabase.GetSkill(selected);
    }
}
