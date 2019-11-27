using UnityEngine;

public abstract class BattleState : State
{
    protected BattleStateMachine bsm;
    protected int currentIndex;

    public GridManager Grid { get { return bsm.grid; } }
    public Selector Selector { get { return bsm.grid.selector; } }
    public InputManager InputManager { get { return bsm.inputManager; } }
    public GameObject BattleMenu { get { return bsm.battleMenu.gameObject; } }

    public Node SelectedNode {get { return bsm.selectedNode; } set { bsm.selectedNode = value; }}
    public Node DestinationNode { get { return bsm.destinationNode; } set { bsm.destinationNode = value; } }

    public void Awake()
    {
        bsm = FindObjectOfType<BattleStateMachine>();
    }

    public override void Enter()
    {
        AddDelegates();
    }

    public override void Exit()
    {
        RemoveDelegates();
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

    protected virtual void AddDelegates()
    {
        InputManager.onGridMovement = OnGridMovement;
        InputManager.onSelect = OnSelect;
        InputManager.onCancel = OnCancel;
    }

    protected virtual void RemoveDelegates()
    {
        InputManager.onGridMovement = null;
        InputManager.onSelect = null;
        InputManager.onCancel = null;
    }
}
