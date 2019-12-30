using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BattleState : State
{
    protected BattleStateMachine bsm;

    protected GridManager Grid { get { return bsm.grid; } }
    protected Selector Selector { get { return bsm.grid.selector; } }
    protected InputManager InputManager { get { return bsm.inputManager; } }
    protected UIManager UIManager { get { return bsm.uiManager; } }
    protected CombatManager CombatManager { get { return bsm.combatManager; } }

    protected Node SelectedNode {get { return bsm.selectedNode; } set { bsm.selectedNode = value; }}
    protected Node DestinationNode { get { return bsm.destinationNode; } set { bsm.destinationNode = value; } }
    protected List<CombatData> CombatDatas { get { return bsm.combatDatas; } set { bsm.combatDatas = value; } }

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

    public virtual void OnMouseMovement(int index)
    {

    }

    public virtual void OnSelect()
    {

    }

    public virtual void OnCancel()
    {

    }

    public virtual void OnReturn()
    {

    }

    public virtual void OnEscape()
    {
        bsm.ChangeState<EditBattleState>();
    }

    protected virtual void AddDelegates()
    {
        InputManager.onMouseMovement = OnMouseMovement;
        InputManager.onSelect = OnSelect;
        InputManager.onCancel = OnCancel;
        InputManager.onEscape = OnEscape;
        InputManager.onReturn = OnReturn;
    }

    protected virtual void RemoveDelegates()
    {
        InputManager.onMouseMovement = null;
        InputManager.onSelect = null;
        InputManager.onCancel = null;
        InputManager.onEscape = null;
        InputManager.onReturn = null;
    }

    protected void DisplayUnitHUD(Unit unit)
    {
        if (unit != null)
        {
            UIManager.unitHUD.SetActive(true);
            UIManager.UpdateUnitHUD(unit);
        }
        else UIManager.unitHUD.SetActive(false);
    }
}
