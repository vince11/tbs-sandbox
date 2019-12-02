using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEditState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Grid.ClearArrows();
        SandBoxMenu.SetActive(false);
        InputManager.onStatEdited = OnStatEdited;
    }

    public override void Exit()
    {
        base.Exit();
        UnitEditor.gameObject.SetActive(false);
        SandBoxMenu.SetActive(true);
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
                UnitHUD.gameObject.SetActive(true);
                UnitHUD.UpdateHUD(Grid.nodes[index].unit);
            }
            else UnitHUD.gameObject.SetActive(false);
        }
    }

    public override void OnSelect()
    {
        if (Grid.nodes[currentIndex].unit != null)
        {
            UnitEditor.gameObject.SetActive(true);
            InputManager.onGridMovement = null;
            InputManager.onSelect = null;

            SelectedNode = Grid.nodes[currentIndex];
            UnitEditor.UpdateStatsView(Grid.nodes[currentIndex].unit);
        }
    }

    public override void OnCancel()
    {
        if (UnitEditor.gameObject.activeSelf)
        {
            UnitEditor.gameObject.SetActive(false);
            InputManager.onGridMovement = OnGridMovement;
            InputManager.onSelect = OnSelect;
        }
        else bsm.ChangeState<UnitSelectionState>();
    }

    public void OnStatEdited()
    {
        UnitEditor.UpdateUnitStat(SelectedNode.unit);
    }
}
