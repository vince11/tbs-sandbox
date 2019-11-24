using UnityEngine;

public class UnitSelectedState : State
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("ENTER unit selected");
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT unit selected");
    }
}
