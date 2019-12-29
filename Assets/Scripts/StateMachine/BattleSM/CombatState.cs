using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : BattleState
{
    public override void Enter()
    {
        base.Enter();

        Unit unit = SelectedNode.unit;
        SelectedNode.unit = null;
        unit.node = DestinationNode;
        DestinationNode.unit = unit;
        Grid.ClearArrows();

        UIManager.LogBattle(CombatDatas);
        UIManager.battleLog.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.battleLog.SetActive(false);
    }

    public override void OnCancel()
    {
        bsm.ChangeState<UnitSelectionState>();
    }
}
