using UnityEngine;

public class ChooseActionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        UIManager.waitButton.onClick.AddListener(OnWaitPressed);
        UIManager.attackButton.onClick.AddListener(OnAttackPressed);
        UIManager.actionMenu.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.waitButton.onClick.RemoveListener(OnWaitPressed);
        UIManager.attackButton.onClick.RemoveListener(OnAttackPressed);
        UIManager.actionMenu.SetActive(false);
    }

    public override void OnCancel()
    {
        SelectedNode.unit.transform.position = new Vector3(SelectedNode.worldPos.x, SelectedNode.unit.transform.position.y, SelectedNode.worldPos.z);
        bsm.ChangeState<UnitSelectedState>();
    }

    public override void OnEscape()
    {
        Grid.ClearArrows();
        SelectedNode.unit.transform.position = new Vector3(SelectedNode.worldPos.x, SelectedNode.unit.transform.position.y, SelectedNode.worldPos.z);
        base.OnEscape();
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

    public void OnAttackPressed()
    {
        SelectedNode.unit.attackNodes = bsm.grid.GetAttackNodes(DestinationNode, SelectedNode.unit);
        if(SelectedNode.unit.attackNodes.Exists((node) => node != SelectedNode && node.unit != null)) bsm.ChangeState<ChooseTargetState>();
    }
}
