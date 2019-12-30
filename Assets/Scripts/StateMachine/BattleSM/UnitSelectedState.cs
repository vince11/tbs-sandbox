using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedState : BattleState
{
    private Unit unit;
    private List<Node> path;

    public override void Enter()
    {
        base.Enter();
        unit = SelectedNode.unit;
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

    public override void Exit()
    {
        base.Exit();
        Grid.Highlight(unit.movementNodes, 0);
        Grid.Highlight(unit.attackNodes, 0);
    }

    public override void OnMouseMovement(int index)
    {
        if (index != Selector.index && unit.movementNodes.Contains(Grid.nodes[index]))
        {
            Grid.ClearArrows();
            Selector.MoveTo(Grid.nodes[index].worldPos);
            path = Grid.GetPath(Grid.nodes[index]);
            Grid.DrawArrow(path);
            Selector.index = index;
        }
    }

    public override void OnSelect()
    {
        if (Grid.nodes[Selector.index].unit == null)
        {
            DestinationNode = Grid.nodes[Selector.index];
            bsm.ChangeState<UnitMovementState>();
        }
        else if (Grid.nodes[Selector.index] == SelectedNode)
        {
            DestinationNode = Grid.nodes[Selector.index];
            bsm.ChangeState<ChooseActionState>();
        }
    }

    public override void OnCancel()
    {
        Grid.ClearArrows();
        bsm.ChangeState<UnitSelectionState>();
    }

    public override void OnEscape()
    {
        Grid.ClearArrows();
        base.OnEscape();
    }
}
