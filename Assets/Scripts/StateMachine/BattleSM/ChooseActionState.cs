using System.Collections.Generic;
using UnityEngine;

public class ChooseActionState : State
{
    public void Awake()
    {
        bsm = FindObjectOfType<BattleStateMachine>();
    }

    public override void Enter()
    {
        bsm.inputManager.onWaitPressed = OnWaitPressed;
        bsm.inputManager.onCancel = OnCancel;
        bsm.battleMenu.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        bsm.inputManager.onWaitPressed = null;
        bsm.inputManager.onCancel = null;
        
        bsm.battleMenu.gameObject.SetActive(false);
    }

    public override void OnCancel()
    {
        bsm.ChangeState<UnitSelectedState>();
    }

    public void OnWaitPressed()
    {
        Unit unit = bsm.selectedNode.unit;
        bsm.selectedNode.unit = null;
        unit.node = bsm.destinationNode;
        bsm.destinationNode.unit = unit;

        bsm.ChangeState<UnitSelectionState>();
    }
}
