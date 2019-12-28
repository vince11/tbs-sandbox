using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BattleState : State
{
    protected BattleStateMachine bsm;
    protected int currentIndex;
    protected List<CombatData> combatDatas;

    protected GridManager Grid { get { return bsm.grid; } }
    protected Selector Selector { get { return bsm.grid.selector; } }
    protected InputManager InputManager { get { return bsm.inputManager; } }
    protected UIManager UIManager { get { return bsm.uiManager; } }
    protected CombatManager CombatManager { get { return bsm.combatManager; } }

    protected Node SelectedNode {get { return bsm.selectedNode; } set { bsm.selectedNode = value; }}
    protected Node DestinationNode { get { return bsm.destinationNode; } set { bsm.destinationNode = value; } }

    public void Awake()
    {
        bsm = FindObjectOfType<BattleStateMachine>();
        currentIndex = -1;
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
        InputManager.onEditPressed = () => bsm.ChangeState<UnitEditState>();
    }

    protected virtual void RemoveDelegates()
    {
        InputManager.onGridMovement = null;
        InputManager.onSelect = null;
        InputManager.onCancel = null;
        InputManager.onEditPressed = null;
    }
}
