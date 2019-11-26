using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedState : State
{
    public Unit unit;
    public List<Node> path;

    public void Awake()
    {
        bsm = FindObjectOfType<BattleStateMachine>();
    }

    public override void Enter()
    {
        unit = bsm.selectedNode.unit;
        unit.transform.position = new Vector3(bsm.selectedNode.worldPos.x, unit.transform.position.y, bsm.selectedNode.worldPos.z);
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

        bsm.grid.arrow.gameObject.SetActive(true);

        bsm.inputManager.onGridMovement = OnGridMovement;
        bsm.inputManager.onSelect = OnSelect;
        bsm.inputManager.onCancel = OnCancel;
    }

    public override void OnGridMovement(int index)
    {
        if (index != currentIndex && unit.movementNodes.Contains(bsm.grid.nodes[index]))
        {
            bsm.grid.selector.MoveTo(bsm.grid.nodes[index].worldPos);
            path = bsm.grid.GetPath(bsm.grid.nodes[index]);
            bsm.grid.DrawArrow(path);
            currentIndex = index;
        }
    }

    public override void OnSelect()
    {
        bsm.destinationNode = bsm.grid.nodes[currentIndex];
        if(bsm.destinationNode == bsm.selectedNode) bsm.ChangeState<ChooseActionState>();
        else bsm.ChangeState<UnitMovementState>();
    }

    public override void OnCancel()
    {
        bsm.ChangeState<UnitSelectionState>();
    }

    public override void Exit()
    {
        bsm.grid.Highlight(unit.movementNodes, 0);
        bsm.grid.Highlight(unit.attackNodes, 0);

        bsm.grid.arrow.positionCount = 0;
        bsm.grid.arrow.gameObject.SetActive(false);

        bsm.inputManager.onGridMovement = null;
        bsm.inputManager.onSelect = null;
        bsm.inputManager.onCancel = null;
    }
}
