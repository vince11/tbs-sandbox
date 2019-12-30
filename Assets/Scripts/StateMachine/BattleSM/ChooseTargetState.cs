using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTargetState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Grid.Highlight(SelectedNode.unit.attackNodes, 2);
    }

    public override void Exit()
    {
        base.Exit();
        Grid.Highlight(SelectedNode.unit.attackNodes, 0);
    }

    public override void OnCancel()
    {
        bsm.ChangeState<ChooseActionState>();
    }

    public override void OnEscape()
    {
        Grid.ClearArrows();
        SelectedNode.unit.transform.position = new Vector3(SelectedNode.worldPos.x, SelectedNode.unit.transform.position.y, SelectedNode.worldPos.z);
        base.OnEscape();
    }

    public override void OnMouseMovement(int index)
    {
        if (index != Selector.index && SelectedNode.unit.attackNodes.Contains(Grid.nodes[index]) && Grid.nodes[index].unit != null && Grid.nodes[index] != SelectedNode)
        {
            Selector.MoveTo(Grid.nodes[index].worldPos);
            Selector.index = index;

            CombatDatas = CombatManager.Combat(SelectedNode.unit, Grid.nodes[index].unit);
            UIManager.forecastUI.SetActive(true);

        }
        else UIManager.forecastUI.SetActive(false);
    }

    public override void OnSelect()
    {
        if (Grid.nodes[Selector.index].unit != null)
        {
            bsm.ChangeState<CombatState>();
        }
    }
}
