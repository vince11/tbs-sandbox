using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleStateMachine : StateMachine
{
    [System.NonSerialized]
    public GridManager grid;

    [System.NonSerialized]
    public UnitManager unitManager;

    [System.NonSerialized]
    public InputManager inputManager;

    [System.NonSerialized]
    public Node selectedNode;

    [System.NonSerialized]
    public Node destinationNode;

    [System.NonSerialized]
    public ForecastUI forecastUI;

    [System.NonSerialized]
    public BattleLog battleLog;

    [System.NonSerialized]
    public UnitHUD unitHUD;

    [System.NonSerialized]
    public UnitEditor unitEditor;

    [System.NonSerialized]
    public CombatManager combatManager;

    public GameObject actionMenu;
    public GameObject sandBoxMenu;
    public EventSystem eventSystem;

    public void Start()
    {
        BattleStart();
    }

    private void BattleStart()
    {
        grid = FindObjectOfType<GridManager>();
        unitManager = FindObjectOfType<UnitManager>();
        inputManager = FindObjectOfType<InputManager>();
        forecastUI = FindObjectOfType<ForecastUI>();
        battleLog = FindObjectOfType<BattleLog>();
        unitHUD = FindObjectOfType<UnitHUD>();
        combatManager = FindObjectOfType<CombatManager>();
        unitEditor = FindObjectOfType<UnitEditor>();

        actionMenu.SetActive(false);
        unitEditor.gameObject.SetActive(false);
        forecastUI.gameObject.SetActive(false);
        unitHUD.gameObject.SetActive(false);
        //battleLog.gameObject.SetActive(false);

        grid.PlaceUnits(unitManager.playerUnits);

        ChangeState<UnitSelectionState>();
    }
}
