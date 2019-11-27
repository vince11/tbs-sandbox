public class ChooseActionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        InputManager.onWaitPressed = OnWaitPressed;
        BattleMenu.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        InputManager.onWaitPressed = null;
        BattleMenu.SetActive(false);
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
