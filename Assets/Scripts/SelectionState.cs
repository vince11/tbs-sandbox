using UnityEngine;

public class SelectionState : GameState
{
    public SelectionState(Selector selector, GridManager gridManager) : base(selector, gridManager) { }

    public override GameState HandleInput()
    {
        return null;
    }

    public override void Execute()
    {
        //throw new System.NotImplementedException();
    }
}
