using UnityEngine;

public class UnitSelectionState : State
{
    private BattleStateMachine bsm;

    public void Start()
    {
        bsm = FindObjectOfType<BattleStateMachine>();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ENTER unit selection");
    }

    public override void Execute()
    {
        Debug.Log("EXECUTE unit selection");
        bsm.ChangeState<UnitSelectedState>();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT unit selection");
    }
}
