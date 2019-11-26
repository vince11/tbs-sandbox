using UnityEngine;

public class UnitSelectionState : State
{
    public void Awake()
    {
        bsm = FindObjectOfType<BattleStateMachine>();
    }

    public override void Enter()
    {
        bsm.inputManager.onGridMovement = OnGridMovement;
        bsm.inputManager.onSelect = OnSelect;
    }

    public override void OnGridMovement(int index)
    {
        if(index != currentIndex)
        {
            bsm.grid.selector.MoveTo(bsm.grid.nodes[index].worldPos);
            currentIndex = index;
        }
        
    }

    public override void OnSelect()
    {
        if(bsm.grid.nodes[currentIndex].unit != null)
        {
            bsm.selectedNode = bsm.grid.nodes[currentIndex];
            bsm.ChangeState<UnitSelectedState>();
        }
    }

    public override void Exit()
    {
        bsm.inputManager.onGridMovement = null;
        bsm.inputManager.onSelect = null;
    }
}
