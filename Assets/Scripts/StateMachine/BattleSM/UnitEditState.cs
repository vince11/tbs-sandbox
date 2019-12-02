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
    }

    public override void Exit()
    {
        base.Exit();
        UnitEditor.SetActive(false);
        SandBoxMenu.SetActive(true);
    }

    public override void OnGridMovement(int index)
    {
        if (index != currentIndex)
        {
            Selector.MoveTo(Grid.nodes[index].worldPos);
            currentIndex = index;
        }
    }

    public override void OnSelect()
    {
        if (Grid.nodes[currentIndex].unit != null)
        {
            UnitEditor.SetActive(true);
            InputManager.onGridMovement = null;
            InputManager.onSelect = null;
        }
    }

    public override void OnCancel()
    {
        bsm.ChangeState<UnitSelectionState>();
    }
}
