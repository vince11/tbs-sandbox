using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected BattleStateMachine bsm;
    protected int currentIndex;

    public virtual void Enter()
    {

    }

    public virtual void OnGridMovement(int index)
    {
        
    }

    public virtual void OnSelect()
    {

    }

    public virtual void OnCancel()
    {

    }

    public virtual void Execute()
    {

    }

    public virtual void Exit()
    {

    }
}
