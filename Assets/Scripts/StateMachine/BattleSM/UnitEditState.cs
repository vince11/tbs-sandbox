using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEditState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Grid.ClearArrows();
        UIManager.sandBoxMenu.SetActive(false);
        InputManager.onStatEdited = OnStatEdited;
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.unitEditor.SetActive(false);
        UIManager.sandBoxMenu.SetActive(true);
        InputManager.onStatEdited = null;
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

    public void OnStatEdited()
    {
        UIManager.UpdateUnitStat(SelectedNode.unit);
    }
}
