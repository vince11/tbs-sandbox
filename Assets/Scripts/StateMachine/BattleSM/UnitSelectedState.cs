using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedState : State
{
    public Unit unit;

    public void Awake()
    {
        bsm = FindObjectOfType<BattleStateMachine>();
    }

    public override void Enter()
    {
        base.Enter();
        unit = bsm.selectedNode.unit;
        unit.movementNodes = bsm.grid.GetMovementNodes(bsm.selectedNode);
        unit.attackNodes = new List<Node>();
        foreach(Node mNode in unit.movementNodes)
        {
            List<Node> attackNodes = bsm.grid.GetAttackNodes(mNode, unit);
            foreach(Node aNode in attackNodes)
            {
                if (!unit.movementNodes.Contains(aNode) && !unit.attackNodes.Contains(aNode)) unit.attackNodes.Add(aNode);
            }
        }

        bsm.grid.Highlight(unit.movementNodes, 1);
        bsm.grid.Highlight(unit.attackNodes, 2);

        bsm.inputManager.onGridMovement = OnGridMovement;
        bsm.inputManager.onSelect = OnSelect;
        bsm.inputManager.onCancel = OnCancel;
    }

    public override void OnGridMovement(int index)
    {
        if (unit.movementNodes.Contains(bsm.grid.nodes[index])) bsm.grid.selector.MoveTo(bsm.grid.nodes[index].worldPos);
    }

    public override void OnSelect()
    {
        bsm.battleMenu.gameObject.SetActive(true);
    }

    public override void OnCancel()
    {
        bsm.ChangeState<UnitSelectionState>();
    }

    public override void Exit()
    {
        base.Exit();
        bsm.grid.Highlight(unit.movementNodes, 0);
        bsm.grid.Highlight(unit.attackNodes, 0);
        bsm.battleMenu.gameObject.SetActive(false);

        bsm.inputManager.onGridMovement = null;
        bsm.inputManager.onSelect = null;
        bsm.inputManager.onCancel = null;
    }
}
