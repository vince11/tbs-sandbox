using UnityEngine;

public class ChooseActionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        InputManager.onWaitPressed = OnWaitPressed;
        ActionMenu.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        InputManager.onWaitPressed = null;
        ActionMenu.SetActive(false);
        if(SelectedNode.unit != null)
        {
            SelectedNode.unit.transform.position = new Vector3(SelectedNode.worldPos.x, SelectedNode.unit.transform.position.y, SelectedNode.worldPos.z);
        }
    }

    public override void OnCancel()
    {
        bsm.ChangeState<UnitSelectedState>();
    }

    public void OnWaitPressed()
    {
        Unit unit = SelectedNode.unit;
        SelectedNode.unit = null;
        unit.node = DestinationNode;
        DestinationNode.unit = unit;

        Grid.ClearArrows();
        bsm.ChangeState<UnitSelectionState>();
    }
}
