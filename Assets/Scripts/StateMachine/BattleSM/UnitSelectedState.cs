using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedState : BattleState
{
    public Unit unit;
    public List<Node> path;

    public override void Enter()
    {
        base.Enter();
        unit = SelectedNode.unit;
        unit.transform.position = new Vector3(SelectedNode.worldPos.x, unit.transform.position.y, SelectedNode.worldPos.z);
        unit.movementNodes = Grid.GetMovementNodes(SelectedNode);
        unit.attackNodes = new List<Node>();
        foreach(Node mNode in unit.movementNodes)
        {
            List<Node> attackNodes = Grid.GetAttackNodes(mNode, unit);
            foreach(Node aNode in attackNodes)
            {
                if (!unit.movementNodes.Contains(aNode) && !unit.attackNodes.Contains(aNode)) unit.attackNodes.Add(aNode);
            }
        }

        Grid.Highlight(unit.movementNodes, 1);
        Grid.Highlight(unit.attackNodes, 2);
    }

    public override void OnGridMovement(int index)
    {
        if (index != currentIndex && unit.movementNodes.Contains(Grid.nodes[index]))
        {
            Grid.ClearArrows();
            Selector.MoveTo(Grid.nodes[index].worldPos);
            path = Grid.GetPath(Grid.nodes[index]);
            Grid.DrawArrow(path);
            currentIndex = index;
        }
    }

    public override void OnSelect()
    {
        if(Grid.nodes[currentIndex].unit == null || Grid.nodes[currentIndex] == SelectedNode)
        {
            DestinationNode = Grid.nodes[currentIndex];
            if (DestinationNode == SelectedNode) bsm.ChangeState<ChooseActionState>();
            else bsm.ChangeState<UnitMovementState>();
        }
    }

    public override void OnCancel()
    {
        Grid.ClearArrows();
        bsm.ChangeState<UnitSelectionState>();
    }

    public override void Exit()
    {
        base.Exit();
        Grid.Highlight(unit.movementNodes, 0);
        Grid.Highlight(unit.attackNodes, 0);
    }
}
