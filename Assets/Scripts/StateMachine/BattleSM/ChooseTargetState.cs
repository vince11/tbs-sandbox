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

    public override void OnGridMovement(int index)
    {
        if (index != currentIndex && SelectedNode.unit.attackNodes.Contains(Grid.nodes[index]) && Grid.nodes[index].unit != null && Grid.nodes[index] != SelectedNode)
        {
            Selector.MoveTo(Grid.nodes[index].worldPos);
            currentIndex = index;

            CombatDatas = CombatManager.Combat(SelectedNode.unit, Grid.nodes[index].unit);
            UIManager.forecastUI.SetActive(true);

        }
        else UIManager.forecastUI.SetActive(false);
    }

    public override void OnSelect()
    {
        if (Grid.nodes[currentIndex].unit != null)
        {
            bsm.ChangeState<CombatState>();
        }
    }
}
